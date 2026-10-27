namespace DivaMC.Common.World.Events.GameEvents
{
    public sealed record GameEndGameEvent : GameEvent
    {
        protected override byte Type => 4;

        public bool PlayCredits
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

        public GameEndGameEvent()
        {
            
        }
    }
}