using System.Collections.Generic;

namespace SOS
{
    class Track
    {
        public List<MIDIMessage> e = new List<MIDIMessage>();
        public Soundbank instrument;
        public Track(Soundbank inst)
        {
            instrument = inst;
        }
    }
}
