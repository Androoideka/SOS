using System;
using System.Drawing;
using System.Windows.Forms;

namespace SOS
{
    public partial class Sequencer : Form
    {
        int n;
        Channel[] ch;
        ComboBox[] chInst;
        static Instrument[] instrumenti;
        static int length;
        public Sequencer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chInst[n] = new ComboBox();
            chInst[n].DropDownStyle = ComboBoxStyle.DropDownList;
            for (int i = 0; i < length; i++)
            {
                chInst[n].Items.Add(instrumenti[i].ime);
            }
            chInst[n].Top = 12 + n * (12 + button1.Size.Height);
            chInst[n].Tag = n;
            chInst[n].Enabled = true;
            Controls.Add(chInst[n]);
            chInst[n].SelectedIndexChanged += new EventHandler(SelectInstrument);
            ch[n] = new Channel();
            n++;
            button1.Top += button1.Size.Height + 12;
            Refresh();
        }

        private void Sequencer_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int i = 0; i < n; i++)
            {
                g.DrawLine(new Pen(Color.Black), 0, chInst[i].Top, ClientRectangle.Width, chInst[i].Top);
                g.DrawLine(new Pen(Color.Black), 0, chInst[i].Top + button1.Size.Height, ClientRectangle.Width, chInst[i].Top + button1.Size.Height);
            }
        }

        private void Sequencer_Load(object sender, EventArgs e)
        {
            ch = new Channel[16];
            chInst = new ComboBox[16];
            instrumenti = new Instrument[128];
            instrumenti[0] = new Instrument("Piano", false);
            length++;
        }
        private void SelectInstrument(object sender, EventArgs e)
        {
            for (int i = 0; i < length; i++)
            {
                if ((sender as ComboBox).Text == instrumenti[i].ime)
                    ch[Convert.ToInt32((sender as ComboBox).Tag)].inst = instrumenti[i];
            }
            ch[Convert.ToInt32((sender as ComboBox).Tag)].CreateNewNotes();
            if(
        }
    }
}
