namespace SOS
{
    class Event
    {
        protected byte deltaTime;
        public byte eventType;
        public int getDT(int interval)
        {
            return deltaTime * interval;
        }
    }
}
