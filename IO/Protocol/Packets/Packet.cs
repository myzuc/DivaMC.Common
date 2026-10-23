using System.Collections.Concurrent;
using System.Net;
using System.Reflection;

namespace Net.Myzuc.DivaMC.Common.IO.Protocol.Packets
{
    public static class Packet
    {
        private delegate IPacket Deserializer(Stream stream, object registry);
        private static ConcurrentDictionary<PacketSignature, Deserializer> Deserializers = [];

        static Packet()
        {
            IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsAssignableTo(typeof(Packet)) && !t.IsAbstract);
            foreach (Type type in types)
            {
                System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(type.TypeHandle);
            }
        }
        
        public static IPacket Deserialize(PacketSignature signature, Stream stream, object registry)
        {
            if (!Deserializers.TryGetValue(signature, out Deserializer? deserializer))
            {
                throw new ProtocolViolationException("Unknown packet signature!");
            }
            IPacket packet = deserializer(stream, registry);
            return packet;
        }
        public static void Serialize(IPacket packet, Stream stream, object registry)
        {
            packet.Serialize(stream, registry);
        }

        internal static void Register<TPacket>() where TPacket : IPacket
        {
            if (!Deserializers.TryAdd(TPacket.Signature, TPacket.Deserialize))
            {
                throw new InvalidOperationException("Duplicate packet signature!");
            }
        }
    }
}