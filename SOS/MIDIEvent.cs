namespace SOS
{
    class MIDIEvent : Event
    {
        internal readonly byte velocity;
        public readonly byte note;
        public MIDIEvent(byte vel, byte no, int deltatime)
        {
            deltaTime = deltatime;
            eventType = 0;
            velocity = vel;
            note = no;
        }
        public float ChangeVolume()
        {
            return 1f / 127f * velocity;
        }
    }
}
