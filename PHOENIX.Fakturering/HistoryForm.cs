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
    public partial class HistoryForm : Form
    {
        public HistoryForm()
        {
            InitializeComponent();
        }

        private void HistoryForm_Load(object sender, EventArgs e)
        {
            this.Text = "Tidigare fakturor";
            label2.Text = "Summa";
            label3.Text = "Netto: ";
            label4.Text = "Moms: ";
            label5.Text = "Brutto: ";

            listViewFakturor.View = View.Details;

            listViewFakturor.Width = 800;
            listViewFakturor.Location = new System.Drawing.Point(10, 10);

            // Declare and construct the ColumnHeader objects.
            ColumnHeader header1, header2, header3, header4, header5, header6;
            header1 = new ColumnHeader();
            header2 = new ColumnHeader();
            header3 = new ColumnHeader();
            header4 = new ColumnHeader();
            header5 = new ColumnHeader();
            header6 = new ColumnHeader();

            // Set the text, alignment and width for each column header.
            header1.Text = "ID";
            header1.TextAlign = HorizontalAlignment.Right;
            header1.Width = 100;

            header2.TextAlign = HorizontalAlignment.Left;
            header2.Text = "Datum";
            header2.Width = 100;

            header3.TextAlign = HorizontalAlignment.Left;
            header3.Text = "Kund";
            header3.Width = 100;

            header4.TextAlign = HorizontalAlignment.Left;
            header4.Text = "Netto";
            header4.Width = 150;

            header5.TextAlign = HorizontalAlignment.Left;
            header5.Text = "Moms";
            header5.Width = 150;

            header6.TextAlign = HorizontalAlignment.Left;
            header6.Text = "Brutto";
            header6.Width = 150;


            listViewFakturor.Columns.Clear();
            // Add the headers to the ListView control.
            listViewFakturor.Columns.Add(header1);
            listViewFakturor.Columns.Add(header2);
            listViewFakturor.Columns.Add(header3);
            listViewFakturor.Columns.Add(header4);
            listViewFakturor.Columns.Add(header5);
            listViewFakturor.Columns.Add(header6);

            string path = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\data\fakturor.xml";
            listViewFakturor.Items.Clear();
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(path);
            System.Xml.XmlNodeList elemList = doc.GetElementsByTagName("faktura");

            if (elemList != null)
            {

                int tempi = 0;
                for (int i = 0; i < elemList.Count; i++)
                {
                    string attrVal = elemList[i].Attributes["ID"].Value;
                    string attrVal2 = elemList[i].Attributes["Date"].Value;
                    string attrVal3 = elemList[i].Attributes["Kund"].Value;
                    string attrVal4 = elemList[i].Attributes["Netto"].Value;
                    string attrVal5 = elemList[i].Attributes["Moms"].Value;
                    string attrVal6 = elemList[i].Attributes["Brutto"].Value;

                    listViewFakturor.Items.Add(attrVal);
                    listViewFakturor.Items[i].SubItems.Add(attrVal2);
                    listViewFakturor.Items[i].SubItems.Add(attrVal3);
                    listViewFakturor.Items[i].SubItems.Add(attrVal4);
                    listViewFakturor.Items[i].SubItems.Add(attrVal5);
                    listViewFakturor.Items[i].SubItems.Add(attrVal6);
                    tempi = i;
                    // MessageBox.Show(attrVal);
                }


                decimal gtotal1 = 0;
                decimal gtotal2 = 0;
                decimal gtotal3 = 0;
                foreach (ListViewItem lstItem in listViewFakturor.Items)
                {
                    gtotal1 += decimal.Parse(lstItem.SubItems[3].Text);
                    gtotal2 += decimal.Parse(lstItem.SubItems[4].Text);
                    gtotal3 += decimal.Parse(lstItem.SubItems[5].Text);
                }

                label3.Text += gtotal1.ToString("0,0.00") + " kr";
                label4.Text += gtotal2.ToString("0,0.00") + " kr";
                label5.Text += gtotal3.ToString("0,0.00") + " kr";

            }
        }

        private void AddExampleBtn_Click(object sender, EventArgs e)
        {
            //file name
            string filename = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\data\fakturor.xml";
            //string filename = @"fakturor.xml";

            //create new instance of XmlDocument
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();

            //load from file
            doc.Load(filename);

            // Create an element that the new attribute will be added to
            System.Xml.XmlElement xmlNewFaktura = doc.CreateElement("faktura");

            // Create a Continent element and set its value to
            xmlNewFaktura.SetAttribute("ID", "3443");
            xmlNewFaktura.SetAttribute("Date", "2013-01-12");
            xmlNewFaktura.SetAttribute("Kund", "004");
            xmlNewFaktura.SetAttribute("Netto", "343443");
            xmlNewFaktura.SetAttribute("Moms", "343443");
            xmlNewFaktura.SetAttribute("Brutto", "343443");

            //add to elements collection
            doc.DocumentElement.AppendChild(xmlNewFaktura);

            //save back
            doc.Save(filename);
        }
    }
}
