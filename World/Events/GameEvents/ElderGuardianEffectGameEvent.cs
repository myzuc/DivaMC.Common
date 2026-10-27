namespace DivaMC.Common.World.Events.GameEvents
{
    public sealed record ElderGuardianEffectGameEvent : GameEvent
    {
        protected override byte Type => 10;

        public bool PlaySound
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
        
        public ElderGuardianEffectGameEvent()
        {
            
        }
    }
}