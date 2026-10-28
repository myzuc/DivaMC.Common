using System.Drawing;
using System.Runtime.InteropServices;
using fNbt;

namespace DivaMC.Protocol.IO.Serialization
{
    internal static class NbtExtension
    {
        extension(NbtTag nbt)
        {
            public NbtTag Rename(string name)
            {
                nbt.Name = name;
                return nbt;
            }
            public void AddTo(NbtCompound compound)
            {
                compound.Add(nbt);
            }
            public void CloneTo(NbtCompound compound)
            {
                compound.Add((NbtTag)nbt.Clone());
            }
        }
        extension(NbtByte nbt)
        {
            public sbyte ToS8()
            {
                return (sbyte)nbt.Value;
            }
            public static NbtByte FromS8(sbyte value, string? name = null)
            {
                return new(name, (byte)value);
            }
            public byte ToU8()
            {
                return nbt.Value;
            }
            public static NbtByte FromU8(byte value)
            {
                return new(value);
            }
            public bool ToU1()
            {
                return nbt.Value != 0x00;
            }
            public static NbtByte FromU1(bool value)
            {
                return new(value ? (byte)0x01 : (byte)0x00);
            }
        }
        extension(NbtShort nbt)
        {
            public short ToS16()
            {
                return nbt.Value;
            }
            public static NbtShort FromS16(short value)
            {
                return new(value);
            }
            public ushort ToU16()
            {
                return (ushort)nbt.Value;
            }
            public static NbtShort FromU16(ushort value)
            {
                return new((short)value);
            }
            public ushort ToT16()
            {
                return (char)nbt.Value;
            }
            public static NbtShort FromT16(char value)
            {
                return new((short)value);
            }
        }
        extension(NbtInt nbt)
        {
            public int ToS32()
            {
                return nbt.Value;
            }
            public static NbtInt FromS32(int value)
            {
                return new(value);
            }
            public uint ToU32()
            {
                return (uint)nbt.Value;
            }
            public static NbtInt FromU32(uint value)
            {
                return new((int)value);
            }
            public Color ToColor()
            {
                return Color.FromArgb(nbt.Value);
            }
            public static NbtInt FromColor(Color value)
            {
                return new(value.ToArgb());
            }
        }
        extension(NbtLong nbt)
        {
            public long ToS64()
            {
                return nbt.Value;
            }
            public static NbtLong FromS64(long value)
            {
                return new(value);
            }
            public ulong ToU64()
            {
                return (ulong)nbt.Value;
            }
            public static NbtLong FromU64(ulong value)
            {
                return new((long)value);
            }
        }
        extension(NbtFloat nbt)
        {
            public float ToF32()
            {
                return nbt.Value;
            }
            public static NbtFloat FromF32(float value)
            {
                return new(value);
            }
        }
        extension(NbtDouble nbt)
        {
            public double ToF64()
            {
                return nbt.Value;
            }
            public static NbtDouble FromF64(double value)
            {
                return new(value);
            }
        }
        extension(NbtByteArray nbt)
        {
            public sbyte[] ToS8A()
            {
                return MemoryMarshal.Cast<byte, sbyte>(nbt.Value).ToArray();
            }
            public static NbtByteArray FromS8A(sbyte[] value)
            {
                return new(MemoryMarshal.Cast<sbyte, byte>(value).ToArray());
            }
            public byte[] ToU8A()
            {
                return nbt.Value;
            }
            public static NbtByteArray FromU8A(byte[] value)
            {
                return new(value);
            }
            public bool[] ToU1A()
            {
                return MemoryMarshal.Cast<byte, bool>(nbt.Value).ToArray();
            }
            public static NbtByteArray FromU1A(bool[] value)
            {
                return new(MemoryMarshal.Cast<bool, byte>(value).ToArray());
            }
        }
        extension(NbtIntArray nbt)
        {
            public int[] ToS32A()
            {
                return nbt.Value;
            }
            public static NbtIntArray FromS32A(int[] value)
            {
                return new(value);
            }
            public uint[] ToU32A()
            {
                return MemoryMarshal.Cast<int, uint>(nbt.Value).ToArray();
            }
            public static NbtIntArray FromU32A(uint[] value)
            {
                return new(MemoryMarshal.Cast<uint, int>(value).ToArray());
            }
        }
        extension(NbtLongArray nbt)
        {
            public long[] ToS64A()
            {
                return nbt.Value;
            }
            public static NbtLongArray FromS64A(long[] value)
            {
                return new(value);
            }
            public ulong[] ToU64A()
            {
                return MemoryMarshal.Cast<long, ulong>(nbt.Value).ToArray();
            }
            public static NbtLongArray FromU64A(ulong[] value)
            {
                return new(MemoryMarshal.Cast<ulong, long>(value).ToArray());
            }
        }
        extension(NbtString nbt)
        {
            public string ToT0A()
            {
                return nbt.Value;
            }
            public static NbtString FromT0A(string value)
            {
                return new(value);
            }
        }
        extension(NbtList nbt)
        {
            public IEnumerable<T> ToIEnumerable<T>() where T : INbtSerializable<T>
            {
                return nbt.Select(T.FromNbt);
            }
            public static NbtList FromIEnumerable<T>(IEnumerable<T> value) where T : INbtSerializable<T>
            {
                return new(value.Select(entry => entry.ToNbt()));
            }
            public IEnumerable<NbtTag> ToIEnumerable()
            {
                return nbt;
            }
            public static NbtList FromIEnumerable(IEnumerable<NbtTag> value)
            {
                return new(value);
            }
        }
    }
}