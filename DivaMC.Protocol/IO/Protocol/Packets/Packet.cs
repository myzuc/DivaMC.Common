using System.Collections.Concurrent;
using System.Reflection;
using DivaMC.Protocol.IO.Serialization;

namespace DivaMC.Protocol.IO.Protocol.Packets
{
    public abstract class Packet : IByteSerializable<Packet, PacketContext>
    {
        protected delegate Packet Deserializer(Stream stream, PacketContext context);
        private static readonly ConcurrentDictionary<PacketSignature, Deserializer> Deserializers = [];
        private static readonly ConcurrentDictionary<PacketSignature, Type> Signatures = [];

        static Packet()
        {
            IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsAssignableTo(typeof(Packet)) && !t.IsAbstract);
            foreach (Type type in types)
            {
                System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(type.TypeHandle);
            }
        }
        
        protected internal Packet()
        {
            
        }

        public virtual void Serialize(Stream stream, PacketContext context)
        {
            if (this is PreserializedPacket preserializedPacket)
            {
                preserializedPacket.Serialize(stream, context);
                return;
            }
            if (this is UnknownPacket unknownPacket)
            {
                stream.WriteVS32(unknownPacket.Id);
                return;
            }
            IEnumerable<KeyValuePair<PacketSignature, Type>> candidates = Signatures.Where(s => s.Value == this.GetType() && s.Key.Direction == context.ProtocolDirection && s.Key.Stage == context.ProtocolStage);
            PacketSignature? signature = candidates.FirstOrDefault().Key;
            if (!signature.HasValue) throw new InvalidOperationException();
            stream.WriteVS32(signature.Value.Id);
        }
        public static Packet Deserialize(Stream stream, PacketContext context)
        {
            int id = stream.ReadVS32();
            PacketSignature signature = new(context.ProtocolDirection, context.ProtocolStage, id);
            if (!Deserializers.TryGetValue(signature, out Deserializer? deserializer))
            {
                return new UnknownPacket(id, stream.ReadU8A());
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
            if (!Signatures.TryAdd(signature, typeof(TPacket)))
            {
                throw new InvalidOperationException("Duplicate packet signature!");
            }
        }
    }
}