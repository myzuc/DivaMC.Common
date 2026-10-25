using System.Buffers.Binary;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

namespace DivaMC.Common.IO
{
    internal static class StreamExtension
    {
        extension(Stream stream)
        {
            #region U8A
            public async ValueTask<byte[]> ReadU8AAsync(int length, CancellationToken cancellationToken = default)
            {
                byte[] buffer = new byte[length];
                await stream.ReadAtLeastAsync(buffer.AsMemory(), length, true, cancellationToken);
                return buffer;
            }
            public byte[] ReadU8A(int length)
            {
                byte[] buffer = new byte[length];
                stream.ReadAtLeast(buffer, length, true);
                return buffer;
            }
            public async ValueTask WriteU8AAsync(byte[] buffer, CancellationToken cancellationToken = default)
            {
                await stream.WriteAsync(buffer, cancellationToken);
            }
            public void WriteU8A(byte[] buffer)
            {
                stream.Write(buffer);
            }
            #endregion
            #region U1
            public async ValueTask<bool> ReadU1Async(CancellationToken cancellationToken = default)
            {
                return await stream.ReadU8Async(cancellationToken) != 0;
            }
            public bool ReadU1()
            {
                return stream.ReadU8() != 0;
            }
            public async ValueTask WriteU1Async(bool data, CancellationToken cancellationToken = default)
            {
                await stream.WriteU8Async((byte)(data ? 1 : 0), cancellationToken);
            }
            public void WriteU1(bool data)
            {
                stream.WriteU8((byte)(data ? 1 : 0));
            }
            #endregion
            #region U8
            public async ValueTask<byte> ReadU8Async(CancellationToken cancellationToken = default)
            {
                return (await stream.ReadU8AAsync(1, cancellationToken))[0];
            }
            public byte ReadU8()
            {
                return stream.ReadU8A(1)[0];
            }
            public async ValueTask WriteU8Async(byte data, CancellationToken cancellationToken = default)
            {
                await stream.WriteU8AAsync([data], cancellationToken);
            }
            public void WriteU8(byte data)
            {
                stream.WriteU8A([data]);
            }
            #endregion
            #region S8
            public async ValueTask<sbyte> ReadS8Async(CancellationToken cancellationToken = default)
            {
                return (sbyte)(await stream.ReadU8AAsync(1, cancellationToken))[0];
            }
            public sbyte ReadS8()
            {
                return (sbyte)stream.ReadU8A(1)[0];
            }
            public async ValueTask WriteS8Async(sbyte data, CancellationToken cancellationToken = default)
            {
                await stream.WriteU8AAsync([(byte)data], cancellationToken);
            }
            public void WriteS8(sbyte data)
            {
                stream.WriteU8A([(byte)data]);
            }
            #endregion
            #region U16
            public async ValueTask<ushort> ReadU16Async(CancellationToken cancellationToken = default)
            {
                byte[] buffer = await stream.ReadU8AAsync(sizeof(ushort), cancellationToken);
                return BinaryPrimitives.ReadUInt16BigEndian(buffer);
            }
            public ushort ReadU16()
            {
                byte[] buffer = stream.ReadU8A(sizeof(ushort));
                return BinaryPrimitives.ReadUInt16BigEndian(buffer);
            }
            public async ValueTask WriteU16Async(ushort data, CancellationToken cancellationToken = default)
            {
                byte[] buffer = new byte[sizeof(ushort)];
                BinaryPrimitives.WriteUInt16BigEndian(buffer, data);
                await stream.WriteU8AAsync(buffer, cancellationToken);
            }
            public void WriteU16(ushort data)
            {
                byte[] buffer = new byte[sizeof(ushort)];
                BinaryPrimitives.WriteUInt16BigEndian(buffer, data);
                stream.WriteU8A(buffer);
            }
            #endregion
            #region S16
            public async ValueTask<short> ReadS16Async(CancellationToken cancellationToken = default)
            {
                byte[] buffer = await stream.ReadU8AAsync(sizeof(short), cancellationToken);
                return BinaryPrimitives.ReadInt16BigEndian(buffer);
            }
            public short ReadS16()
            {
                byte[] buffer = stream.ReadU8A(sizeof(short));
                return BinaryPrimitives.ReadInt16BigEndian(buffer);
            }
            public async ValueTask WriteS16Async(short data, CancellationToken cancellationToken = default)
            {
                byte[] buffer = new byte[sizeof(short)];
                BinaryPrimitives.WriteInt16BigEndian(buffer, data);
                await stream.WriteU8AAsync(buffer, cancellationToken);
            }
            public void WriteS16(short data)
            {
                byte[] buffer = new byte[sizeof(short)];
                BinaryPrimitives.WriteInt16BigEndian(buffer, data);
                stream.WriteU8A(buffer);
            }
            #endregion
            #region U32
            public async ValueTask<uint> ReadU32Async(CancellationToken cancellationToken = default)
            {
                byte[] buffer = await stream.ReadU8AAsync(sizeof(uint), cancellationToken);
                return BinaryPrimitives.ReadUInt32BigEndian(buffer);
            }
            public uint ReadU32()
            {
                byte[] buffer = stream.ReadU8A(sizeof(uint));
                return BinaryPrimitives.ReadUInt32BigEndian(buffer);
            }
            public async ValueTask WriteU32Async(uint data, CancellationToken cancellationToken = default)
            {
                byte[] buffer = new byte[sizeof(uint)];
                BinaryPrimitives.WriteUInt32BigEndian(buffer, data);
                await stream.WriteU8AAsync(buffer, cancellationToken);
            }
            public void WriteU32(uint data)
            {
                byte[] buffer = new byte[sizeof(uint)];
                BinaryPrimitives.WriteUInt32BigEndian(buffer, data);
                stream.WriteU8A(buffer);
            }
            #endregion
            #region S32
            public async ValueTask<int> ReadS32Async(CancellationToken cancellationToken = default)
            {
                byte[] buffer = await stream.ReadU8AAsync(sizeof(int), cancellationToken);
                return BinaryPrimitives.ReadInt32BigEndian(buffer);
            }
            public int ReadS32()
            {
                byte[] buffer = stream.ReadU8A(sizeof(int));
                return BinaryPrimitives.ReadInt32BigEndian(buffer);
            }
            public async ValueTask WriteS32Async(int data, CancellationToken cancellationToken = default)
            {
                byte[] buffer = new byte[sizeof(int)];
                BinaryPrimitives.WriteInt32BigEndian(buffer, data);
                await stream.WriteU8AAsync(buffer, cancellationToken);
            }
            public void WriteS32(int data)
            {
                byte[] buffer = new byte[sizeof(int)];
                BinaryPrimitives.WriteInt32BigEndian(buffer, data);
                stream.WriteU8A(buffer);
            }
            #endregion
            #region U64
            public async ValueTask<ulong> ReadU64Async(CancellationToken cancellationToken = default)
            {
                byte[] buffer = await stream.ReadU8AAsync(sizeof(ulong), cancellationToken);
                return BinaryPrimitives.ReadUInt64BigEndian(buffer);
            }
            public ulong ReadU64()
            {
                byte[] buffer = stream.ReadU8A(sizeof(ulong));
                return BinaryPrimitives.ReadUInt64BigEndian(buffer);
            }
            public async ValueTask WriteU64Async(ulong data, CancellationToken cancellationToken = default)
            {
                byte[] buffer = new byte[sizeof(ulong)];
                BinaryPrimitives.WriteUInt64BigEndian(buffer, data);
                await stream.WriteU8AAsync(buffer, cancellationToken);
            }
            public void WriteU64(ulong data)
            {
                byte[] buffer = new byte[sizeof(ulong)];
                BinaryPrimitives.WriteUInt64BigEndian(buffer, data);
                stream.WriteU8A(buffer);
            }
            #endregion
            #region S64
            public async ValueTask<long> ReadS64Async(CancellationToken cancellationToken = default)
            {
                byte[] buffer = await stream.ReadU8AAsync(sizeof(long), cancellationToken);
                return BinaryPrimitives.ReadInt64BigEndian(buffer);
            }
            public long ReadS64()
            {
                byte[] buffer = stream.ReadU8A(sizeof(long));
                return BinaryPrimitives.ReadInt64BigEndian(buffer);
            }
            public async ValueTask WriteS64Async(long data, CancellationToken cancellationToken = default)
            {
                byte[] buffer = new byte[sizeof(long)];
                BinaryPrimitives.WriteInt64BigEndian(buffer, data);
                await stream.WriteU8AAsync(buffer, cancellationToken);
            }
            public void WriteS64(long data)
            {
                byte[] buffer = new byte[sizeof(long)];
                BinaryPrimitives.WriteInt64BigEndian(buffer, data);
                stream.WriteU8A(buffer);
            }
            #endregion
            #region F32
            public async ValueTask<float> ReadF32Async(CancellationToken cancellationToken = default)
            {
                byte[] buffer = await stream.ReadU8AAsync(sizeof(float), cancellationToken);
                return BinaryPrimitives.ReadSingleBigEndian(buffer);
            }
            public float ReadF32()
            {
                byte[] buffer = stream.ReadU8A(sizeof(float));
                return BinaryPrimitives.ReadSingleBigEndian(buffer);
            }
            public async ValueTask WriteF32Async(float data, CancellationToken cancellationToken = default)
            {
                byte[] buffer = new byte[sizeof(float)];
                BinaryPrimitives.WriteSingleBigEndian(buffer, data);
                await stream.WriteU8AAsync(buffer, cancellationToken);
            }
            public void WriteF32(float data)
            {
                byte[] buffer = new byte[sizeof(float)];
                BinaryPrimitives.WriteSingleBigEndian(buffer, data);
                stream.WriteU8A(buffer);
            }
            #endregion
            #region F64
            public async ValueTask<double> ReadF64Async(CancellationToken cancellationToken = default)
            {
                byte[] buffer = await stream.ReadU8AAsync(sizeof(double), cancellationToken);
                return BinaryPrimitives.ReadDoubleBigEndian(buffer);
            }
            public double ReadF64()
            {
                byte[] buffer = stream.ReadU8A(sizeof(double));
                return BinaryPrimitives.ReadDoubleBigEndian(buffer);
            }
            public async ValueTask WriteF64Async(double data, CancellationToken cancellationToken = default)
            {
                byte[] buffer = new byte[sizeof(double)];
                BinaryPrimitives.WriteDoubleBigEndian(buffer, data);
                await stream.WriteU8AAsync(buffer, cancellationToken);
            }
            public void WriteF64(double data)
            {
                byte[] buffer = new byte[sizeof(double)];
                BinaryPrimitives.WriteDoubleBigEndian(buffer, data);
                stream.WriteU8A(buffer);
            }
            #endregion
            
