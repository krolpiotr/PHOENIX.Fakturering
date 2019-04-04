using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using System.Web;

namespace PHOENIX.Fakturering
{
    public partial class CompanyForm : Form
    {
        public CompanyForm()
        {
            InitializeComponent();
        }

        public string foretagnamn;
        public string varreferens;
        public string fmomsregnr;
        public string forgnr;
        public string fepost;
        public string fontsize;
        public string adress1;
        public string adress2;
        public string adress3;
        public string telefon;
        public string fbankgiro;
        public string fwebbsida;
        public string betalningsvillkor;
        public string drojsmalsranta;

        private void CompanyForm_Load(object sender, EventArgs e)
        {
            label1.Text = "Företag namn:";
            label2.Text = "Vår referens:";
            label3.Text = "Org. nummer:";
            label4.Text = "Momsreg. nr:";
            label5.Text = "BankGiro nr:";
            label6.Text = "Telefon:";
            label7.Text = "Webbsida:";
            label8.Text = "E-post:";
            label9.Text = "Betalningsvillkor:";
            label10.Text = "Dröjsmålsränta:";
            label11.Text = "Textstorlek:";

            string path = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\data\foretag.xml";

            XDocument xmldoc = XDocument.Load(path);

            var items = (from i in xmldoc.Descendants("foretag")
                         select new
                         {
                             foretagnamn = i.Element("foretagnamn").Value,
                             varreferens = i.Element("varreferens").Value,

                             fmomsregnr = i.Element("fmomsregnr").Value,
                             forgnr = i.Element("forgnr").Value,
                             fepost = i.Element("fepost").Value,
                             fontsize = i.Element("fontsize").Value,
                             adress1 = i.Element("adress1").Value,
                             adress2 = i.Element("adress2").Value,
                             adress3 = i.Element("adress3").Value,
                             telefon = i.Element("telefon").Value,
                             fbankgiro = i.Element("fbankgiro").Value,
                             fwebbsida = i.Element("fwebbsida").Value,
                             betalningsvillkor = i.Element("betalningsvillkor").Value,
                             drojsmalsranta = i.Element("drojsmalsranta").Value
                         }).ToList();

            foreach (var item in items)
            {
                this.foretagnamn = item.foretagnamn;
                this.varreferens = item.varreferens;

                this.fmomsregnr = item.fmomsregnr;
                this.forgnr = item.forgnr;
                this.fepost = item.fepost;
                this.fontsize = item.fontsize;
                this.adress1 = item.adress1;
                this.adress2 = item.adress2;
                this.adress3 = item.adress3;
                this.telefon = item.telefon;
                this.fbankgiro = item.fbankgiro;
                this.fwebbsida = item.fwebbsida;
                this.betalningsvillkor = item.betalningsvillkor;
                this.drojsmalsranta = item.drojsmalsranta;
            }

            tbForetagName.Text = this.foretagnamn;
            tbVarRef.Text = this.varreferens;
            tbVATnr.Text = this.fmomsregnr;
            tbOrgNr.Text = this.forgnr;
            tbEpost.Text = this.fepost;
            tbFontSize.Text = this.fontsize;
            tbAddress1.Text = this.adress1;
            tbAddress2.Text = this.adress2;
            tbAddress3.Text = this.adress3;
            tbTelefon.Text = this.telefon;
            tbBankGiro.Text = this.fbankgiro;
            tbWebbsida.Text = this.fwebbsida;
            tbBetalningsvillkor.Text = this.betalningsvillkor;
            tbDrojsmalsranta.Text = this.drojsmalsranta;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string path = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\data\foretag.xml";

            XDocument xmlFile = XDocument.Load(path);
            var query = from c in xmlFile.Elements("info").Elements("foretag")
                        select c;
            foreach (XElement foretag in query)
            {
                foretag.Element("foretagnamn").Value = tbForetagName.Text;
                foretag.Element("varreferens").Value = tbVarRef.Text;
                foretag.Element("forgnr").Value = tbOrgNr.Text;
                foretag.Element("fmomsregnr").Value = tbVATnr.Text;
                foretag.Element("fepost").Value = tbEpost.Text;
                foretag.Element("fontsize").Value = tbFontSize.Text;
                foretag.Element("adress1").Value = tbAddress1.Text;
                foretag.Element("adress2").Value = tbAddress2.Text;
                foretag.Element("adress3").Value = tbAddress3.Text;
                foretag.Element("telefon").Value = tbTelefon.Text;
                foretag.Element("fbankgiro").Value = tbBankGiro.Text;
                foretag.Element("fwebbsida").Value = tbWebbsida.Text;
                foretag.Element("betalningsvillkor").Value = tbBetalningsvillkor.Text;
                foretag.Element("drojsmalsranta").Value = tbDrojsmalsranta.Text;
            }
            xmlFile.Save(path);
        }
    }
}
