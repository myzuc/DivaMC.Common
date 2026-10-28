using DivaMC.Protocol.IO.Serialization;
using DivaMC.Protocol.World.Chunks;
using DivaMC.Protocol.World.Chunks.Heightmaps;
using DivaMC.Protocol.World.Chunks.Light;
using DivaMC.Protocol.World.TileEntities;

namespace DivaMC.Protocol.IO.Protocol.Packets.Game
{
    public sealed class CBChunkDataFullPacket : Packet
    {
        static CBChunkDataFullPacket()
        {
            Register<CBChunkDataFullPacket>(new(ProtocolDirection.Clientbound, ProtocolStage.Game,0x2E), Deserialize);
        }
        
        public required ChunkLocation Location { get; set; }
        public required List<Heightmap> Heightmaps { get; set; }
        public required byte[] ChunkData { get; set; }
        public required List<TileEntity> TileEntities { get; set; }
        public required LightChunk LightData { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
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