            #region VU32
            public async ValueTask<uint> ReadVU32Async(CancellationToken cancellationToken = default)
            {
                uint data = 0;
                int position = 0;
                while (true)
                {
                    byte current = await stream.ReadU8Async(cancellationToken);
                    data |= (current & 127U) << position;
                    if ((current & 128) == 0) return data;
                    position += 7;
                    if (position >= sizeof(uint) * 8) throw new InvalidDataException($"VarInt size exceeded!");
                }
            }
            public uint ReadVU32()
            {
                uint data = 0;
                int position = 0;
                while (true)
                {
                    byte current = stream.ReadU8();
                    data |= (current & 127U) << position;
                    if ((current & 128) == 0) return data;
                    position += 7;
                    if (position >= sizeof(uint) * 8) throw new InvalidDataException($"VarInt size exceeded!");
                }
            }
            public async ValueTask WriteVU32Async(uint data, CancellationToken cancellationToken = default)
            {
                while (data >= 0x80)
                {
                    await stream.WriteU8Async((byte)(data | 0x80), cancellationToken);
                    data >>= 7;
                }
                await stream.WriteU8Async((byte)data, cancellationToken);
            }
            public void WriteVU32(uint data)
            {
                while (data >= 0x80)
                {
                    stream.WriteU8((byte)(data | 0x80));
                    data >>= 7;
                }
                stream.WriteU8((byte)data);
            }
            #endregion
            #region VS32
            public async ValueTask<int> ReadVS32Async(CancellationToken cancellationToken = default)
            {
                return (int)await stream.ReadVU32Async(cancellationToken);
            }
            public int ReadVS32()
            {
                return (int)stream.ReadVU32();
            }
            public async ValueTask WriteVS32Async(int data, CancellationToken cancellationToken = default)
            {
                await stream.WriteVU32Async((uint)data, cancellationToken);
            }
            public void WriteVS32(int data)
            {
                stream.WriteVU32((uint)data);
            }
            #endregion
            #region VU64
            public async ValueTask<ulong> ReadVU64Async(CancellationToken cancellationToken = default)
            {
                ulong data = 0;
                int position = 0;
                while (true)
                {
                    byte current = await stream.ReadU8Async(cancellationToken);
                    data |= (current & 127U) << position;
                    if ((current & 128) == 0) return data;
                    position += 7;
                    if (position >= sizeof(ulong) * 8) throw new InvalidDataException($"VarLong size exceeded!");
                }
            }
            public ulong ReadVU64()
            {
                ulong data = 0;
                int position = 0;
                while (true)
                {
                    byte current = stream.ReadU8();
                    data |= (current & 127U) << position;
                    if ((current & 128) == 0) return data;
                    position += 7;
                    if (position >= sizeof(ulong) * 8) throw new InvalidDataException($"VarLong size exceeded!");
                }
            }
            public async ValueTask WriteVU64Async(ulong data, CancellationToken cancellationToken = default)
            {
                do
                {
                    byte current = (byte)data;
                    data >>= 7;
                    if (data != 0) current |= 128;
                    else current &= 127;
                    await stream.WriteU8Async(current, cancellationToken);
                }
                while (data != 0);
            }
            public void WriteVU64(ulong data)
            {
                do
                {
                    byte current = (byte)data;
                    data >>= 7;
                    if (data != 0) current |= 128;
                    else current &= 127;
                    stream.WriteU8(current);
                }
                while (data != 0);
            }
            #endregion
            #region VS64
            public async ValueTask<long> ReadVS64Async(CancellationToken cancellationToken = default)
            {
                return (long)await stream.ReadVU64Async(cancellationToken);
            }
            public long ReadVS64()
            {
                return (long)stream.ReadVU64();
            }
            public async ValueTask WriteVS64Async(long data, CancellationToken cancellationToken = default)
            {
                await stream.WriteVU64Async((ulong)data, cancellationToken);
            }
            public void WriteVS64(long data)
            {
                stream.WriteVU64((ulong)data);
            }
            #endregion
            
