using System.IO.Compression;
using System.Net.Sockets;
using DivaMC.Common.IO.Cryptography;
using DivaMC.Common.IO.Protocol.Packets;
using DivaMC.Common.IO.Serialization;
using HandshakePackets = DivaMC.Common.IO.Protocol.Packets.Handshake;
using StatusPackets = DivaMC.Common.IO.Protocol.Packets.Status;
using LoginPackets = DivaMC.Common.IO.Protocol.Packets.Login;
using ConfigurationPackets = DivaMC.Common.IO.Protocol.Packets.Configuration;
using GamePackets = DivaMC.Common.IO.Protocol.Packets.Game;

namespace DivaMC.Common.IO.Protocol
{
   public sealed class Connection : IDisposable, IAsyncDisposable
   {
       private Stream Stream { get; set; }
       private SemaphoreSlim ReadSync { get; } = new(1, 1);
       private SemaphoreSlim WriteSync { get; } = new(1, 1);
       private int CompressionThreshold { get; set; } = -1;
       private readonly bool RemoteIsClient;
       private bool Disposed;
       
       public Connection(Socket socket, bool remoteIsClient)
       {
           Stream = new NetworkStream(socket, true);
           RemoteIsClient = remoteIsClient;
       }
       public void Encrypt(byte[] secret)
       {
           Stream = new AesCfbStream(Stream, secret, secret, false);
       }
       public void Compress(int threshold)
       {
           CompressionThreshold = threshold;
       }
       public async Task<Packet> ReadAsync(PacketContext context, CancellationToken cancellationToken = default)
       {
           context = context with
           {
               ProtocolDirection = RemoteIsClient ? ProtocolDirection.Serverbound : ProtocolDirection.Clientbound,
           };
           await using Stream ms = await ReadAsync(cancellationToken);
           return Packet.Deserialize(ms, context);
       }
       private async Task<Stream> ReadAsync(CancellationToken cancellationToken = default)
       {
           byte[] data;
           try
           {
               await ReadSync.WaitAsync(cancellationToken);
               data = await Stream.ReadU8AAsync(await Stream.ReadVS32Async(cancellationToken), cancellationToken);
           }
           finally
           {
               ReadSync.Release();
           }
           if (CompressionThreshold < 0) return new MemoryStream(data);
           Stream ms2 = new MemoryStream(data);
           int decompressedSize = ms2.ReadVS32();
           if (decompressedSize <= 0) return ms2;
           await using ZLibStream zlib = new(ms2, CompressionMode.Decompress, false);
           return new MemoryStream(zlib.ReadU8A(decompressedSize));
       }
       public async Task WriteAsync(Packet packet, PacketContext context, CancellationToken cancellationToken = default)
       {
           context = context with
           {
               ProtocolDirection = RemoteIsClient ? ProtocolDirection.Clientbound : ProtocolDirection.Serverbound,
           };
           using MemoryStream ms = new();
           packet.Serialize(ms, context);
           await WriteAsync(ms.ToArray(), cancellationToken);
       }
       private async Task WriteAsync(byte[] data, CancellationToken cancellationToken = default)
       {
           if (CompressionThreshold >= 0)
           {
               using MemoryStream ms2 = new();
               if (data.Length < CompressionThreshold)
               {
                   ms2.WriteVS32(0);
                   ms2.WriteU8A(data);
               }
               else
               {
                   ms2.WriteVS32(data.Length);
                   await using ZLibStream zlib = new(ms2, CompressionMode.Compress, true);
                   zlib.WriteU8A(data);
                   zlib.Flush();
               }
               data = ms2.ToArray();
           }
           try
           {
               await WriteSync.WaitAsync(cancellationToken);
               await Stream.WriteVS32Async(data.Length, cancellationToken);
               await Stream.WriteU8AAsync(data, cancellationToken);
               await Stream.FlushAsync(cancellationToken);
           }
           finally
           {
               WriteSync.Release();
           }
       }
       public void Dispose()
       {
           if (Disposed) return;
           Disposed = true;
           Stream.Dispose();
       }
       public async ValueTask DisposeAsync()
       {
           if (Disposed) return;
           Disposed = true;
           await Stream.DisposeAsync();
       }
   }
}
   