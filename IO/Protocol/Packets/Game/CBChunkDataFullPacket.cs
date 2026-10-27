using DivaMC.Common.IO.Serialization;
using DivaMC.Common.World.Chunks;
using DivaMC.Common.World.Chunks.Heightmaps;
using DivaMC.Common.World.Chunks.Light;
using DivaMC.Common.World.TileEntities;

namespace DivaMC.Common.IO.Protocol.Packets.Game
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