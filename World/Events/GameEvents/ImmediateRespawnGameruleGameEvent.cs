namespace DivaMC.Common.World.Events.GameEvents
{
    public sealed record ImmediateRespawnGameruleGameEvent : GameEvent
    {
        protected override byte Type => 11;

        public bool Enabled
        {
            get
            {
                return RawValue != 0.0f;
            }
            set
            {
                RawValue = value ? 1.0f : 0.0f;
            }
        }

        public ImmediateRespawnGameruleGameEvent()
        {
            
        }
    }
}