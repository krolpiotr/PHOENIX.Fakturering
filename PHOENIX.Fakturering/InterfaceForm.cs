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
    public partial class InterfaceForm : Form
    {
        public InterfaceForm()
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

        public string fakturorDir;
        public string dataDir;
        public string produkterDir;

        private void InterfaceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Vill du verkligen stänga programmet?", "Stäng programmet", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes)
            {
                System.Environment.Exit(1);
            }
        }

       // private void ArkivToolStripMenuItem_Click(object sender, EventArgs e)
      // {

      //  }

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

        private void BtnMyCompany_Click(object sender, EventArgs e)
        {
            CompanyForm cyForm = new CompanyForm();
            cyForm.ShowDialog();
            this.autoloadCompany();
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Är du säker att du vill ta bort kunden \n\n\t" + customersBox.Text + "\n\nfrån kunddatabasen?", "Obs!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string filename = this.dataDir + @"kunder.xml";
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(filename);

                System.Xml.XmlNode deleteContact =
                   doc.SelectSingleNode("descendant::kund[kundnr='" + customersBox.SelectedValue.ToString() + "']");
                //Remove the XmlNode from the Document
                doc.DocumentElement.RemoveChild(deleteContact);
                //Save the Document
                doc.Save(filename);
                this.autoloadCustomers();
            }
        }

        public string dataLocation()
        {
            string execDir = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            DirectoryInfo df = new DirectoryInfo(execDir + @"\data\");

            if (!df.Exists)
            {
                // create new directory
                DirectoryInfo di = Directory.CreateDirectory(execDir + @"\data\");

            }
            return execDir + @"\data\";
        }

        public string fakturorLocation()
        {
            string execDir = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            DirectoryInfo df = new DirectoryInfo(execDir + @"\fakturor\");

            if (!df.Exists)
            {
                // create new directory
                DirectoryInfo di = Directory.CreateDirectory(execDir + @"\fakturor\");
            }
            return execDir + @"\fakturor\";
        }

        public string produkterLocation()
        {
            string execDir = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            DirectoryInfo df = new DirectoryInfo(execDir + @"\produkter\");

            if (!df.Exists)
            {
                // create new directory
                DirectoryInfo di = Directory.CreateDirectory(execDir + @"\produkter\");

            }
            return execDir + @"\produkter\";
        }

        public void autoloadCustomers()
        {
            XDocument xmldoc = XDocument.Load(this.dataDir + "kunder.xml");
            var items = (from i in xmldoc.Descendants("kund") select new { namn = i.Element("namn").Value, kundnr = i.Element("kundnr").Value }).ToList();

            customersBox.DataSource = items;
            customersBox.DisplayMember = "namn";
            customersBox.ValueMember = "kundnr";
        }

        public void autoloadCompany()
        {
            XDocument xmldoc_info = XDocument.Load(this.dataDir + "foretag.xml");
            var items_info = (from i in xmldoc_info.Descendants("foretag")
                              select new
                              {
                                  foretagnamn = i.Element("foretagnamn").Value,
                                  fontsize = i.Element("fontsize").Value,
                                  telefon = i.Element("telefon").Value,
                                  fepost = i.Element("fepost").Value,
                                  fmomsregnr = i.Element("fmomsregnr").Value,
                                  forgnr = i.Element("forgnr").Value,
                                  fwebbsida = i.Element("fwebbsida").Value,
                                  fbankgiro = i.Element("fbankgiro").Value,
                                  betalningsvillkor = i.Element("betalningsvillkor").Value,
                                  drojsmalsranta = i.Element("drojsmalsranta").Value,
                                  varreferens = i.Element("varreferens").Value,
                                  adress1 = i.Element("adress1").Value,
                                  adress2 = i.Element("adress2").Value,
                                  adress3 = i.Element("adress3").Value
                              }).ToList();

            foreach (var item_info in items_info) /* var because is an anonymous type */
            {
                this.foretagnamn = item_info.foretagnamn;
                this.fontsize = item_info.fontsize;
                this.fepost = item_info.fepost;
                this.fmomsregnr = item_info.fmomsregnr;
                this.forgnr = item_info.forgnr;
                this.fwebbsida = item_info.fwebbsida;
                this.fbankgiro = item_info.fbankgiro;
                this.varreferens = item_info.varreferens;
                this.betalningsvillkor = item_info.betalningsvillkor;
                this.drojsmalsranta = item_info.drojsmalsranta;
                this.telefon = item_info.telefon;
                this.adress1 = item_info.adress1;
                this.adress2 = item_info.adress2;
                this.adress3 = item_info.adress3;
            }
            this.Text = "" + this.foretagnamn;
        }

        private void InterfaceForm_Load(object sender, EventArgs e)
        {
            this.dataDir = this.dataLocation();
            this.fakturorDir = this.fakturorLocation();
            this.produkterDir = this.produkterLocation();

            rd25.Checked = true;
            rbFaktura.Checked = true;

            // customers
            XDocument xmldoc = XDocument.Load(this.dataDir + "kunder.xml");
            var items = (from i in xmldoc.Descendants("kund") select new { namn = i.Element("namn").Value, kundnr = i.Element("kundnr").Value }).ToList();

            customersBox.DataSource = items;
            customersBox.DisplayMember = "namn";
            customersBox.ValueMember = "kundnr";

            // company
            XDocument xmldoc_info = XDocument.Load(this.dataDir + "foretag.xml");
            var items_info = (from i in xmldoc_info.Descendants("foretag")
                              select new
                              {
                                  foretagnamn = i.Element("foretagnamn").Value,
                                  fontsize = i.Element("fontsize").Value,
                                  telefon = i.Element("telefon").Value,
                                  fepost = i.Element("fepost").Value,
                                  fmomsregnr = i.Element("fmomsregnr").Value,
                                  forgnr = i.Element("forgnr").Value,
                                  fwebbsida = i.Element("fwebbsida").Value,
                                  fbankgiro = i.Element("fbankgiro").Value,
                                  betalningsvillkor = i.Element("betalningsvillkor").Value,
                                  drojsmalsranta = i.Element("drojsmalsranta").Value,
                                  varreferens = i.Element("varreferens").Value,
                                  adress1 = i.Element("adress1").Value,
                                  adress2 = i.Element("adress2").Value,
                                  adress3 = i.Element("adress3").Value
                              }).ToList();

            foreach (var item_info in items_info)
            {
                this.foretagnamn = item_info.foretagnamn;
                this.fontsize = item_info.fontsize;
                this.fepost = item_info.fepost;
                this.fmomsregnr = item_info.fmomsregnr;
                this.forgnr = item_info.forgnr;
                this.fwebbsida = item_info.fwebbsida;
                this.fbankgiro = item_info.fbankgiro;
                this.varreferens = item_info.varreferens;
                this.betalningsvillkor = item_info.betalningsvillkor;
                this.drojsmalsranta = item_info.drojsmalsranta;
                this.telefon = item_info.telefon;
                this.adress1 = item_info.adress1;
                this.adress2 = item_info.adress2;
                this.adress3 = item_info.adress3;
            }

            this.Text = "" + this.foretagnamn;

        }

        private void BtnEditCustomer_Click(object sender, EventArgs e)
        {
            // edit selected customer
            CustomerForm kForm = new CustomerForm(customersBox.SelectedValue.ToString());
            kForm.ShowDialog();
            this.autoloadCustomers();
        }

        private void BtnAddCustomer_Click(object sender, EventArgs e)
        {
            CustomerForm kForm = new CustomerForm("NEW");
            kForm.ShowDialog();
            this.autoloadCustomers();
        }

        private void BtnProducts_Click(object sender, EventArgs e)
        {
            ProductsForm frForm = new ProductsForm();
            frForm.ShowDialog();
        }
    }
}
