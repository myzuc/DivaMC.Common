using System.Collections;
using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Data
{
    public sealed class PackedArray : IList<int>, IBinarySerializable<PackedArray, (int bits, int length)>
    {
        private sealed class Enumerator : IEnumerator<int>
        {
            private readonly PackedArray Data;
            private int Index;
            public int Current { get; private set; }
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
            if (bits < 1 || bits > 31) throw new ArgumentException("Invalid bit amount!");
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
        
        public int this[int index]
        {
            get
            {
                if (index < 0 || index > Length) throw new IndexOutOfRangeException();

                int entriesPerWord = 64 / Bits;
                int offset = index / entriesPerWord;
                int shift = (index % entriesPerWord) * Bits;
                ulong mask = Bits < 64 ? (1ul << Bits) - 1 : ulong.MaxValue;
                
                ulong value = (Buffer[offset] >> shift) & mask;
                return (int)value;
            }
            set
            {
                if (index < 0 || index > Length) throw new IndexOutOfRangeException();
                if (value > (Bits < 64 ? (1 << Bits) - 1 : int.MaxValue)) throw new ArgumentOutOfRangeException();
                
                int entriesPerWord = 64 / Bits;
                int offset = index / entriesPerWord;
                int shift = (index % entriesPerWord) * Bits;
                ulong mask = Bits < 64 ? (1ul << Bits) - 1 : ulong.MaxValue;

                Buffer[offset] &= ~(mask << shift);
                Buffer[offset] |= ((ulong)value & mask) << shift;
            }
        }
        
        public void Serialize(Stream stream, (int bits, int length) context)
        {
            if (context.bits != Bits || context.length != Length) throw new ArgumentException();
            stream.WriteU64A(Buffer);
        }
        public static PackedArray Deserialize(Stream stream, (int bits, int length) context)
        {
            return new(stream.ReadU64A(DetermineBufferSize(context.bits, context.length)), context.bits, context.length);
        }
        
        void ICollection<int>.Add(int item)
        {
            throw new InvalidOperationException();
        }
        void IList<int>.Insert(int index, int item)
        {
            throw new InvalidOperationException();
        }
        bool ICollection<int>.Remove(int item)
        {
            throw new InvalidOperationException();
        }
        void IList<int>.RemoveAt(int index)
        {
            throw new InvalidOperationException();
        }
        public void Clear()
        {
            Array.Clear(Buffer);
        }
        public void CopyTo(int[] array, int arrayIndex)
        {
            for (int i = 0; i < Length; i++)
            {
                array[arrayIndex + i] = this[i];
            }
        }
        public bool Contains(int item)
        {
            for (int i = 0; i < Length; i++)
            {
                if (this[i] == item) return true;
            }
            return false;
        }
        public int IndexOf(int item)
        {
            for (int i = 0; i < Length; i++)
            {
                if (this[i] == item) return i;
            }
            return -1;
        }
        public IEnumerator<int> GetEnumerator()
        {
            return new Enumerator(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        private static int DetermineBufferSize(int bits, int length)
        {
            int entriesPerWord = 64 / bits;
            int size = (length + entriesPerWord - 1) / entriesPerWord;
            return size;
        }
        
        public PackedArray Resize(byte bits, int length)
        {
            PackedArray destination = new(bits, length);
            CopyTo(destination);
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