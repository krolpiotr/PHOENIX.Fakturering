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
    public partial class NewInvoiceForm : Form
    {
        public string customerID = "0";
        public double iMOMS = 0;
        public string sKindOfFaktura = "0";

        public NewInvoiceForm(string sTEXT, double sMOMS, string sKindOfFaktura)
        {
            InitializeComponent();
            this.customerID = sTEXT;
            this.iMOMS = sMOMS;
            this.sKindOfFaktura = sKindOfFaktura;
        }

        private void NewInvoiceForm_Load(object sender, EventArgs e)
        {
            this.Text = "Ny Faktura";
        }
    }
}
