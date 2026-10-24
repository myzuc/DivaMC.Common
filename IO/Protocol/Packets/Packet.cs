using System.Collections.Concurrent;
using System.Reflection;

namespace Net.Myzuc.DivaMC.Common.IO.Protocol.Packets
{
    public record Packet
    {
        protected delegate Packet Deserializer(Stream stream, object registry);
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

        public PacketSignature Signature
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
        protected virtual void Serialize(Stream stream, object registry)
        {
            
        }
        
        public static Packet Deserialize(PacketSignature signature, Stream stream, object registry)
        {
            if (!Deserializers.TryGetValue(signature, out Deserializer? deserializer))
            {
                throw new InvalidDataException("Unknown packet signature!");
            }
            Packet packet = deserializer(stream, registry);
            return packet;
        }
        public static void Serialize(Packet packet, Stream stream, object registry)
        {
            packet.Serialize(stream, registry);
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