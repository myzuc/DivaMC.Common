namespace DivaMC.Common.World.Events.GameEvents
{
    public sealed record DemoGameEvent : GameEvent
    {
        public enum DemoMessage : int
        {
            Welcome = 0,
            Movement = 101,
            Jump = 102,
            Inventory = 103,
            End = 104,
        }
        
        protected override byte Type => 5;

        public DemoMessage Message
        {
            get
            {
                return (DemoMessage)(int)RawValue;
            }
            set
            {
                RawValue = (float)(int)value;
            }
        }

        public DemoGameEvent()
        {
            
        }
    }
}