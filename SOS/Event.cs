namespace SOS
{
    class Event
    {
        protected int deltaTime;
        public byte eventType;
        public int getDT(int interval)
        {
            return deltaTime * interval;
        }
    }
}
