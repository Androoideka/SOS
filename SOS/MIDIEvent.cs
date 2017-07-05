namespace SOS
{
    class MIDIEvent : Event
    {
        internal readonly byte velocity;
        public readonly byte note;
        public MIDIEvent(byte vel, byte no, int deltatime) : base(0, deltatime)
        {
            velocity = vel;
            note = no;
        }
        public float GetVolume()
        {
            return 1f / 127f * velocity;
        }
    }
}