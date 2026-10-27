using DivaMC.Common.IO.Serialization;
using DivaMC.Common.World.Chunks;
using DivaMC.Common.World.Chunks.Heightmaps;
using DivaMC.Common.World.Chunks.Light;
using DivaMC.Common.World.TileEntities;

namespace DivaMC.Common.IO.Protocol.Packets.Game.Clientbound
{
    public sealed class ChunkDataFullPacket : Packet
    {
        static ChunkDataFullPacket()
        {
            Register<ChunkDataFullPacket>(Signature, Deserialize);
        }
    
        new public static readonly PacketSignature Signature = new PacketSignature(
            ProtocolDirection.Clientbound,
            ProtocolStage.Play,
            0x2E
        );
        
        public required ChunkLocation Location { get; set; }
        public required List<Heightmap> Heightmaps { get; set; }
        public required byte[] ChunkData { get; set; }
        public required List<TileEntity> TileEntities { get; set; }
        public required LightChunk LightData { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            context.Check(Signature);
            stream.Write(Location);
            stream.WriteVS32(Heightmaps.Count);
            foreach (Heightmap heightmap in Heightmaps)
            {
                if (heightmap.Context.Dimension != context.ConnectionContext.Dimension) throw new InvalidOperationException();
                stream.Write(heightmap, context.ConnectionContext.DeriveChunkContext());
            }
            stream.WriteVS32(ChunkData.Length);
            stream.Write(ChunkData);
            stream.WriteVS32(TileEntities.Count);
            foreach (TileEntity tileEntity in TileEntities)
            {
                stream.Write(tileEntity);
            }
            stream.Write(LightData, context.ConnectionContext.DeriveChunkContext());
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
    }
}