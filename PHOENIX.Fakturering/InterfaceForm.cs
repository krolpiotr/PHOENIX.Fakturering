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

        public double tax;
        public string kindOfFaktura;

        private void InterfaceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Vill du verkligen stänga programmet?", "Stäng programmet", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes)
            {
                System.Environment.Exit(1);
            }
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
            this.firstRun();

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

        private void BtnHistory_Click(object sender, EventArgs e)
        {
            HistoryForm hForm = new HistoryForm();
            hForm.ShowDialog();
        }

        private void BtnNewInvoice_Click(object sender, EventArgs e)
        {
            if (rd0.Checked == true)
            {
                this.tax = 0;
            }
            else if (rd6.Checked == true)
            {
                this.tax = 0.06;
            }
            else if (rd12.Checked == true)
            {
                this.tax = 0.12;
            }
            else if (rd25.Checked == true)
            {
                this.tax = 0.25;
            }

            if (rbFaktura.Checked == true)
            {
                this.kindOfFaktura = "FAKTURA";
            }
            else if (rbFAconto.Checked == true)
            {
                this.kindOfFaktura = "ACONTO";
            }

            NewInvoiceForm sForm = new NewInvoiceForm(customersBox.SelectedValue.ToString(), this.tax, this.kindOfFaktura);
            sForm.ShowDialog();
        }

        public void firstRun()
        {
            this.fakturorDir = fakturorLocation();
            this.dataDir = dataLocation();
            this.produkterDir = produkterLocation();
            this.companyFile();
            this.customersFile();
            this.invoicesFile();
            this.productsFile();
            this.typesFile();
            this.acontoFile();
            this.tableImage();
            this.logoImage();
            return;
        }

        public void tableImage()
        {
            if (!File.Exists(this.dataDir + "table.png"))
            {
                var bmp = new Bitmap(PHOENIX.Fakturering.Properties.Resources.table);
                bmp.Save(dataDir + "table.png", System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        public void logoImage()
        {
            if (!File.Exists(this.dataDir + "logo.png"))
            {
                var bmp = new Bitmap(PHOENIX.Fakturering.Properties.Resources.logo);
                bmp.Save(dataDir + "logo.png", System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        public void companyFile()
        {
            if (!File.Exists(this.dataDir + "foretag.xml"))
            {
                System.Xml.XmlDocument newDoc = new System.Xml.XmlDocument();

                string sTextX = "<info>";
                sTextX += "<foretag>";
                sTextX += "<foretagnamn>Exempel Foretag2</foretagnamn>";
                sTextX += "<fontsize>26</fontsize>";
                sTextX += "<varreferens>Johan Andersson</varreferens>";
                sTextX += "<fmomsregnr>6103546554</fmomsregnr>";
                sTextX += "<forgnr>61034-5656</forgnr>";
                sTextX += "<fepost>exempel@gmail.com</fepost>";
                sTextX += "<adress1>Exempel Foretag2</adress1>";
                sTextX += "<adress2>Högsätravägen 75,</adress2>";
                sTextX += "<adress3>181 58 Lidingö</adress3>";
                sTextX += "<telefon>0739677677</telefon>";
                sTextX += "<fbankgiro>6209-03</fbankgiro>";
                sTextX += "<fwebbsida></fwebbsida>";
                sTextX += "<betalningsvillkor>30</betalningsvillkor>";
                sTextX += "<drojsmalsranta>11,00%</drojsmalsranta>";
                sTextX += "</foretag>";
                sTextX += "</info>";
                newDoc.LoadXml(sTextX);
                newDoc.PrependChild(newDoc.CreateXmlDeclaration("1.0", "utf-8", ""));
                newDoc.Save(this.dataDir + @"foretag.xml");
            }
        }

        public void customersFile()
        {
            if (!File.Exists(this.dataDir + "kunder.xml"))
            {
                System.Xml.XmlDocument newDoc = new System.Xml.XmlDocument();

                string sTextX = "<customers>";
                sTextX += "<kund>";
                sTextX += "<namn>Kund Foretag 1</namn>";
                sTextX += "<er>Max Hansson</er>";
                sTextX += "<kundnr>001</kundnr>";
                sTextX += "<momsregnr>SE59010434343</momsregnr>";
                sTextX += "<fakturaadress1>Nygatan 89,</fakturaadress1>";
                sTextX += "<fakturaadress2>145 59 Johaneshov</fakturaadress2>";
                sTextX += "<fakturaadress3></fakturaadress3>";
                sTextX += "<fakturaemail>some1@sdd.se</fakturaemail>";
                sTextX += "</kund>";
                sTextX += "<kund>";
                sTextX += "<namn>Mikud AB</namn>";
                sTextX += "<er>Anders Johansson</er>";
                sTextX += "<kundnr>002</kundnr>";
                sTextX += "<momsregnr>SE55345453534</momsregnr>";
                sTextX += "<fakturaadress1>Gamlagatan 67,</fakturaadress1>";
                sTextX += "<fakturaadress2>145 59 Stockholm</fakturaadress2>";
                sTextX += "<fakturaadress3></fakturaadress3>";
                sTextX += "<fakturaemail>some223@sdd.se</fakturaemail>";
                sTextX += "</kund>";
                sTextX += "</customers>";

                newDoc.LoadXml(sTextX);
                newDoc.PrependChild(newDoc.CreateXmlDeclaration("1.0", "utf-8", ""));
                newDoc.Save(this.dataDir + @"kunder.xml");
            }
        }

        public void invoicesFile()
        {
            if (!File.Exists(this.dataDir + "fakturor.xml"))
            {
                System.Xml.XmlDocument newDoc = new System.Xml.XmlDocument();

                string sTextX = "<fakturor>";
                sTextX += "</fakturor>";

                newDoc.LoadXml(sTextX);
                newDoc.PrependChild(newDoc.CreateXmlDeclaration("1.0", "utf-8", ""));
                newDoc.Save(this.dataDir + @"fakturor.xml");
            }
        }

        public void productsFile()
        {
            if (!File.Exists(this.dataDir + "produkter.xml"))
            {
                System.Xml.XmlDocument newDoc = new System.Xml.XmlDocument();

                string sTextX = "<produkter>";
                sTextX += "<produkt>";
                sTextX += "<pnamn></pnamn>";
                sTextX += "<pris></pris>";
                sTextX += "</produkt>";
                sTextX += "<produkt>";
                sTextX += "<pnamn>AL LAMELL 50 mm 6 m²</pnamn>";
                sTextX += "<pris>87</pris>";
                sTextX += "</produkt>";
                sTextX += "<produkt>";
                sTextX += "<pnamn>AL LAMELL 30 mm 9,6 m²</pnamn>";
                sTextX += "<pris>59</pris>";
                sTextX += "</produkt>";
                sTextX += "<produkt>";
                sTextX += "<pnamn>AL TAPE ARMERAD 75 mm 45,7 m</pnamn>";
                sTextX += "<pris>267</pris>";
                sTextX += "</produkt>";
                sTextX += "<produkt>";
                sTextX += "<pnamn>ALU TAPE GF 50 MMX 25 m</pnamn>";
                sTextX += "<pris>135</pris>";
                sTextX += "</produkt>";
                sTextX += "<produkt>";
                sTextX += "<pnamn>ALU TAPE GF 70 MMX 25 m</pnamn>";
                sTextX += "<pris>159</pris>";
                sTextX += "</produkt>";
                sTextX += "<produkt>";
                sTextX += "<pnamn>AGM STIFT 29 mm 500 st</pnamn>";
                sTextX += "<pris>1772</pris>";
                sTextX += "</produkt>";
                sTextX += "</produkter>";

                newDoc.LoadXml(sTextX);
                newDoc.PrependChild(newDoc.CreateXmlDeclaration("1.0", "utf-8", ""));
                newDoc.Save(this.dataDir + @"produkter.xml");
            }
        }

        public void typesFile()
        {
            if (!File.Exists(this.dataDir + "types.xml"))
            {
                System.Xml.XmlDocument newDoc = new System.Xml.XmlDocument();

                string sTextX = "<types>";
                sTextX += "<type>";
                sTextX += "<tnamn></tnamn>";
                sTextX += "</type>";
                sTextX += "<type>";
                sTextX += "<tnamn>Timme</tnamn>";
                sTextX += "</type>";
                sTextX += "<type>";
                sTextX += "<tnamn>Timmar</tnamn>";
                sTextX += "</type>";
                sTextX += "<type>";
                sTextX += "<tnamn>Styck</tnamn>";
                sTextX += "</type>";
                sTextX += "<type>";
                sTextX += "<tnamn>m²</tnamn>";
                sTextX += "</type>";
                sTextX += "<type>";
                sTextX += "<tnamn>Kartong</tnamn>";
                sTextX += "</type>";
                sTextX += "<type>";
                sTextX += "<tnamn>Enhet</tnamn>";
                sTextX += "</type>";
                sTextX += "<type>";
                sTextX += "<tnamn>Enheter</tnamn>";
                sTextX += "</type>";
                sTextX += "<type>";
                sTextX += "<tnamn>Kilo</tnamn>";
                sTextX += "</type>";
                sTextX += "</types>";

                newDoc.LoadXml(sTextX);
                newDoc.PrependChild(newDoc.CreateXmlDeclaration("1.0", "utf-8", ""));
                newDoc.Save(this.dataDir + @"types.xml");
            }
        }

        public void acontoFile()
        {
            if (File.Exists(this.dataDir + "aconto.xml"))
            {
                System.Xml.XmlDocument newDoc = new System.Xml.XmlDocument();

                string sTextX = "<produkter>";
                sTextX += "<produkt>";
                sTextX += "<pnamn></pnamn>";
                sTextX += "<pris></pris>";
                sTextX += "</produkt>";
                sTextX += "<produkt>";
                sTextX += "<pnamn>A-conto 1:</pnamn>";
                sTextX += "<pris></pris>";
                sTextX += "</produkt>";
                sTextX += "<produkt>";
                sTextX += "<pnamn>Anbudssumma</pnamn>";
                sTextX += "<pris></pris>";
                sTextX += "</produkt>";
                sTextX += "<produkt>";
                sTextX += "<pnamn>Kvar att fakturera</pnamn>";
                sTextX += "<pris></pris>";
                sTextX += "</produkt>";
                sTextX += "<produkt>";
                sTextX += "<pnamn>Faktura</pnamn>";
                sTextX += "<pris></pris>";
                sTextX += "</produkt>";
                sTextX += "</produkter>";

                newDoc.LoadXml(sTextX);
                newDoc.PrependChild(newDoc.CreateXmlDeclaration("1.0", "utf-8", ""));
                newDoc.Save(this.dataDir + @"aconto.xml");
            }
        }

    }
}
