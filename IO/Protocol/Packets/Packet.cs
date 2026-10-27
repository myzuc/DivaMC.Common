using System.Collections.Concurrent;
using System.Reflection;
using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets
{
    public abstract class Packet : IBinarySerializable<Packet, PacketContext>
    {
        protected delegate Packet Deserializer(Stream stream, PacketContext context);
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

        public virtual void Serialize(Stream stream, PacketContext context)
        {
            context.Check(Signature);
            stream.WriteVS32(Signature.Id);
        }
        public static Packet Deserialize(Stream stream, PacketContext context)
        {
            int id = stream.ReadVS32();
            PacketSignature signature = new(context.ProtocolDirection, context.ProtocolStage, id);
            if (!Deserializers.TryGetValue(signature, out Deserializer? deserializer))
            {
                return new UnknownPacket(signature, stream.ReadU8A());
            }
            Packet packet = deserializer(stream, context);
            return packet;
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