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
    public partial class ProductForm : Form
    {
        public ProductForm(string sTEXT)
        {
            InitializeComponent();
            this.productName = sTEXT;
        }

        public string productName;
        public string pnamn;
        public string pris;

        private void ProductForm_Load(object sender, EventArgs e)
        {
            string path = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\data\produkter.xml";
            toolStripStatusLabel1.Text = "";

            if (this.productName != "NEW")
            {
                this.Text = "Redigera - " + this.productName;
                XDocument xmldoc = XDocument.Load(path);
                var items = (from i in xmldoc.Descendants("produkt")
                             where (string)(i.Element("pnamn")) == this.productName
                             select new
                             {
                                 pnamn = i.Element("pnamn").Value,
                                 pris = i.Element("pris").Value
                             }).ToList();

                foreach (var item in items) /* var because product is an anonymous type */
                {
                    this.pnamn = item.pnamn;
                    this.pris = item.pris;
                }
                textBoxName.Text = this.pnamn;
                textBoxPris.Text = this.pris;
            }
            else
            {
                this.Text = "Ny produkt";
            }
        }

        private void Indextop_Click(object sender, EventArgs e)
        {
            textBoxName.Text += "²";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string path = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\data\produkter.xml";
            if (this.productName != "NEW")
            {
                XDocument xmlFile = XDocument.Load(path);
                var query = from c in xmlFile.Elements("produkter").Elements("produkt")
                            where (string)(c.Element("pnamn")) == this.productName
                            select c;
                foreach (XElement foretag in query)
                {
                    foretag.Element("pnamn").Value = textBoxName.Text;
                    foretag.Element("pris").Value = textBoxPris.Text;
                }
                xmlFile.Save(path);
            }
            else
            {
                string filename = path;

                //create new instance of XmlDocument
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();

                //load from file
                doc.Load(filename);

                //create node and add value
                System.Xml.XmlNode node = doc.CreateNode(System.Xml.XmlNodeType.Element, "produkt", null);

                System.Xml.XmlNode nodeNamn = doc.CreateElement("pnamn");
                nodeNamn.InnerText = textBoxName.Text;

                System.Xml.XmlNode nodeER = doc.CreateElement("pris");
                nodeER.InnerText = textBoxPris.Text;

                //add to parent node
                node.AppendChild(nodeNamn);
                node.AppendChild(nodeER);

                //add to elements collection
                doc.DocumentElement.AppendChild(node);

                //save back
                doc.Save(filename);
            }
            toolStripStatusLabel1.Text = "Sparade";
        }
    }
}
