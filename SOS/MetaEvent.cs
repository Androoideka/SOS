namespace SOS
{
    class MetaEvent : Event
    {
        public readonly byte patch;
        public MetaEvent(byte i, byte deltatime)
        {
            deltaTime = deltatime;
            eventType = 255;
            patch = i;
        }
    }
}
