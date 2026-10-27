namespace DivaMC.Common.World.Events.GameEvents
{
    public sealed record ThunderLevelGameEvent : GameEvent
    {
        protected override byte Type => 8;

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
        
        public ThunderLevelGameEvent()
        {
            
        }
    }
}