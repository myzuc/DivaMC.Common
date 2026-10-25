using System.Collections.Concurrent;
using System.Reflection;

namespace DivaMC.Common.IO.Protocol.Packets
{
    public abstract record Packet
    {
        protected delegate Packet Deserializer(Stream stream);
        private static readonly ConcurrentDictionary<PacketSignature, Deserializer> Deserializers = [];
        private static readonly ConcurrentDictionary<Type, PacketSignature> Signatures = [];

        static Packet()
        {
            IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsAssignableTo(typeof(Packet)) && !t.IsAbstract);
            foreach (Type type in types)
            {
                System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(type.TypeHandle);
            }
        }

        public virtual PacketSignature Signature
        {
            get
            {
                if (!Signatures.TryGetValue(GetType(), out PacketSignature value))
                {
                    throw new InvalidOperationException("Unknown packet!");
                }
                return value;
            }
        }
        protected internal Packet()
        {
            
        }
        
        protected abstract void Serialize(Stream stream);
        public static Packet? Deserialize(PacketSignature signature, Stream stream)
        {
            if (!Deserializers.TryGetValue(signature, out Deserializer? deserializer))
            {
                return null;
            }
            Packet packet = deserializer(stream);
            return packet;
        }
        public static void Serialize(Packet packet, Stream stream)
        {
            packet.Serialize(stream);
        }

        protected static void Register<TPacket>(PacketSignature signature, Deserializer deserializer) where TPacket : Packet
        {
            if (!Deserializers.TryAdd(signature, deserializer))
            {
                throw new InvalidOperationException("Duplicate packet deserializer!");
            }
            if (!Signatures.TryAdd(typeof(TPacket), signature))
            {
                throw new InvalidOperationException("Duplicate packet signature!");
            }
        }
    }
}