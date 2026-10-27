using DivaMC.Protocol.IO.Serialization;
using fNbt;

namespace DivaMC.Protocol.World.TileEntities
{
    public sealed class TileEntity : IBinarySerializable<TileEntity>
    {
        public TileEntityType Type { get; }
        public required TileEntityLocation Location { get; set; }
        public required NbtTag Data { get; set; }
        public TileEntity(TileEntityType type)
        {
            Type = type;
        }
        
        public void Serialize(Stream stream)
        {
            stream.Write(Location);
            stream.WriteVS32(Type);
            stream.WriteNbt(Data);
        }
        public static TileEntity Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}