namespace SOS
{
    class PCEvent : Event
    {
        public readonly byte patch;
        public PCEvent(byte i, byte deltatime)
        {
            deltaTime = deltatime;
            eventType = 255;
            patch = i;
        }
    }
}