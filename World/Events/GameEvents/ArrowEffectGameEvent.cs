namespace DivaMC.Protocol.World.Events.GameEvents
{
    public sealed record ArrowEffectGameEvent : GameEvent
    {
        protected override byte Type => 6;

        public ArrowEffectGameEvent()
        {
            
        }
    }
}