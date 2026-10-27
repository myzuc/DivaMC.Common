using DivaMC.Protocol.Common.Enums;

namespace DivaMC.Protocol.World.Events.GameEvents
{
    public sealed record GamemodeGameEvent : GameEvent
    {
        protected override byte Type => 3;

        public Gamemode Gamemode
        {
            get
            {
                return (Gamemode)(sbyte)RawValue;
            }
            set
            {
                RawValue = (float)(sbyte)value;
            }
        }

        public GamemodeGameEvent()
        {
            
        }
    }
}