namespace DivaMC.Protocol.World.Events.GameEvents
{
    public sealed record PufferfishEffectGameEvent : GameEvent
    {
        protected override byte Type => 9;

        public PufferfishEffectGameEvent()
        {
            
        }
    }
}