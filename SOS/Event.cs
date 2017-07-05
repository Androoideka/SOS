namespace SOS
{
    class Event
    {
        protected int deltaTime;
        public byte eventType;
        public Event(byte et, int deltatime)
        {
            deltaTime = deltatime;
            eventType = et;
        }
        public int getDT(int interval)
        {
            return deltaTime * interval;
        }
    }
}