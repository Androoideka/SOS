namespace SOS
{
    class MIDIMessage
    {
        public int deltaTime;
        public byte velocity;
        public byte note;
        public MIDIMessage(byte vel, byte no, int deltatime)
        {
            deltaTime = deltatime;
            velocity = vel;
            note = no;
        }
    }
}
