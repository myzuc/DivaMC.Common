using System.Collections;
using DivaMC.Protocol.IO.Serialization;

namespace DivaMC.Protocol.World.Chunks.Light
{
    public sealed class LightChunk : IByteSerializable<LightChunk, ChunkContext>
    {
        public ChunkContext Context { get; }
        public LightTypeChunk Sky { get; }
        public LightTypeChunk Tile { get; }
        
        public LightChunk(ChunkContext context)
        {
            Context = context;
            Sky = new(context);
            Tile = new(context);
        }
        
        public void Serialize(Stream stream, ChunkContext context)
        {
            if (context != Context) throw new InvalidOperationException();
            
            BitArray skyDataMask = new(Sky.Sections);
            for (int i = 0; i < Sky.Sections; i++)
            {
                skyDataMask[i] = !Sky.Data[i]?.IsEmpty ?? false;
            }
            stream.WriteVS32((skyDataMask.Length + 7) / 8);
            stream.WriteU1A(skyDataMask);
            //stream.WriteU1A(skyDataMask, (skyDataMask.Bits, skyDataMask.Length));
            BitArray tileDataMask = new(Tile.Sections);
            for (int i = 0; i < Tile.Sections; i++)
            {
                tileDataMask[i] = !Tile.Data[i]?.IsEmpty ?? false;
            }
            stream.WriteVS32((tileDataMask.Length + 7) / 8);
            stream.WriteU1A(tileDataMask);
            //stream.WriteU1A(tileDataMask, (tileDataMask.Bits, tileDataMask.Length));
            BitArray skyEmptyMask = new(Sky.Sections);
            for (int i = 0; i < Sky.Sections; i++)
            {
                skyEmptyMask[i] = Sky.Data[i]?.IsEmpty ?? false;
            }
            stream.WriteVS32((skyEmptyMask.Length + 7) / 8);
            stream.WriteU1A(skyEmptyMask);
            //stream.WriteU1A(skyEmptyMask, (skyEmptyMask.Bits, skyEmptyMask.Length));
            BitArray tileEmptyMask = new(Tile.Sections);
            for (int i = 0; i < Tile.Sections; i++)
            {
                tileEmptyMask[i] = Tile.Data[i]?.IsEmpty ?? false;
            }
            stream.WriteVS32((tileEmptyMask.Length + 7) / 8);
            stream.WriteU1A(tileEmptyMask);
            //stream.WriteU1A(tileEmptyMask, (tileEmptyMask.Bits, tileEmptyMask.Length));
            stream.WriteVS32(skyDataMask.Cast<bool>().Count(v => v));
            for (int i = 0; i < Sky.Sections; i++)
            {
                if (!skyDataMask[i]) continue;
                stream.Write(Sky.Data[i]!);
            }
            stream.WriteVS32(tileDataMask.Cast<bool>().Count(v => v));
            for (int i = 0; i < Tile.Sections; i++)
            {
                if (!tileDataMask[i]) continue;
                stream.Write(Tile.Data[i]!);
            }
        }
        public static LightChunk Deserialize(Stream stream, ChunkContext context)
        {
            throw new NotImplementedException();
        }
    }
}