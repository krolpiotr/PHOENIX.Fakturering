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

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace PHOENIX.Fakturering
{
    public partial class ProductsForm : Form
    {
        public ProductsForm()
        {
            InitializeComponent();
        }

        public string produkterF;
        public XDocument listofProducts;

        public string foretagF;
        public string foretagnamn;
        public string adress1;
        public string adress2;
        public string adress3;
        public string telefon;
        public string varreferens;
        public string fepost;
        public string fmomsregnr;
        public string forgnr;
        public string fwebbsida;
        public string fbankgiro;
        public string betalningsvillkor;
        public string drojsmalsranta;
        public string fontsize;

        public string imageLogo;




        public string fvfilename;

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

        public string tnamn;
        public string tshort;

        public string pnamn;

        public string fakturanr;

        public double countMoms;
        public double summaTotalMedMoms;

        AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();

        public string kunderF;
        public string typesF;
        public string imageF;
        public string fakturorF;
        public string acontoF;

        public string produkterDir;

        public double pagesAll = 0;

        public string produkterLocation()
        {
            string execDir = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            DirectoryInfo df = new DirectoryInfo(execDir + @"\produkter\");



            if (df.Exists)
            {
                ;// MessageBox.Show("fdf");
            }
            else
            {
                // create new directory
                DirectoryInfo di = Directory.CreateDirectory(execDir + @"\produkter\");

            }
            return execDir + @"\produkter\";
        }

        protected static void PlaceText(PdfContentByte pdfContentByte
                                , string text
                                , iTextSharp.text.Font font
                                , float lowerLeftx
                                , float lowerLefty
                                , float upperRightx
                                , float upperRighty
                                , float leading
                                , int alignment)
        {
            ColumnText ct = new ColumnText(pdfContentByte);
            ct.SetSimpleColumn(new Phrase(text, font), lowerLeftx, lowerLefty, upperRightx, upperRighty, leading, alignment);
            ct.Go();
        }

        public bool Is40(int iIt)
        {
            if ((iIt % 40) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            //System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            //return regex.IsMatch(text);
        }

        public bool IsNumber(string text)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^[-+]?[0-9]*\,?[0-9]+$");
            return regex.IsMatch(text);
        }

        private void ProductsForm_Load(object sender, EventArgs e)
        {
            btnDelete.Text = "Ta_bort";
            this.Text = "Produkter";
            btnAdd.Text = "Lägg till";
            btnEdit.Text = "Redigera";
            btnPrint.Text = "Print";

            this.produkterF = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\data\produkter.xml";
            this.listofProducts = XDocument.Load(this.produkterF);
            this.imageLogo = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\data\logo.png";
            this.foretagF = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\data\foretag.xml";

            XDocument xmldoc_info = XDocument.Load(this.foretagF);
            var items_info = (from i in xmldoc_info.Descendants("foretag")
                                  //   where (string)(i.Element("kundnr")) == this.customerID
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

            foreach (var item_info in items_info) /* var because product is an anonymous type */
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

            listViewProdukter.View = View.Details;

            listViewProdukter.Width = 724;
            listViewProdukter.Location = new System.Drawing.Point(10, 10);

            // Declare and construct the ColumnHeader objects.
            ColumnHeader header1, header2, header3, header4, header5, header6;
            header1 = new ColumnHeader();
            header2 = new ColumnHeader();
            header3 = new ColumnHeader();
            header4 = new ColumnHeader();
            header5 = new ColumnHeader();
            header6 = new ColumnHeader();

            // Set the text, alignment and width for each column header.
            header1.Text = "Namn";
            header1.TextAlign = HorizontalAlignment.Right;
            header1.Width = 300;

            header2.TextAlign = HorizontalAlignment.Left;
            header2.Text = "Pris";
            header2.Width = 100;

            listViewProdukter.Columns.Clear();
            // Add the headers to the ListView control.
            listViewProdukter.Columns.Add(header1);
            listViewProdukter.Columns.Add(header2);

            string path = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\data\produkter.xml";
            listViewProdukter.Items.Clear();

            XDocument xmldoc = XDocument.Load(path);
            var items = (from i in xmldoc.Descendants("produkt")
                             // where (string)(i.Element("pnamn")) == "AL LAMELL 30 mm 9,6 m²"
                         select new
                         {
                             pnamn = i.Element("pnamn").Value,
                             pris = i.Element("pris").Value
                         }).ToList();

            for (int i = 0; i < items.Count; i++)
            {
                listViewProdukter.Items.Add(items[i].pnamn.ToString());
                listViewProdukter.Items[i].SubItems.Add(items[i].pris.ToString() + " kr");
            }
            listViewProdukter.Items.RemoveAt(0);
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            Document document = new Document();

            this.produkterDir = produkterLocation();
            this.fvfilename = this.produkterDir + "produkter.pdf";

            // if (this.fvfilename.Contains("/")) { this.fvfilename = this.fvfilename.Replace("/", "-"); }
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(this.fvfilename, FileMode.Create));
            // open the document
            document.Open();

            PdfContentByte cb = writer.DirectContent;

            cb.SetColorStroke(new CMYKColor(100f, 100f, 100f, 100f));

            cb.SetColorFill(new CMYKColor(100f, 100f, 100f, 100f));

            // x, y of bottom left corner, width, height
            //-----------------------------------------------------------
            cb.Rectangle(300f, 775f, 260f, 25f); // pierwsza tabelka
            cb.Stroke();
            cb.Rectangle(300f, 745f, 135f, 25f); // druga tabelka
            cb.Stroke();

            cb.Rectangle(440f, 745f, 120f, 25f); // druga tabelka
            cb.Stroke();


            cb.Rectangle(300f, 650f, 260f, 90f); // czwarta tabelka
            cb.Stroke();

            if (logoExist() == true)
            {
                iTextSharp.text.Image imgLogo = iTextSharp.text.Image.GetInstance(this.imageLogo);
                imgLogo.ScaleAbsolute(264, 187);
                imgLogo.SetAbsolutePosition(30, 613);
                imgLogo.Alignment = iTextSharp.text.Image.TEXTWRAP;
                document.Add(imgLogo);
            }

            iTextSharp.text.Font georgia = FontFactory.GetFont("georgia", 10f);

            int fontSix = Convert.ToInt32(this.fontsize);
            Paragraph p = new Paragraph(this.foretagnamn, FontFactory.GetFont(FontFactory.TIMES_BOLD, fontSix, iTextSharp.text.Font.NORMAL, new iTextSharp.text.BaseColor(0, 0, 0)));
            Chapter chapter2 = new Chapter(p, 0);
            if (logoExist() == false)
            {
                document.Add(p);
            }

            cb.SetColorFill(new CMYKColor(100f, 100f, 100f, 100f));
            PlaceText(writer.DirectContent, "BENÄMNING", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL), 600, 650, 30, 30, 14, Element.ALIGN_LEFT);
            PlaceText(writer.DirectContent, "", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL), 346, 650, -185, 30, 14, Element.ALIGN_RIGHT);
            PlaceText(writer.DirectContent, "ENHETER", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL), 447, 650, -185, 30, 14, Element.ALIGN_RIGHT);
            PlaceText(writer.DirectContent, "À-PRIS", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL), 556, 650, -185, 30, 14, Element.ALIGN_RIGHT);
            PlaceText(writer.DirectContent, "Produkter", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 18, iTextSharp.text.Font.BOLD), 386, 793, -185, 30, 14, Element.ALIGN_RIGHT);
            cb.SetColorFill(new CMYKColor(100f, 100f, 100f, 100f));

            string path = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\data\produkter.xml";
            // listViewProdukter.Items.Clear();

            XDocument xmldoc = XDocument.Load(path);
            var items = (from i in xmldoc.Descendants("produkt")
                             // where (string)(i.Element("pnamn")) == "AL LAMELL 30 mm 9,6 m²"
                         select new
                         {
                             pnamn = i.Element("pnamn").Value,
                             pris = i.Element("pris").Value
                         }).ToList();

            // liczba stron
            if ((items.Count % 40) == 0)
            {
                this.pagesAll = items.Count / 40;
            }
            else if ((items.Count % 40) != 0)
            {
                this.pagesAll = (items.Count / 40) + 1;
            }
            double item_price = 0.8;
            string s_item_price;
            int currentPage = 1;
            string text_faktura_datum = @"Datum";

            string text_faktura_address_l1 = this.adress1; 
            string text_faktura_address_l2 = this.adress2;
            string text_faktura_address_l3 = this.adress3;


            PdfContentByte cbs = writer.DirectContent;
            cbs.BeginText();

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 8);
            cbs.SetTextMatrix(443, 762);
            cbs.ShowText(text_faktura_datum);

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 8);
            cbs.SetTextMatrix(303, 732);
            cbs.ShowText("Företagadress");

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 8);
            cbs.SetTextMatrix(303, 762);
            cbs.ShowText("Sida");

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 12);
            cbs.SetTextMatrix(314, 749);
            cbs.ShowText("  " + currentPage.ToString() + " / " + this.pagesAll.ToString());

            string strDateNow = System.DateTime.Now.ToString("yyyy-MM-dd");

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 12);
            cbs.SetTextMatrix(460, 749);
            cbs.ShowText(strDateNow);

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 12);
            cbs.SetTextMatrix(314, 717);
            cbs.ShowText(text_faktura_address_l1);

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 12);
            cbs.SetTextMatrix(314, 702);
            cbs.ShowText(text_faktura_address_l2);

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 12);
            cbs.SetTextMatrix(314, 687);
            cbs.ShowText(text_faktura_address_l3);

            cbs.EndText();


            int kLine = 650;
            int underLine = 633;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].pris.ToString() != "")
                {
                    if (IsNumber(items[i].pris.ToString()) == true)
                    {
                        item_price = Convert.ToDouble(items[i].pris.ToString());
                        s_item_price = item_price.ToString("0,0.00") + " kr";
                    }
                    else
                    {
                        item_price = 0;
                        s_item_price = items[i].pris.ToString();
                    }

                }
                else
                {
                    item_price = 0;
                    s_item_price = "";
                }

                PlaceText(writer.DirectContent, items[i].pnamn.ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL), 600, kLine, 30, 30, 14, Element.ALIGN_LEFT);
                PlaceText(writer.DirectContent, s_item_price, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL), 556, kLine, -185, 30, 14, Element.ALIGN_RIGHT);
                kLine = kLine - 15;

                cb.MoveTo(100, underLine);
                cb.LineTo(560, underLine);
                cb.ClosePathStroke();
                underLine = underLine - 15;
                if (Is40(i) == true && i != 0) // 
                                               // if (i == 40 || i == 80 || i == 120 || i == 160 || i == 200)
                {
                    kLine = 685;
                    underLine = 668;

                    document.NewPage();
                    // currentPage = document.PageNumber;

                    PdfContentByte Dcbs = writer.DirectContent;
                    Dcbs.BeginText();

                    Dcbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 8);
                    Dcbs.SetTextMatrix(443, 762);
                    Dcbs.ShowText(text_faktura_datum);


                    Dcbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 12);
                    Dcbs.SetTextMatrix(460, 749);
                    Dcbs.ShowText(strDateNow);

                    Dcbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 8);
                    Dcbs.SetTextMatrix(303, 762);
                    Dcbs.ShowText("Sida");

                    Dcbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 12);
                    Dcbs.SetTextMatrix(314, 749);
                    currentPage = currentPage + 1;
                    Dcbs.ShowText("  " + currentPage.ToString() + " / " + this.pagesAll.ToString());


                    Dcbs.EndText();

                    // x, y of bottom left corner, width, height
                    //-----------------------------------------------------------
                    cb.Rectangle(300f, 775f, 260f, 25f); // first table
                    cb.Stroke();

                    cb.Rectangle(300f, 745f, 135f, 25f); // second table
                    cb.Stroke();

                    cb.Rectangle(440f, 745f, 120f, 25f); // third table
                    cb.Stroke();

                    if (logoExist() == true)
                    {
                        iTextSharp.text.Image imgLogo = iTextSharp.text.Image.GetInstance(this.imageLogo);
                        imgLogo.ScaleAbsolute(264, 187);
                        imgLogo.SetAbsolutePosition(30, 613);
                        imgLogo.Alignment = iTextSharp.text.Image.TEXTWRAP;
                        document.Add(imgLogo);
                    }
          
                    if (logoExist() == false)
                    {
                        document.Add(p);
                    }

                    cb.SetColorFill(new CMYKColor(100f, 100f, 100f, 100f));
                    PlaceText(writer.DirectContent, "BENÄMNING", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL), 600, kLine + 15, 30, 30, 14, Element.ALIGN_LEFT);
                    PlaceText(writer.DirectContent, "", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL), 346, kLine + 15, -185, 30, 14, Element.ALIGN_RIGHT);
                    PlaceText(writer.DirectContent, "ENHETER", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL), 447, kLine + 15, -185, 30, 14, Element.ALIGN_RIGHT);
                    PlaceText(writer.DirectContent, "À-PRIS", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL), 556, kLine + 15, -185, 30, 14, Element.ALIGN_RIGHT);
                    PlaceText(writer.DirectContent, "Produkter", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 18, iTextSharp.text.Font.BOLD), 386, 793, -185, 30, 14, Element.ALIGN_RIGHT);
                    cb.SetColorFill(new CMYKColor(100f, 100f, 100f, 100f));

                    cb.MoveTo(100, 683);
                    cb.LineTo(560, 683);
                    cb.ClosePathStroke();

                }
            }

            cb.ClosePathFillStroke();

            document.Close();

            System.Threading.Thread.Sleep(1000);
            try
            {
                System.Diagnostics.Process.Start(this.fvfilename);
            }
            catch
            { }
        }

        public bool logoExist()
        {
            if (File.Exists(System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\data\logo.png"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void autoloadProducts()
        {
            listViewProdukter.View = View.Details;

            listViewProdukter.Width = 724;
            listViewProdukter.Location = new System.Drawing.Point(10, 10);

            // Declare and construct the ColumnHeader objects.
            ColumnHeader header1, header2, header3, header4, header5, header6;
            header1 = new ColumnHeader();
            header2 = new ColumnHeader();
            header3 = new ColumnHeader();
            header4 = new ColumnHeader();
            header5 = new ColumnHeader();
            header6 = new ColumnHeader();

            // Set the text, alignment and width for each column header.
            header1.Text = "Namn";
            header1.TextAlign = HorizontalAlignment.Right;
            header1.Width = 300;

            header2.TextAlign = HorizontalAlignment.Left;
            header2.Text = "Pris";
            header2.Width = 100;

            listViewProdukter.Columns.Clear();
            // Add the headers to the ListView control.
            listViewProdukter.Columns.Add(header1);
            listViewProdukter.Columns.Add(header2);

            string path = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\data\produkter.xml";
            listViewProdukter.Items.Clear();

            XDocument xmldoc = XDocument.Load(path);
            var items = (from i in xmldoc.Descendants("produkt")
                             // where (string)(i.Element("pnamn")) == "AL LAMELL 30 mm 9,6 m²"
                         select new
                         {
                             pnamn = i.Element("pnamn").Value,
                             pris = i.Element("pris").Value
                         }).ToList();

            for (int i = 0; i < items.Count; i++)
            {
                listViewProdukter.Items.Add(items[i].pnamn.ToString());
                listViewProdukter.Items[i].SubItems.Add(items[i].pris.ToString() + " kr");
            }
            listViewProdukter.Items.RemoveAt(0);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (listViewProdukter.SelectedItems.Count > 1)
            {
                MessageBox.Show("Välj en produkt");
            }
            else if (listViewProdukter.SelectedItems.Count == 1)
            {
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(this.produkterF);

                //Use an XPath query to find a XmlNode
                System.Xml.XmlNode deleteContact =
                   doc.SelectSingleNode("descendant::produkt[pnamn='" + listViewProdukter.SelectedItems[0].Text + "']");

                //Remove the XmlNode from the Document
                doc.DocumentElement.RemoveChild(deleteContact);
                //Save the Document

                doc.Save(this.produkterF);
                listViewProdukter.SelectedItems[0].Remove();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ProductForm frpForm = new ProductForm("NEW");
            frpForm.ShowDialog();
            autoloadProducts();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (listViewProdukter.SelectedItems.Count == 1)
            {
                ProductForm frForm = new ProductForm(listViewProdukter.SelectedItems[0].Text);
                frForm.ShowDialog();
                autoloadProducts();
            }
            else
            {
                MessageBox.Show("Välj en produkt");
            }
        }
    }
}
