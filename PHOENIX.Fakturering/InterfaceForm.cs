using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PHOENIX.Fakturering
{
    public partial class InterfaceForm : Form
    {
        public InterfaceForm()
        {
            InitializeComponent();
        }

        private void InterfaceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Vill du verkligen stänga programmet?", "Stäng programmet", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes)
            {
                System.Environment.Exit(1);
            }
        }

        private void ArkivToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void CountWordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WordsCountingForm frForm = new WordsCountingForm();
            frForm.ShowDialog();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm sdForm = new AboutForm();
            sdForm.ShowDialog();
        }
    }
}
