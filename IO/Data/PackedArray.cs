using System.Collections;
using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Data
{
    public sealed class PackedArray : IList<ulong>, IBinarySerializable<PackedArray, (int bits, int length)>
    {
        private sealed class Enumerator : IEnumerator<ulong>
        {
            private readonly PackedArray Data;
            private int Index;
            public ulong Current { get; private set; }
            object IEnumerator.Current => Current;
            public Enumerator(PackedArray data)
            {
                Data = data;
                Index = -1;
                Current = 0;
            }
            public void Dispose()
            {

            }
            public bool MoveNext()
            {
                if (Index + 1 >= Data.Length) return false;
                Current = Data[++Index];
                return true;
            }
            public void Reset()
            {
                Index = -1;
                Current = 0;
            }
        }
        
        public int Count => Length;
        public bool IsReadOnly => Buffer.IsReadOnly;
        
        public int Bits { get; }
        public int Length { get; }
        private ulong[] Buffer { get; }
        
        public PackedArray(int bits, int length)
        {
            if (bits < 1 || bits > 64) throw new ArgumentException("Invalid bit amount!");
            if (length < 0 || length > int.MaxValue / 64) throw new ArgumentException("Invalid length!");
            Bits = bits;
            Length = length;
            Buffer = new ulong[DetermineBufferSize(bits, length)];
        }
        public PackedArray(ulong[] buffer, int bits, int length) : this(bits, length)
        {
            if (buffer.Length < Buffer.Length) throw new ArgumentException("Buffer too small!");
            Array.Copy(buffer, Buffer, Buffer.Length);
        }
        
        public ulong this[int index]
        {
            get
            {
                if (index < 0 || index > Length) throw new IndexOutOfRangeException();
                
                int totalBitOffsetLowest = index * Bits;
                int totalBitOffsetHighest = totalBitOffsetLowest + Bits;
                
                int wordOffsetLow = totalBitOffsetLowest / 64;
                int wordOffsetHigh = totalBitOffsetHighest / 64;
                int shiftLow = totalBitOffsetLowest % 64;
                int shiftHigh = 64 - shiftLow;

                ulong mask = Bits < 64 ? (1ul << Bits) - 1 : ulong.MaxValue;
                ulong maskLow = mask << shiftLow;
                ulong maskHigh = shiftHigh == 64 ? 0 : mask >> shiftHigh;
                
                ulong wordLow = (Buffer[wordOffsetLow] & maskLow) >> shiftLow;
                ulong wordHigh = (Buffer[wordOffsetHigh] & maskHigh) << shiftHigh;

                ulong wordFull = wordLow | wordHigh;
                return wordFull;
            }
            set
            {
                if (index < 0 || index > Length) throw new IndexOutOfRangeException();
                if (value > (Bits < 64 ? (1ul << Bits) - 1 : ulong.MaxValue)) throw new ArgumentOutOfRangeException();
                
                int totalBitOffsetLowest = index * Bits;
                int totalBitOffsetHighest = totalBitOffsetLowest + Bits;
                
                int wordOffsetLow = totalBitOffsetLowest / 64;
                int wordOffsetHigh = totalBitOffsetHighest / 64;
                int shiftLow = totalBitOffsetLowest % 64;
                int shiftHigh = 64 - shiftLow;

                ulong mask = Bits < 64 ? (1ul << Bits) - 1 : ulong.MaxValue;
                ulong maskLow = mask << shiftLow;
                ulong maskHigh = shiftHigh == 64 ? 0 : mask >> shiftHigh;

                Buffer[wordOffsetLow] &= ~maskLow;
                Buffer[wordOffsetLow] |= (value << shiftLow) & maskLow;
                Buffer[wordOffsetHigh] &= ~maskHigh;
                Buffer[wordOffsetHigh] |= (value >> shiftHigh) & maskHigh;
            }
        }
        
        public void Serialize(Stream stream, (int bits, int length) context)
        {
            if (context.bits != Bits || context.length != Length) throw new ArgumentException();
            stream.WriteVS32(Buffer.Length);
            stream.WriteU64A(Buffer);
        }
        public static PackedArray Deserialize(Stream stream, (int bits, int length) context)
        {
            return new(stream.ReadU64A(stream.ReadVS32()), context.bits, context.length);
        }
        
        void ICollection<ulong>.Add(ulong item)
        {
            throw new InvalidOperationException();
        }
        void IList<ulong>.Insert(int index, ulong item)
        {
            throw new InvalidOperationException();
        }
        bool ICollection<ulong>.Remove(ulong item)
        {
            throw new InvalidOperationException();
        }
        void IList<ulong>.RemoveAt(int index)
        {
            throw new InvalidOperationException();
        }
        public void Clear()
        {
            Array.Clear(Buffer);
        }
        public void CopyTo(ulong[] array, int arrayIndex)
        {
            for (int i = 0; i < Length; i++)
            {
                array[arrayIndex + i] = this[i];
            }
        }
        public bool Contains(ulong item)
        {
            for (int i = 0; i < Length; i++)
            {
                if (this[i] == item) return true;
            }
            return false;
        }
        public int IndexOf(ulong item)
        {
            for (int i = 0; i < Length; i++)
            {
                if (this[i] == item) return i;
            }
            return -1;
        }
        public IEnumerator<ulong> GetEnumerator()
        {
            return new Enumerator(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        private static int DetermineBufferSize(int bits, int length)
        {
            return (length - 1) / (64 / bits) + 1;
        }
        
        public PackedArray Resize(byte bits, int length)
        {
            PackedArray destination = new(bits, length);
            ulong mask = Bits < 64 ? (1ul << Bits) - 1 : ulong.MaxValue;
            for (int i = 0; i < length && i < Length; i++)
            {
                destination[i] = this[i] & mask;
            }
            return destination;
        }
        public ulong[] ExportBuffer()
        {
            ulong[] buffer = new ulong[Buffer.Length];
            Array.Copy(Buffer, buffer, Buffer.Length);
            return buffer;
        }
        public void CopyTo(PackedArray destination)
        {
            if (destination.Length != Length || destination.Bits != Bits) throw new ArgumentException();
            Array.Copy(Buffer, destination.Buffer, Buffer.Length);
        }
    }
}