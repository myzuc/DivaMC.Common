namespace DivaMC.Common.World.Events.GameEvents
{
    public sealed record LimitedCraftingGameruleGameEvent : GameEvent
    {
        protected override byte Type => 12;

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

        public LimitedCraftingGameruleGameEvent()
        {
            
        }
    }
}