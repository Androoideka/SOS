using System;
using System.Windows.Forms;

namespace SOS
{
    public partial class Sequencer : Form
    {
        Projekt prj = new Projekt();
        int n;
        ComboBox[] chInst;
        DataGridView[] dgv;
        public Sequencer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (n < 16)
            {
                chInst[n] = new ComboBox();
                chInst[n].DropDownStyle = ComboBoxStyle.DropDownList;
                chInst[n].Items.AddRange(prj.FindInstruments(null));
                chInst[n].Top = 12 + n * (12 + button1.Size.Height);
                chInst[n].Tag = n;
                chInst[n].Enabled = true;
                Controls.Add(chInst[n]);
                n++;
                button1.Top += button1.Size.Height + 12;
            }
        }

        private void Sequencer_Load(object sender, EventArgs e)
        {
            chInst = new ComboBox[16];
            dgv = new DataGridView[16];
        }
        private void SelectInstrument(object sender, EventArgs e)
        {
            prj.InitTrk(Convert.ToInt32((sender as ComboBox).Tag), (sender as ComboBox).Text);
            (sender as ComboBox).Items.Clear();
            (sender as ComboBox).Items.AddRange();  
        }
    }
}
