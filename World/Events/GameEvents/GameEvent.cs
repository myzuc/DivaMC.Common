using DivaMC.Protocol.IO.Serialization;

namespace DivaMC.Protocol.World.Events.GameEvents
{
    public abstract record GameEvent : IByteSerializable<GameEvent>
    {
        protected abstract byte Type { get; }
        protected float RawValue { get; set; }
        
        protected internal GameEvent()
        {
            
        }
        
        public void Serialize(Stream stream)
        {
            stream.WriteU8(Type);
            stream.WriteF32(RawValue);
        }
        public static GameEvent Deserialize(Stream stream)
        {
            byte type = stream.ReadU8();
            GameEvent data = type switch
            {
                0 => new RespawnFailureGameEvent(),
                1 => new RainStartGameEvent(),
                2 => new RainEndGameEvent(),
                3 => new GamemodeGameEvent(),
                4 => new GameEndGameEvent(),
                5 => new DemoGameEvent(),
                6 => new ArrowEffectGameEvent(),
                7 => new RainStartGameEvent(),
                8 => new ThunderLevelGameEvent(),
                9 => new PufferfishEffectGameEvent(),
                10 => new ElderGuardianEffectGameEvent(),
                11 => new ImmediateRespawnGameruleGameEvent(),
                12 => new LimitedCraftingGameruleGameEvent(),
                13 => new ReceiveChunksGameEvent(),
                _ => throw new InvalidDataException(),
            };
            data.RawValue = stream.ReadF32();
            return data;
        }
    }
}