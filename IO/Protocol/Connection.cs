using System.IO.Compression;
using System.Net.Sockets;
using DivaMC.Common.IO.Protocol.Packets;

namespace DivaMC.Common.IO.Protocol
{
   public sealed class Connection : IDisposable, IAsyncDisposable
   {
       private Stream Stream { get; set; }
       private SemaphoreSlim ReadSync { get; } = new(1, 1);
       private SemaphoreSlim WriteSync { get; } = new(1, 1);
       public ProtocolStage ProtocolStage { get; private set; } = ProtocolStage.Handshake;
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
       public async Task<Packet> ReadAsync(CancellationToken cancellationToken = default)
       {
           await using Stream ms = await readRawAsync();
           int id = ms.ReadVS32();
           PacketSignature signature = new(RemoteIsClient ? ProtocolDirection.Serverbound : ProtocolDirection.Clientbound, ProtocolStage, id);
           Packet packet = Packet.Deserialize(signature, ms, null);
           Run(packet);
           return packet;

           async Task<Stream> readRawAsync()
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
       }
       public async Task WriteAsync(Packet packet, CancellationToken cancellationToken = default)
       {
           if (packet.Signature.Direction != (RemoteIsClient ? ProtocolDirection.Clientbound : ProtocolDirection.Serverbound) || packet.Signature.Stage != ProtocolStage) throw new InvalidOperationException($"Disallowed packet!");
           using MemoryStream ms = new();
           ms.WriteVS32(packet.Signature.Id);
           Packet.Serialize(packet, ms, null);
           await writeRawAsync(ms.ToArray());
           Run(packet);
           return;
           
           async Task writeRawAsync(byte[] data)
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
               }
               finally
               {
                   WriteSync.Release();
               }
           }
       }
       private void Run(Packet packet)
       {
           switch (packet)
           {
               case Packets.Handshake.Serverbound.Intention intention:
               {
                   ProtocolStage = intention.Intent switch
                   {
                       Intent.Status => ProtocolStage.Status,
                       Intent.Login or Intent.Transfer => ProtocolStage.Login,
                       _ => throw new InvalidDataException("Unknown intent!")
                   };
                   break;
               }
               /*case Protocol.Packets.Login.Clientbound.DisconnectPacket:
               {
                   ProtocolStage = this.ProtocolStage.Disconnected;
                   break;
               }
               case CompressionPacket loginCompressionPacket:
               {
                   CompressionThreshold = loginCompressionPacket.Threshold;
                   break;
               }
               case Protocol.Packets.Login.Serverbound.EndPacket:
               {
                   ProtocolStage = this.ProtocolStage.Configuration;
                   break;
               }
               case DisconnectPacket:
               {
                   ProtocolStage = this.ProtocolStage.Disconnected;
                   break;
               }
               case EndPacket:
               {
                   ProtocolStage = this.ProtocolStage.Play;
                   break;
               }*/
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
   