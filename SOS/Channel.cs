namespace SOS
{
    class Channel
    {
        public Note[] note;
        public Instrument inst;
        public void CreateNewNotes()
        {
            if (!inst.percussion)
            {
                note = new Note[128];
                for (int i = 0; i < 128; i++)
                    note[i] = new Note(i);
            }
            else
            {
                note = new Note[1];
                note[0] = new Note(60);
            }
        }
    }
}
