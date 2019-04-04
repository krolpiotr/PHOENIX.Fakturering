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
    public partial class CustomerForm : Form
    {
        public string customerID = "0";

        public CustomerForm(string sTEXT)
        {
            InitializeComponent();
            this.customerID = sTEXT;
        }

        public string kundnr;
        public string namn;
        public string er;
        public string momsregnr;
        public string fakturaadress1;
        public string fakturaadress2;
        public string fakturaadress3;
        public string fakturadatum;
        public string forfallodatum;
        public string fakturaemail;

        public string lastKundID;
        public int newkundID;
        public string newKundNummer;

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string path = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\data\kunder.xml";

            if (this.customerID != "NEW") {
                XDocument xmlFile = XDocument.Load(path);
                var query = from c in xmlFile.Elements("customers").Elements("kund")
                            where (string)(c.Element("kundnr")) == this.customerID
                            select c;
                foreach (XElement foretag in query)
                {
                    foretag.Element("namn").Value = tbForetagName.Text;
                    foretag.Element("kundnr").Value = tbKundNr.Text;
                    foretag.Element("er").Value = tbEr.Text;
                    foretag.Element("fakturaadress1").Value = tbAddress1.Text;
                    foretag.Element("fakturaadress2").Value = tbAddress2.Text;
                    foretag.Element("fakturaadress3").Value = tbAddress3.Text;
                    foretag.Element("fakturaemail").Value = tbEmail.Text;
                    foretag.Element("momsregnr").Value = tbMomsRegNr.Text;

                }
                xmlFile.Save(path);
            } else {
                //file name
                string filename = path;

                //create new instance of XmlDocument
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();

                //load from file
                doc.Load(filename);

                //create node and add value
                System.Xml.XmlNode node = doc.CreateNode(System.Xml.XmlNodeType.Element, "kund", null);

                System.Xml.XmlNode nodeNamn = doc.CreateElement("namn");
                nodeNamn.InnerText = tbForetagName.Text;

                System.Xml.XmlNode nodeER = doc.CreateElement("er");
                nodeER.InnerText = tbEr.Text;

                System.Xml.XmlNode nodeKundNR = doc.CreateElement("kundnr");
                nodeKundNR.InnerText = this.newKundNummer.ToString();//tbKundNr.Text;

                System.Xml.XmlNode nodeMomsRegNr = doc.CreateElement("momsregnr");
                nodeMomsRegNr.InnerText = tbMomsRegNr.Text;

                System.Xml.XmlNode nodeAddress1 = doc.CreateElement("fakturaadress1");
                nodeAddress1.InnerText = tbAddress1.Text;
                System.Xml.XmlNode nodeAddress2 = doc.CreateElement("fakturaadress2");
                nodeAddress2.InnerText = tbAddress2.Text;
                System.Xml.XmlNode nodeAddress3 = doc.CreateElement("fakturaadress3");
                nodeAddress3.InnerText = tbAddress3.Text;

                System.Xml.XmlNode nodeEmail = doc.CreateElement("fakturaemail");
                nodeEmail.InnerText = tbEmail.Text;

                //add to parent node
                node.AppendChild(nodeNamn);
                node.AppendChild(nodeER);
                node.AppendChild(nodeKundNR);
                node.AppendChild(nodeMomsRegNr);
                node.AppendChild(nodeAddress1);
                node.AppendChild(nodeAddress2);
                node.AppendChild(nodeAddress3);
                node.AppendChild(nodeEmail);

                //add to elements collection
                doc.DocumentElement.AppendChild(node);

                //save back
                doc.Save(filename);
            }
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            string path = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\data\kunder.xml";

            label1.Text = "Namn:";
            label2.Text = "Er referens:";
            label3.Text = "Kundnr:";
            label4.Text = "Moms-registreringsnummer:";
            label5.Text = "Fakturaadress:";
            label6.Text = "Fakturaadress2:";
            label7.Text = "Fakturaadress3:";
            label8.Text = "E-post:";

            if (this.customerID != "NEW")
            {
                XDocument xmldoc = XDocument.Load(path);
                var items = (from i in xmldoc.Descendants("kund")
                             where (string)(i.Element("kundnr")) == this.customerID
                             select new
                             {
                                 namn = i.Element("namn").Value,
                                 kundnr = i.Element("kundnr").Value,
                                 er = i.Element("er").Value,
                                 momsregnr = i.Element("momsregnr").Value,
                                 fakturaadress1 = i.Element("fakturaadress1").Value,
                                 fakturaadress2 = i.Element("fakturaadress2").Value,
                                 fakturaadress3 = i.Element("fakturaadress3").Value,
                                 fakturaemail = i.Element("fakturaemail").Value
                             }).ToList();

                foreach (var item in items) /* var because is an anonymous type */
                {
                    this.namn = item.namn;
                    this.kundnr = item.kundnr;
                    this.er = item.er;
                    this.momsregnr = item.momsregnr;
                    this.fakturaadress1 = item.fakturaadress1;
                    this.fakturaadress2 = item.fakturaadress2;
                    this.fakturaadress3 = item.fakturaadress3;
                    this.fakturaemail = item.fakturaemail;
                }

                tbForetagName.Text = this.namn;
                tbEr.Text = this.er;
                tbKundNr.Text = this.kundnr;
                tbMomsRegNr.Text = this.momsregnr;
                tbAddress1.Text = this.fakturaadress1;
                tbAddress2.Text = this.fakturaadress2;
                tbAddress3.Text = this.fakturaadress3;
                tbEmail.Text = this.fakturaemail;
            } else {
                // last customer nr
                var xDoc = XDocument.Load(path);

                var userElements = xDoc.Descendants("kund")
                    .ToList();

                if (userElements.Any())
                {
                    string lastDate = userElements.Select(x => x.Element("kundnr").Value)
                        .OrderByDescending(x => x)
                        .First()
                        .ToString();

                    this.lastKundID = lastDate;
                    this.newkundID = Convert.ToInt32(this.lastKundID) + 1;
                    this.newKundNummer = this.newkundID.ToString("000"); ;
                    tbKundNr.Text = this.newKundNummer;
                    //MessageBox.Show(this.newkundID.ToString() + this.newKundNummer);
                }
            }
        }
    }
}