            #region U0
            public async ValueTask<ulong> ReadU0Async(int size, CancellationToken cancellationToken = default)
            {
                if (size < 0) throw new ArgumentException("Integer size is negative.");
                if (size > 8) throw new ArgumentException("Integer size exceeds maximum bound.");
                ulong data = 0;
                
                for (int i = 0; i < size; i++)
                {
                    data <<= 8;
                    data |= await stream.ReadU8Async(cancellationToken);
                }
                return data;
            }
            public ulong ReadU0(int size)
            {
                if (size < 0) throw new ArgumentException("Integer size is negative.");
                if (size > 8) throw new ArgumentException("Integer size exceeds maximum bound.");
                ulong data = 0;
                
                for (int i = 0; i < size; i++)
                {
                    data <<= 8;
                    data |= stream.ReadU8();
                }
                return data;
            }
            public async ValueTask WriteU0Async(ulong data, int size, CancellationToken cancellationToken = default)
            {
                if (size < 0) throw new ArgumentException("Integer size is negative.");
                if (size > 8) throw new ArgumentException("Integer size exceeds maximum bound.");
                
                for (int i = 0; i < size; i++)
                {
                    await stream.WriteU8Async((byte)data, cancellationToken);
                    data >>= 8;
                }
            }
            public void WriteU0(ulong data, int size)
            {
                if (size < 0) throw new ArgumentException("Integer size is negative.");
                if (size > 8) throw new ArgumentException("Integer size exceeds maximum bound.");
                
                for (int i = 0; i < size; i++)
                {
                    stream.WriteU8((byte)data);
                    data >>= 8;
                }
            }
            #endregion
            #region S0
            public async ValueTask<long> ReadS0Async(int size, CancellationToken cancellationToken = default)
            {
                return (long)await stream.ReadU0Async(size, cancellationToken);
            }
            public long ReadS0(int size)
            {
                return (long)stream.ReadU0(size);
            }
            public async ValueTask WriteS0Async(long data, int size, CancellationToken cancellationToken = default)
            {
                await stream.WriteU0Async((ulong)data, size, cancellationToken);
            }
            public void WriteS0(long data, int size)
            {
                stream.WriteU0((ulong)data, size);
            }
            #endregion

