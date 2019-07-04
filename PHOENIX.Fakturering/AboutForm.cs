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
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        public string GetFrameWorkVersion()
        {
            return System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            label1.Text = "Författare: Piotr Krol";
            label2.Text = "Programmets namn: PHOENIX Fakturering";
            label3.Text = "Kontakt:";
            label4.Text = "Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            label5.Text = "Enviroment: " + GetFrameWorkVersion();
            label6.Text = "Webbsida:";

            Font font = new Font("Tahoma", 9, FontStyle.Regular);
            Font font2 = new Font("Tahoma", 9, FontStyle.Bold);
            richTextBox1.SelectionFont = font;
            richTextBox1.SelectionColor = Color.Red;
            richTextBox1.SelectedText = Environment.NewLine + richTextBox1.Text;

            richTextBox1.Text += "\nMIT License."; 
            richTextBox1.SelectionFont = font2;
            richTextBox1.SelectedText += "Programmet är gratis för privatpersoner.";
            richTextBox1.SelectionFont = font;
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://fakturering.simon-phoenix.se");
        }

        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://simon-phoenix.se");
        }
    }
}
