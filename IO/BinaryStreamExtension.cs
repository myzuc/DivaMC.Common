using System.Buffers.Binary;
using System.Collections.Immutable;
using System.Runtime.InteropServices;
using System.Text;
using Me.Shiokawaii.IO;

namespace Net.Myzuc.DivaMC.Common.IO
{
    internal static class BinaryStreamExtension
    {
        extension(BinaryStream stream)
        {
            #region T0
            public string ReadT0AU16()
            {
                byte[] buffer = stream.ReadU8A(stream.ReadU16());
                return Encoding.UTF8.GetString(buffer);
            }
            public void WriteT0AU16(string data)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                stream.WriteU16((ushort)buffer.Length);
                if (buffer.Length > ushort.MaxValue) throw new ArgumentException("String exceeds encodable length");
                stream.WriteU8A(buffer);
            }
            #endregion

            #region S32A
            public Span<int> ReadS32A(int size)
            {
                Span<byte> buffer = stream.ReadU8A(size * sizeof(int));
                Span<int> data = MemoryMarshal.Cast<byte, int>(buffer);
                if (BitConverter.IsLittleEndian) BinaryPrimitives.ReverseEndianness(data, data);
                return data;
            }
            public void WriteS32A(ReadOnlySpan<int> data)
            {
                if(!BitConverter.IsLittleEndian)
                {
                    stream.WriteU8A(MemoryMarshal.AsBytes(data).ToArray());
                    return;
                }
                Span<int> reversed = new(new int[data.Length]);
                BinaryPrimitives.ReverseEndianness(data, reversed);
                stream.WriteU8A(MemoryMarshal.AsBytes(reversed).ToArray());
            }
            #endregion

            #region S64A
            public Span<long> ReadS64A(int size)
            {
                Span<byte> buffer = stream.ReadU8A(size * sizeof(long));
                Span<long> data = MemoryMarshal.Cast<byte, long>(buffer);
                if (BitConverter.IsLittleEndian) BinaryPrimitives.ReverseEndianness(data, data);
                return data;
            }
            public void WriteS64A(ReadOnlySpan<long> data)
            {
                if(!BitConverter.IsLittleEndian)
                {
                    stream.WriteU8A(MemoryMarshal.AsBytes(data).ToArray());
                    return;
                }
                Span<long> reversed = new(new long[data.Length]);
                BinaryPrimitives.ReverseEndianness(data, reversed);
                stream.WriteU8A(MemoryMarshal.AsBytes(reversed).ToArray());
            }
            #endregion
        }
    }
}