            #region Guid
            public async ValueTask<Guid> ReadGuidAsync(CancellationToken cancellationToken = default)
            {
                return new Guid(await stream.ReadU8AAsync(16, cancellationToken), true);
            }
            public Guid ReadGuid()
            {
                return new Guid(stream.ReadU8A(16), true);
            }
            public async ValueTask WriteGuidAsync(Guid data, CancellationToken cancellationToken = default)
            {
                await stream.WriteU8AAsync(data.ToByteArray(true), cancellationToken);
            }
            public void WriteGuid(Guid data)
            {
                stream.WriteU8A(data.ToByteArray(true));
            }
            #endregion
            
            #region T0
            public string ReadT0AVS32()
            {
                byte[] buffer = stream.ReadU8A(stream.ReadVS32());
                return Encoding.UTF8.GetString(buffer);
            }
            public void WriteT0AVS32(string data)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                stream.WriteVS32(buffer.Length);
                stream.WriteU8A(buffer);
            }
            #endregion
            #region JSON
            public T ReadJSON<T>()
            {
                T? data = JsonSerializer.Deserialize<T>(stream.ReadT0AVS32(), Global.JsonSerializerOptions);
                if (data is null) throw new InvalidDataException("Object is null!");
                return data;
            }

            public void WriteJSON<T>(T data)
            {
                stream.WriteT0AVS32(JsonSerializer.Serialize(data, Global.JsonSerializerOptions));
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

            #region IBinarySerializable
            public T Read<T>() where T : IBinarySerializable<T>
            {
                return T.Deserialize(stream);
            }

            public void Write<T>(T data) where T : IBinarySerializable<T>
            {
                data.Serialize(stream);
            }
            #endregion
        }
    }
}