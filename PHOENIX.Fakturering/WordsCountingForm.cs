using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pxlib;

namespace PHOENIX.Fakturering
{
    public partial class WordsCountingForm : Form
    {
        public WordsCountingForm()
        {
            InitializeComponent();
        }

        private void BtnCount_Click(object sender, EventArgs e)
        {
            int iWords = Functions.CountWords(richTextBoxWords.Text);
            MessageBox.Show(iWords + " ord", "Räkna ord", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void WordsCountingForm_Load(object sender, EventArgs e)
        {
            btnCount.Text = "Räkna";
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxWords.Text = Clipboard.GetText();
        }
    }
}
