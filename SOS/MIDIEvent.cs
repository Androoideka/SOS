namespace SOS
{
    class MIDIEvent : Event
    {
        public readonly byte velocity;
        public readonly byte note;
        public MIDIEvent(byte vel, byte no, byte deltatime)
        {
            deltaTime = deltatime;
            eventType = 0;
            velocity = vel;
            note = no;
        }
    }
}
