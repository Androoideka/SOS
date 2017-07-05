namespace SOS
{
    class PCEvent : Event
    {
        public readonly byte patch;
        public PCEvent(byte i, int deltatime) : base(1, deltatime)
        {
            patch = i;
        }
    }
}
