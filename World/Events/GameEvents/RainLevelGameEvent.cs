namespace DivaMC.Protocol.World.Events.GameEvents
{
    public sealed record RainLevelGameEvent : GameEvent
    {
        protected override byte Type => 7;

        public float Strength
        {
            get
            {
                return RawValue;
            }
            set
            {
                RawValue = value;
            }
        }
        
        public RainLevelGameEvent()
        {
            
        }
    }
}