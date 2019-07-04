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
    public partial class NewInvoiceForm : Form
    {
        public string customerID = "0";
        public double iMOMS = 0;
        public string sKindOfFaktura = "0";

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

        public string fvfilename;

        public double countMoms;
        public double summaTotalMedMoms;

        AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();

        public string kunderF;
        public string produkterF;
        public string typesF;
        public string imageF;
        public string imageLogo;
        public string fakturorF;
        public string foretagF;
        public string acontoF;
        public XDocument listofProducts;

        public double summa1;
        public double summa2;
        public double summa3;
        public double summa4;
        public double summa5;
        public double summa6;
        public double summa7;
        public double summa8;
        public double summa9;
        public double summa10;
        public double summa11;
        public double summa12;
        public double summa13;
        public double summa14;

        public double summaTotalNetto;

        public string fakturorDir;

        public string title_FakturaKredit = "Faktura";

        public NewInvoiceForm(string sTEXT, double sMOMS, string sKindOfFaktura)
        {
            InitializeComponent();
            this.customerID = sTEXT;
            this.iMOMS = sMOMS;
            this.sKindOfFaktura = sKindOfFaktura;
        }

        private void NewInvoiceForm_Load(object sender, EventArgs e)
        {
            this.kunderF = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\data\kunder.xml";
            this.produkterF = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\data\produkter.xml";
            this.typesF = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\data\types.xml";
            this.imageF = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\data\table.png";
            this.fakturorF = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\data\fakturor.xml";
            this.foretagF = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\data\foretag.xml";
            this.acontoF = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\data\aconto.xml";

            // if file exists
            this.imageLogo = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\data\logo.png";

            this.listofProducts = XDocument.Load(this.produkterF);

            this.Text = "Ny Faktura";



            XDocument xmldoc = XDocument.Load(this.kunderF);
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

            foreach (var item in items) /* var because customer is an anonymous type */
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

            label9.Text = "Kundnr: " + this.customerID;
            label11.Text = "Er referens: " + this.er;

            label19.Text = "Företagsnamn: " + this.namn;
            label10.Text = "Momsreg. nr: " + this.momsregnr;

            label12.Text = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd");

            label13.Text = dateTimePicker1.Value.AddDays(30).ToString("yyyy-MM-dd");

            label15.Text = "" + this.fakturaadress1;
            label16.Text = "" + this.fakturaadress2;
            label17.Text = "" + this.fakturaadress3;
            label18.Text = "";
            linkLabel1.Text = this.fakturaemail;

            textBox6.Text = lastFakturaID().ToString();
           // MessageBox.Show(lastFakturaID().ToString());
           // return;


            // from types.xml
            XDocument xmldoc_typ = XDocument.Load(this.typesF);
            var items_typ = (from i in xmldoc_typ.Descendants("type")
                             select new
                             {
                                 tnamn = i.Element("tnamn").Value
                             }).ToArray();

            foreach (var item in items_typ)
            {
                cbanttid.Items.Add(item.tnamn.ToString());
                comboBox1.Items.Add(item.tnamn.ToString());
                comboBox2.Items.Add(item.tnamn.ToString());
                comboBox3.Items.Add(item.tnamn.ToString());
                comboBox4.Items.Add(item.tnamn.ToString());
                comboBox5.Items.Add(item.tnamn.ToString());
                comboBox6.Items.Add(item.tnamn.ToString());
                comboBox7.Items.Add(item.tnamn.ToString());
                comboBox8.Items.Add(item.tnamn.ToString());
                comboBox9.Items.Add(item.tnamn.ToString());
                comboBox10.Items.Add(item.tnamn.ToString());
                comboBox11.Items.Add(item.tnamn.ToString());
                comboBox12.Items.Add(item.tnamn.ToString());
                comboBox13.Items.Add(item.tnamn.ToString());
            }

            if (this.sKindOfFaktura == "FAKTURA")
            {
                // from produkter.xml
                XDocument xmldoc_produkter = XDocument.Load(this.produkterF);
                var produkter = (from i in xmldoc_produkter.Descendants("produkt") select new { pnamn = i.Element("pnamn").Value }).ToList();
                foreach (var produkt in produkter) /* var because product is an anonymous type */
                {
                    this.pnamn = produkt.pnamn;
                    autoComplete.Add(this.pnamn.ToString());
                }
            }
            else if (this.sKindOfFaktura == "ACONTO")
            {
                // from aconto.xml
                XDocument xmldoc_produkter = XDocument.Load(this.acontoF);
                var produkter = (from i in xmldoc_produkter.Descendants("produkt") select new { pnamn = i.Element("pnamn").Value }).ToList();
                foreach (var produkt in produkter) /* var because product is an anonymous type */
                {
                    this.pnamn = produkt.pnamn;
                    autoComplete.Add(this.pnamn.ToString());
                }
            }

            //----------- Set the auto suggestion in description box ------------
            textBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox2.AutoCompleteCustomSource = autoComplete;

            textBox7.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox7.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox7.AutoCompleteCustomSource = autoComplete;

            textBox11.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox11.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox11.AutoCompleteCustomSource = autoComplete;

            textBox15.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox15.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox15.AutoCompleteCustomSource = autoComplete;

            textBox19.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox19.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox19.AutoCompleteCustomSource = autoComplete;

            textBox23.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox23.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox23.AutoCompleteCustomSource = autoComplete;

            textBox27.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox27.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox27.AutoCompleteCustomSource = autoComplete;

            textBox31.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox31.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox31.AutoCompleteCustomSource = autoComplete;

            textBox35.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox35.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox35.AutoCompleteCustomSource = autoComplete;

            textBox39.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox39.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox39.AutoCompleteCustomSource = autoComplete;

            textBox43.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox43.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox43.AutoCompleteCustomSource = autoComplete;

            textBox47.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox47.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox47.AutoCompleteCustomSource = autoComplete;

            textBox51.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox51.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox51.AutoCompleteCustomSource = autoComplete;

            textBox55.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox55.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox55.AutoCompleteCustomSource = autoComplete;


            XDocument xmldoc_info = XDocument.Load(this.foretagF);
            var items_info = (from i in xmldoc_info.Descendants("foretag")
                                  // where (string)(i.Element("kundnr")) == this.customerID
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

            foreach (var item_info in items_info) /* var because item_info is an anonymous type */
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

            this.Text = this.foretagnamn + " - Ny faktura";


            //checkBox1.Text = "Omvänd skattskyldighet för byggtjänster gäller."; // old
            checkBox1.Text = "Omvänd betalningsskyldighet.";
            if (this.iMOMS != 0)
            {
                checkBox1.Checked = false;
            }
            else
            {
                checkBox1.Checked = true;
            }

            checkBox2.Checked = true;
            checkBox2.Text = "Godkänd för F-skatt";  // Godkänt F-skattebevis

            CheckBox3.Checked = false;
            CheckBox3.Text = "Kreit Faktura";

            textBox1.Text = "Märkning:";

            label9.Text = "Kundnr: " + this.customerID;
        }














        private void BtnGenerate_Click(object sender, EventArgs e)
        {
             //InterfaceForm fc = new InterfaceForm();
             //MessageBox.Show(fc.produkterLocation());

            if (textBox6.Text == "")
            {
                MessageBox.Show("Ange fakturanummer", "Information");
                return;
            }
            this.fakturorDir = fakturorLocation();

            this.fakturanr = textBox6.Text;

            // generate an invoice
            Document document = new Document();

            if (this.sKindOfFaktura == "FAKTURA")
            {
                this.fvfilename = this.fakturorDir + "faktura.nr." + this.fakturanr + ".pdf";
            }
            else if (this.sKindOfFaktura == "ACONTO")
            {
                this.fvfilename = this.fakturorDir + "faktura.nr." + this.fakturanr + ".aconto.pdf";
            }


            if (this.fvfilename.Contains("/")) { this.fvfilename = this.fvfilename.Replace("/", "-"); }
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(this.fvfilename, FileMode.Create));

            // open the document
            document.Open();

            PdfContentByte cb = writer.DirectContent;

            cb.SetColorStroke(new CMYKColor(100f, 100f, 100f, 100f));

            cb.SetColorFill(new CMYKColor(100f, 100f, 100f, 100f));


            // x, y of bottom left corner, width, height
            //-----------------------------------------------------------
            cb.Rectangle(300f, 775f, 260f, 25f); // first table
            cb.Stroke();
            cb.Rectangle(300f, 745f, 135f, 25f); // second table
            cb.Stroke();

            cb.Rectangle(440f, 745f, 120f, 25f); // third table
            cb.Stroke();

            cb.Rectangle(300f, 650f, 260f, 90f); // fourth table
            cb.Stroke();

            cb.Rectangle(30f, 145f, 530f, 435f); // BENÄMNING
            cb.Stroke();
            cb.Rectangle(30f, 110f, 530f, 30f); // under BENÄMNING
            cb.Stroke();
            //-----------------------------------------------------------

            cb.MoveTo(30, 560);

            cb.LineTo(560, 560);

            // Path closed and stroked

            cb.ClosePathStroke();

            cb.MoveTo(30, 545);
            cb.LineTo(560, 545);
            cb.ClosePathStroke();

            cb.MoveTo(150, 530);
            cb.LineTo(560, 530);
            cb.ClosePathStroke();


            iTextSharp.text.Image img1 = iTextSharp.text.Image.GetInstance(this.imageF);
            img1.ScaleAbsolute(530, 20);
            img1.SetAbsolutePosition(30, 560);
            img1.Alignment = iTextSharp.text.Image.TEXTWRAP;
            document.Add(img1);

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
            //chapter2.SetNumberDepth(0);            
            if (logoExist() == false)
            {
                document.Add(p);
            }


            cb.SetColorFill(new CMYKColor(0f, 0f, 0f, 0f));
            PlaceText(writer.DirectContent, "BENÄMNING", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL), 95, 581, -185, 30, 14, Element.ALIGN_RIGHT);
            PlaceText(writer.DirectContent, "ANTAL/TID", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL), 346, 581, -185, 30, 14, Element.ALIGN_RIGHT);
            PlaceText(writer.DirectContent, "À-PRIS", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL), 447, 581, -185, 30, 14, Element.ALIGN_RIGHT);
            PlaceText(writer.DirectContent, "SUMMA", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL), 556, 581, -185, 30, 14, Element.ALIGN_RIGHT);
            cb.SetColorFill(new CMYKColor(100f, 100f, 100f, 100f));


            // string text_faktura = @"Faktura";
            string text_faktura = this.title_FakturaKredit;
            string text_faktura_address = @"Fakturaadress";
            string text_faktura_address_l1 = this.namn; 
            string text_faktura_address_l2 = this.fakturaadress1;
            string text_faktura_address_l3 = this.fakturaadress2;
            string text_faktura_address_l4 = this.fakturaadress3;

            PdfContentByte cbs = writer.DirectContent;
            cbs.BeginText();
            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, false), 18);
            cbs.SetTextMatrix(314, 782);
            cbs.ShowText(text_faktura);

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 8);
            cbs.SetTextMatrix(303, 732);
            cbs.ShowText(text_faktura_address);
            //--
            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 12);
            cbs.SetTextMatrix(314, 717);
            cbs.ShowText(text_faktura_address_l1);

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 12);
            cbs.SetTextMatrix(314, 702);
            cbs.ShowText(text_faktura_address_l2);

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 12);
            cbs.SetTextMatrix(314, 687);
            cbs.ShowText(text_faktura_address_l3);

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 12);
            cbs.SetTextMatrix(314, 672);
            cbs.ShowText(text_faktura_address_l4);

            string text_faktura_nr = @"Fakt nr / Kundnr";

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 8);
            cbs.SetTextMatrix(303, 762);
            cbs.ShowText(text_faktura_nr);

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 12);
            cbs.SetTextMatrix(314, 749);
            cbs.ShowText(this.fakturanr);

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 12);
            cbs.SetTextMatrix(400, 749);
            cbs.ShowText(this.kundnr);

            string text_faktura_datum = @"Fakturadatum";

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 8);
            cbs.SetTextMatrix(443, 762);
            cbs.ShowText(text_faktura_datum);

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 12);
            cbs.SetTextMatrix(460, 749);
            cbs.ShowText(label12.Text);

            //-- ER
            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 10);
            cbs.SetTextMatrix(30, 635);
            cbs.ShowText("Er referens");

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 10);
            cbs.SetTextMatrix(130, 635);
            cbs.ShowText(this.er);

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 10);
            cbs.SetTextMatrix(30, 620);
            cbs.ShowText("Leveransdatum");

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 10);
            cbs.SetTextMatrix(130, 620);
            cbs.ShowText(label12.Text);

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 10);
            cbs.SetTextMatrix(30, 605);
            cbs.ShowText("Ert momsreg. nr");

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 10);
            cbs.SetTextMatrix(130, 605);
            cbs.ShowText(this.momsregnr);
            //--
            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 10);
            cbs.SetTextMatrix(300, 635);
            cbs.ShowText("Vår referens");

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 10);
            cbs.SetTextMatrix(400, 635);
            cbs.ShowText(this.varreferens);

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 10);
            cbs.SetTextMatrix(300, 620);
            cbs.ShowText("Betalningsvillkor");

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 10);
            cbs.SetTextMatrix(400, 620);

            cbs.ShowText(this.betalningsvillkor + " dagar netto");

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 10);
            cbs.SetTextMatrix(300, 605);
            cbs.ShowText("Förfallodatum");

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 10);
            cbs.SetTextMatrix(400, 605);
            cbs.ShowText(label13.Text);

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 10);
            cbs.SetTextMatrix(300, 590);
            cbs.ShowText("Dröjsmålsränta");

            cbs.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 10);
            cbs.SetTextMatrix(400, 590);
            cbs.ShowText(this.drojsmalsranta);

            cbs.EndText();


            var FontColour = new BaseColor(0, 0, 0);
            var MyFont = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, FontColour);

            // ----------------------------------------
            // antal tid sector
            double atid1 = 3458.98;
            double atid2 = 3458.98;
            double atid3 = 3458.98;
            double atid4 = 3458.98;
            double atid5 = 3458.98;
            double atid6 = 3458.98;
            double atid7 = 3458.98;
            double atid8 = 3458.98;
            double atid9 = 3458.98;
            double atid10 = 3458.98;
            double atid11 = 3458.98;
            double atid12 = 3458.98;
            double atid13 = 3458.98;
            double atid14 = 3458.98;

            if (textBox3.Text != "")
            {
                atid1 = Convert.ToDouble(textBox3.Text);
            }
            if (textBox8.Text != "")
            {
                atid2 = Convert.ToDouble(textBox8.Text);
            }
            if (textBox12.Text != "")
            {
                atid3 = Convert.ToDouble(textBox12.Text);
            }
            if (textBox16.Text != "")
            {
                atid4 = Convert.ToDouble(textBox16.Text);
            }
            if (textBox20.Text != "")
            {
                atid5 = Convert.ToDouble(textBox20.Text);
            }
            if (textBox24.Text != "")
            {
                atid6 = Convert.ToDouble(textBox24.Text);
            }
            if (textBox28.Text != "")
            {
                atid7 = Convert.ToDouble(textBox28.Text);
            }
            if (textBox32.Text != "")
            {
                atid8 = Convert.ToDouble(textBox32.Text);
            }
            if (textBox36.Text != "")
            {
                atid9 = Convert.ToDouble(textBox36.Text);
            }
            if (textBox40.Text != "")
            {
                atid10 = Convert.ToDouble(textBox40.Text);
            }
            if (textBox44.Text != "")
            {
                atid11 = Convert.ToDouble(textBox44.Text);
            }
            if (textBox48.Text != "")
            {
                atid12 = Convert.ToDouble(textBox48.Text);
            }
            if (textBox52.Text != "")
            {
                atid13 = Convert.ToDouble(textBox52.Text);
            }
            if (textBox56.Text != "")
            {
                atid14 = Convert.ToDouble(textBox56.Text);
            }
            // ----------------------------------------
            // a price sector
            double apris1 = 300.68;
            double apris2 = 300.68;
            double apris3 = 300.68;
            double apris4 = 300.68;
            double apris5 = 300.68;
            double apris6 = 300.68;
            double apris7 = 300.68;
            double apris8 = 300.68;
            double apris9 = 300.68;
            double apris10 = 300.68;
            double apris11 = 300.68;
            double apris12 = 300.68;
            double apris13 = 300.68;
            double apris14 = 300.68;

            if (textBox4.Text != "")
            {
                apris1 = Convert.ToDouble(textBox4.Text);
            }
            if (textBox9.Text != "")
            {
                apris2 = Convert.ToDouble(textBox9.Text);
            }
            if (textBox13.Text != "")
            {
                apris3 = Convert.ToDouble(textBox13.Text);
            }
            if (textBox17.Text != "")
            {
                apris4 = Convert.ToDouble(textBox17.Text);
            }
            if (textBox21.Text != "")
            {
                apris5 = Convert.ToDouble(textBox21.Text);
            }
            if (textBox25.Text != "")
            {
                apris6 = Convert.ToDouble(textBox25.Text);
            }
            if (textBox29.Text != "")
            {
                apris7 = Convert.ToDouble(textBox29.Text);
            }
            if (textBox33.Text != "")
            {
                apris8 = Convert.ToDouble(textBox33.Text);
            }
            if (textBox37.Text != "")
            {
                apris9 = Convert.ToDouble(textBox37.Text);
            }
            if (textBox41.Text != "")
            {
                apris10 = Convert.ToDouble(textBox41.Text);
            }
            if (textBox45.Text != "")
            {
                apris11 = Convert.ToDouble(textBox45.Text);
            }
            if (textBox49.Text != "")
            {
                apris12 = Convert.ToDouble(textBox49.Text);
            }
            if (textBox53.Text != "")
            {
                apris13 = Convert.ToDouble(textBox53.Text);
            }
            if (textBox57.Text != "")
            {
                apris14 = Convert.ToDouble(textBox57.Text);
            }

            // ----------------------------------------
            // summa sector
            this.summaTotalNetto = summaAll();

            double summa1 = 0;

            if (textBox3.Text != "" && textBox4.Text != "")
            {
                try
                {
                    textBox5.Text = (atid1 * apris1).ToString();
                }
                catch
                {
                }
            }

            if (textBox5.Text != "" && IsNumber(textBox5.Text) == true)
            {
                summa1 = Convert.ToDouble(textBox5.Text);
            }
            else
            {
                //summa1 = 0;
                MessageBox.Show("I summa, bara siffror", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            string outputAntTid = String.Format("{0:0.##}", atid1);

            PdfContentByte cbk = writer.DirectContent;
            //Write the text here
            cbk.BeginText();
            cbk.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 10);

            // ----------------------------------------
            // apris sector
            if (textBox4.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, apris1.ToString("0,0.00") + " kr", 447, 535, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 447, 535, 0);

            if (textBox9.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, apris2.ToString("0,0.00") + " kr", 447, 520, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 447, 520, 0);

            if (textBox13.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, apris3.ToString("0,0.00") + " kr", 447, 505, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 447, 505, 0);

            if (textBox17.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, apris4.ToString("0,0.00") + " kr", 447, 490, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 447, 490, 0);

            if (textBox21.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, apris5.ToString("0,0.00") + " kr", 447, 475, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 447, 475, 0);

            if (textBox25.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, apris6.ToString("0,0.00") + " kr", 447, 460, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 447, 460, 0);

            if (textBox29.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, apris7.ToString("0,0.00") + " kr", 447, 445, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 447, 445, 0);

            if (textBox33.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, apris8.ToString("0,0.00") + " kr", 447, 430, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 447, 430, 0);

            if (textBox37.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, apris9.ToString("0,0.00") + " kr", 447, 415, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 447, 415, 0);

            if (textBox41.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, apris10.ToString("0,0.00") + " kr", 447, 400, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 447, 400, 0);

            if (textBox45.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, apris11.ToString("0,0.00") + " kr", 447, 385, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 447, 385, 0);

            if (textBox49.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, apris12.ToString("0,0.00") + " kr", 447, 370, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 447, 370, 0);

            if (textBox53.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, apris13.ToString("0,0.00") + " kr", 447, 355, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 447, 355, 0);

            if (textBox57.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, apris14.ToString("0,0.00") + " kr", 447, 340, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 447, 340, 0);

            // ----------------------------------------
            // antal tid sector
            if (textBox3.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, outAntTid(atid1) + " " + cbanttid.Text, 347, 535, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 347, 535, 0);
            if (textBox8.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, outAntTid(atid2) + " " + comboBox1.Text, 347, 520, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 347, 520, 0);
            if (textBox12.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, outAntTid(atid3) + " " + comboBox2.Text, 347, 505, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 347, 505, 0);
            if (textBox16.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, outAntTid(atid4) + " " + comboBox3.Text, 347, 490, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 347, 490, 0);
            if (textBox20.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, outAntTid(atid5) + " " + comboBox4.Text, 347, 475, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 347, 475, 0);
            if (textBox24.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, outAntTid(atid6) + " " + comboBox5.Text, 347, 460, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 347, 460, 0);
            if (textBox28.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, outAntTid(atid7) + " " + comboBox6.Text, 347, 445, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 347, 445, 0);
            if (textBox32.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, outAntTid(atid8) + " " + comboBox7.Text, 347, 430, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 347, 430, 0);
            if (textBox36.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, outAntTid(atid9) + " " + comboBox8.Text, 347, 415, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 347, 415, 0);
            if (textBox40.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, outAntTid(atid10) + " " + comboBox9.Text, 347, 400, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 347, 400, 0);
            if (textBox44.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, outAntTid(atid11) + " " + comboBox10.Text, 347, 385, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 347, 385, 0);
            if (textBox48.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, outAntTid(atid12) + " " + comboBox11.Text, 347, 370, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 347, 370, 0);
            if (textBox52.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, outAntTid(atid13) + " " + comboBox12.Text, 347, 355, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 347, 355, 0);
            if (textBox56.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, outAntTid(atid14) + " " + comboBox13.Text, 347, 340, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 347, 340, 0);

            // ----------------------------------------
            // summa sector
            if (textBox5.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, this.summa1.ToString("0,0.00") + " kr", 557, 535, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 557, 535, 0);
            if (textBox10.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, this.summa2.ToString("0,0.00") + " kr", 557, 520, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 557, 520, 0);
            if (textBox14.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, this.summa3.ToString("0,0.00") + " kr", 557, 505, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 557, 505, 0);
            if (textBox18.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, this.summa4.ToString("0,0.00") + " kr", 557, 490, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 557, 490, 0);
            if (textBox22.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, this.summa5.ToString("0,0.00") + " kr", 557, 475, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 557, 475, 0);
            if (textBox26.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, this.summa6.ToString("0,0.00") + " kr", 557, 460, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 557, 460, 0);
            if (textBox30.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, this.summa7.ToString("0,0.00") + " kr", 557, 445, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 557, 445, 0);
            if (textBox34.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, this.summa8.ToString("0,0.00") + " kr", 557, 430, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 557, 430, 0);
            if (textBox38.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, this.summa9.ToString("0,0.00") + " kr", 557, 415, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 557, 415, 0);
            if (textBox42.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, this.summa10.ToString("0,0.00") + " kr", 557, 400, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 557, 400, 0);
            if (textBox46.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, this.summa11.ToString("0,0.00") + " kr", 557, 385, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 557, 385, 0);
            if (textBox50.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, this.summa12.ToString("0,0.00") + " kr", 557, 370, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 557, 370, 0);
            if (textBox54.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, this.summa13.ToString("0,0.00") + " kr", 557, 355, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 557, 355, 0);
            if (textBox58.Text != "")
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, this.summa14.ToString("0,0.00") + " kr", 557, 340, 0);
            else
                cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, " ", 557, 340, 0);


            cbk.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Netto", 33, 170, 0);

            //double countMoms;
            if (this.summaTotalNetto != 0)
            {
                cbk.ShowTextAligned(PdfContentByte.ALIGN_LEFT, this.summaTotalNetto.ToString("0,0.00") + " kr", 33, 155, 0);
                // counting moms
                if (this.iMOMS != 0)
                {
                    cbk.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Exkl moms", 125, 170, 0);
                    cbk.ShowTextAligned(PdfContentByte.ALIGN_LEFT, this.summaTotalNetto.ToString("0,0.00") + " kr", 125, 155, 0);
                    cbk.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Moms %", 215, 170, 0);
                    cbk.ShowTextAligned(PdfContentByte.ALIGN_LEFT, this.iMOMS.ToString().Replace("0,", "").Replace("0.", "") + "%", 215, 155, 0);
                    cbk.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Moms kr", 305, 170, 0);
                    this.countMoms = this.iMOMS * this.summaTotalNetto;
                    cbk.ShowTextAligned(PdfContentByte.ALIGN_LEFT, this.countMoms.ToString("0,0.00") + " kr", 305, 155, 0);
                    cbk.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Öresavr", 395, 170, 0);
                    cbk.ShowTextAligned(PdfContentByte.ALIGN_LEFT, this.iMOMS.ToString() + "", 395, 155, 0);
                    cbk.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, false), 10);
                    this.summaTotalMedMoms = this.summaTotalNetto + this.countMoms;
                    cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, this.summaTotalMedMoms.ToString("0,0.00") + " kr", 555, 155, 0);
                    //MessageBox.Show(this.countMoms.ToString());
                    saveToFakturor(this.fakturanr, this.dateTimePicker1.Text, this.kundnr, this.summaTotalNetto.ToString("0,0.00"), this.countMoms.ToString("0,0.00"), this.summaTotalMedMoms.ToString("0,0.00"));
                }
                else if (this.iMOMS == 0)
                {
                    if (this.sKindOfFaktura == "FAKTURA")
                    {
                        cbk.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, false), 10);
                        cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, this.summaTotalNetto.ToString("0,0.00") + " kr", 555, 155, 0);
                        saveToFakturor(this.fakturanr, this.dateTimePicker1.Text, this.kundnr, this.summaTotalNetto.ToString("0,0.00"), "0", this.summaTotalNetto.ToString("0,0.00"));
                    }
                    else if (this.sKindOfFaktura == "ACONTO")
                    {
                        if (textBox5.Text != "")
                        {
                            this.summaTotalNetto = Convert.ToDouble(textBox5.Text);
                        }
                        cbk.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, false), 10);
                        cbk.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, this.summaTotalNetto.ToString("0,0.00") + " kr", 555, 155, 0);
                        saveToFakturor(this.fakturanr, this.dateTimePicker1.Text, this.kundnr, this.summaTotalNetto.ToString("0,0.00"), "0", this.summaTotalNetto.ToString("0,0.00"));
                    }
                }
            }
            else
            {
                cbk.ShowTextAligned(PdfContentByte.ALIGN_LEFT, " ", 33, 155, 0);
            }

            cbk.EndText();

            // here if there is a value in the textbox then there is a line, if not then no
            PdfContentByte cbm = writer.DirectContent;
            cbm.SetColorStroke(new CMYKColor(100f, 100f, 100f, 100f));
            cbm.SetColorFill(new CMYKColor(100f, 100f, 100f, 100f));
            //cb.Stroke();
            if (textBox2.Text != "")
            {
                cbm.MoveTo(150, 530);
                cbm.LineTo(560, 530);
                cbm.ClosePathStroke();
            }

            if (textBox7.Text != "")
            {
                cbm.MoveTo(150, 515);
                cbm.LineTo(560, 515);
                cbm.ClosePathStroke();
            }

            if (textBox11.Text != "")
            {
                cbm.MoveTo(150, 500);
                cbm.LineTo(560, 500);
                cbm.ClosePathStroke();
            }

            if (textBox15.Text != "")
            {
                cbm.MoveTo(150, 485);
                cbm.LineTo(560, 485);
                cbm.ClosePathStroke();
            }

            if (textBox19.Text != "")
            {
                cbm.MoveTo(150, 470);
                cbm.LineTo(560, 470);
                cbm.ClosePathStroke();
            }

            if (textBox23.Text != "")
            {
                cbm.MoveTo(150, 455);
                cbm.LineTo(560, 455);
                cbm.ClosePathStroke();
            }

            if (textBox27.Text != "")
            {
                cbm.MoveTo(150, 440);
                cbm.LineTo(560, 440);
                cbm.ClosePathStroke();
            }

            if (textBox31.Text != "")
            {
                cbm.MoveTo(150, 425);
                cbm.LineTo(560, 425);
                cbm.ClosePathStroke();
            }

            if (textBox35.Text != "")
            {
                cbm.MoveTo(150, 410);
                cbm.LineTo(560, 410);
                cbm.ClosePathStroke();
            }

            if (textBox39.Text != "")
            {
                cbm.MoveTo(150, 395);
                cbm.LineTo(560, 395);
                cbm.ClosePathStroke();
            }

            if (textBox43.Text != "")
            {
                cbm.MoveTo(150, 380);
                cbm.LineTo(560, 380);
                cbm.ClosePathStroke();
            }

            if (textBox47.Text != "")
            {
                cbm.MoveTo(150, 365);
                cbm.LineTo(560, 365);
                cbm.ClosePathStroke();
            }

            if (textBox51.Text != "")
            {
                cbm.MoveTo(150, 350);
                cbm.LineTo(560, 350);
                cbm.ClosePathStroke();
            }

            if (textBox55.Text != "")
            {
                cbm.MoveTo(150, 335);
                cbm.LineTo(560, 335);
                cbm.ClosePathStroke();
            }

            PdfContentByte cbd = writer.DirectContent;
            cbd.BeginText();
            cbd.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 10);
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, textBox1.Text, 33, 549, 0);
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, textBox2.Text, 33, 535, 0); // first line
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, textBox7.Text, 33, 520, 0); 
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, textBox11.Text, 33, 505, 0); 
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, textBox15.Text, 33, 490, 0); 
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, textBox19.Text, 33, 475, 0); 
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, textBox23.Text, 33, 460, 0); 
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, textBox27.Text, 33, 445, 0); 
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, textBox31.Text, 33, 430, 0); 
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, textBox35.Text, 33, 415, 0); 
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, textBox39.Text, 33, 400, 0); 
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, textBox43.Text, 33, 385, 0); 
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, textBox47.Text, 33, 370, 0); 
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, textBox51.Text, 33, 355, 0); 
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, textBox55.Text, 33, 340, 0); // fourteenth line


            if (checkBox1.Checked)
            {
                //cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Omvänd skattskyldighet för byggtjänster gäller.", 33, 185, 0); // old
                cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Omvänd betalningsskyldighet.", 33, 185, 0);
            }

            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "ADRESS", 40, 95, 0);
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, this.adress1, 40, 80, 0);
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, this.adress2, 40, 65, 0);
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, this.adress3, 40, 50, 0);

            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "TELEFON", 190, 95, 0);
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, this.telefon, 190, 80, 0);
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "E-POST", 190, 65, 0);
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, this.fepost, 190, 50, 0);

            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "BANKGIRO", 320, 95, 0);
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, this.fbankgiro, 320, 80, 0);

            if (this.fwebbsida != "")
            {
                cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "WEBBSIDA", 320, 65, 0);
                cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, this.fwebbsida, 320, 50, 0);
            }

            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "ORG. NUMMER", 456, 95, 0);
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, this.forgnr, 456, 80, 0);
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "MOMSREG.NR", 456, 65, 0);
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, this.fmomsregnr, 456, 50, 0);
            //cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Godkänt F-skattebevis", 456, 35, 0); //Godkänt - old
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Godkänd för F-skatt", 456, 35, 0); //Godkänt

            // change of the font and inserting - att betala
            cbd.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, false), 10);
            cbd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "ATT BETALA", 490, 170, 0);
            cbd.EndText();

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





































        // DZIALA


        public void saveToFakturor(string ID, string Date, string Kund, string Netto, string Moms, string Brutto)
        {
            //file name
            string filename = this.fakturorF;

            // checking if an invoice exists
            if (fakturaExist(ID) == "")
            {
                //MessageBox.Show("doesn't exist!");
                //create new instance of XmlDocument
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();

                //load from file
                doc.Load(filename);

                // Create an element that the new attribute will be added to
                System.Xml.XmlElement xmlNewFaktura = doc.CreateElement("faktura");

                // Create a new element 
                xmlNewFaktura.SetAttribute("ID", ID);
                xmlNewFaktura.SetAttribute("Date", Date);
                xmlNewFaktura.SetAttribute("Kund", Kund);
                xmlNewFaktura.SetAttribute("Netto", Netto);
                xmlNewFaktura.SetAttribute("Moms", Moms);
                xmlNewFaktura.SetAttribute("Brutto", Brutto);

                //add to elements collection
                doc.DocumentElement.AppendChild(xmlNewFaktura);

                //save back
                doc.Save(filename);
            }
            else
            {
                // override values if exists
                MessageBox.Show("Redigerade berättelser fakturor.");
                UpdateFakturorXML(ID, Date, Kund, Netto, Moms, Brutto);
            }
        }

        public int lastFakturaID()
        {
            string filename = this.fakturorF;

            string path = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\data\fakturor.xml";

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(path);
            System.Xml.XmlNodeList elemList = doc.GetElementsByTagName("faktura");

            string ids = "0";

            int ids2 = 0;
            ids = "0";

            if (elemList != null)
            {
                for (int i = 0; i < elemList.Count; i++)
                {
                    //MessageBox.Show(elemList[i].Attributes["ID"].Value );
                    ids = elemList[i].Attributes["ID"].Value;
                }

                ids2 = Convert.ToInt32(ids) + 1;
                return ids2;
            }
            return ids2;
        }

        public string outAntTid(double val1)
        {
            return String.Format("{0:0.##}", val1);
        }

        public string fakturorLocation()
        {
            string execDir = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            DirectoryInfo df = new DirectoryInfo(execDir + @"\fakturor\");

            if (df.Exists)
            {
                ;// MessageBox.Show("exist");
            }
            else
            {
                // create new directory
                DirectoryInfo di = Directory.CreateDirectory(execDir + @"\fakturor\");
            }
            return execDir + @"\fakturor\";
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
            ct.SetSimpleColumn(new Phrase(text, font), lowerLeftx,
            lowerLefty, upperRightx, upperRighty, leading, alignment);
            ct.Go();
        }

        public string getPrice(string productName)
        {
            var items = (from i in listofProducts.Descendants("produkt")
                         where (string)(i.Element("pnamn")) == productName
                         select new
                         {
                             pnamn = i.Element("pnamn").Value,
                             pris = i.Element("pris").Value,
                         }).ToList();

            foreach (var item in items) /* var because product is an anonymous type */
            {
                // MessageBox.Show(item.pnamn.ToString());
                // MessageBox.Show(item.pris.ToString());
                return item.pris.ToString();
            }

            return "";
        }

        public double convertToDouble(string value)
        {
            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {
                return 0;
            }
        }

        public bool IsNumber(string text)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^[-+]?[0-9]*\,?[0-9]+$");
            return regex.IsMatch(text);
        }

        public double summaAll()
        {
            if (this.sKindOfFaktura == "FAKTURA")
            {
                this.summa1 = convertToDouble(textBox5.Text);
                this.summa2 = convertToDouble(textBox10.Text);
                this.summa3 = convertToDouble(textBox14.Text);
                this.summa4 = convertToDouble(textBox18.Text);
                this.summa5 = convertToDouble(textBox22.Text);
                this.summa6 = convertToDouble(textBox26.Text);
                this.summa7 = convertToDouble(textBox30.Text);
                this.summa8 = convertToDouble(textBox34.Text);
                this.summa9 = convertToDouble(textBox38.Text);
                this.summa10 = convertToDouble(textBox42.Text);
                this.summa11 = convertToDouble(textBox46.Text);
                this.summa12 = convertToDouble(textBox50.Text);
                this.summa13 = convertToDouble(textBox54.Text);
                this.summa14 = convertToDouble(textBox58.Text);

                this.summaTotalNetto = this.summa1 + this.summa2 + this.summa3 + this.summa4 + this.summa5 + this.summa6 + this.summa7 + this.summa8 + this.summa9 + this.summa10 + this.summa11 + this.summa12 + this.summa13 + this.summa14;
            }
            else if (this.sKindOfFaktura == "ACONTO")
            {
                this.summa1 = convertToDouble(textBox5.Text);
                this.summa2 = convertToDouble(textBox10.Text);
                this.summa3 = convertToDouble(textBox14.Text);
                this.summa4 = convertToDouble(textBox18.Text);
                this.summa5 = convertToDouble(textBox22.Text);
                this.summa6 = convertToDouble(textBox26.Text);
                this.summa7 = convertToDouble(textBox30.Text);
                this.summa8 = convertToDouble(textBox34.Text);
                this.summa9 = convertToDouble(textBox38.Text);
                this.summa10 = convertToDouble(textBox42.Text);
                this.summa11 = convertToDouble(textBox46.Text);
                this.summa12 = convertToDouble(textBox50.Text);
                this.summa13 = convertToDouble(textBox54.Text);
                this.summa14 = convertToDouble(textBox58.Text);
                this.summaTotalNetto = this.summa1;
            }

            return this.summaTotalNetto;
        }

        public string fakturaExist(string fnummer)
        {
            string filename = this.fakturorF;

            XDocument xmldoc_typ = XDocument.Load(filename);

            var items_typ = (from c in xmldoc_typ.Descendants("faktura")
                             where c.Attribute("ID").Value == fnummer
                             //select c.Value
                             select new
                             {
                                 faktura = c.Attribute("ID").Value,
                                 date = c.Attribute("Date").Value
                             }
                        ).ToList();

            if (items_typ.Count() > 0)
            {
                // the invoice including this number exist
                foreach (var item in items_typ)
                {
                    return item.faktura.ToString();
                }
            }
            else
            {
                return "";
            }
            return "";
        }

        public bool UpdateFakturorXML(string fakturaID, string Date, string Kund, string Netto, string Moms, string Brutto)
        {
            XDocument xmlFile = XDocument.Load(this.fakturorF);
            var query = from c in xmlFile.Elements("fakturor").Elements("faktura")
                        where (string)(c.Attribute("ID")) == fakturaID
                        select c;
            foreach (XElement foretag in query)
            {
                foretag.Attribute("Date").Value = Date;
                foretag.Attribute("Kund").Value = Kund;
                foretag.Attribute("Netto").Value = Netto;
                foretag.Attribute("Moms").Value = Moms;
                foretag.Attribute("Brutto").Value = Brutto;
            }
            xmlFile.Save(this.fakturorF);
            return true;
        }

        private void TextBox6_TextChanged(object sender, EventArgs e)
        {
            // here is changing if customer want 
            this.fakturanr = textBox6.Text;
        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox3.Checked == true)
            {
                groupBox2.Text = "Kredit Faktura";
                this.title_FakturaKredit = "Kredit Faktura";
            }
            if (CheckBox3.Checked == false)
            {
                groupBox2.Text = "Faktura";
                this.title_FakturaKredit = "Faktura";
            }
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox4.Text) * Convert.ToDouble(textBox3.Text);
                textBox5.Text = Convert.ToString(gf);

            }
            catch
            {
                // MessageBox.Show(fEx.Message);
            }
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox4.Text) * Convert.ToDouble(textBox3.Text);
                textBox5.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox8_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox8.Text) * Convert.ToDouble(textBox9.Text);
                textBox10.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox9_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox8.Text) * Convert.ToDouble(textBox9.Text);
                textBox10.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox12_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox12.Text) * Convert.ToDouble(textBox13.Text);
                textBox14.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox13_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox12.Text) * Convert.ToDouble(textBox13.Text);
                textBox14.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox16_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox16.Text) * Convert.ToDouble(textBox17.Text);
                textBox18.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox17_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox16.Text) * Convert.ToDouble(textBox17.Text);
                textBox18.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox20_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox20.Text) * Convert.ToDouble(textBox21.Text);
                textBox22.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox21_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox20.Text) * Convert.ToDouble(textBox21.Text);
                textBox22.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox24_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox24.Text) * Convert.ToDouble(textBox25.Text);
                textBox26.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox25_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox24.Text) * Convert.ToDouble(textBox25.Text);
                textBox26.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox28_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox28.Text) * Convert.ToDouble(textBox29.Text);
                textBox30.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox29_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox28.Text) * Convert.ToDouble(textBox29.Text);
                textBox30.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox32_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox32.Text) * Convert.ToDouble(textBox33.Text);
                textBox34.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox33_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox32.Text) * Convert.ToDouble(textBox33.Text);
                textBox34.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox36_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox36.Text) * Convert.ToDouble(textBox37.Text);
                textBox38.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox37_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox36.Text) * Convert.ToDouble(textBox37.Text);
                textBox38.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox40_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox40.Text) * Convert.ToDouble(textBox41.Text);
                textBox42.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox41_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox40.Text) * Convert.ToDouble(textBox41.Text);
                textBox42.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox44_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox44.Text) * Convert.ToDouble(textBox45.Text);
                textBox46.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox45_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox44.Text) * Convert.ToDouble(textBox45.Text);
                textBox46.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox48_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox48.Text) * Convert.ToDouble(textBox49.Text);
                textBox50.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox49_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox48.Text) * Convert.ToDouble(textBox49.Text);
                textBox50.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox52_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox52.Text) * Convert.ToDouble(textBox53.Text);
                textBox54.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox53_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox52.Text) * Convert.ToDouble(textBox53.Text);
                textBox54.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox56_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox56.Text) * Convert.ToDouble(textBox57.Text);
                textBox58.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox57_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gf = Convert.ToDouble(textBox56.Text) * Convert.ToDouble(textBox57.Text);
                textBox58.Text = Convert.ToString(gf);
            }
            catch { }
        }

        private void TextBox2_Leave(object sender, EventArgs e)
        {
            // when leave, the price from produkter.xml show up
            if (this.sKindOfFaktura == "FAKTURA")
            {
                textBox4.Text = this.getPrice(textBox2.Text);
            }
        }

        private void TextBox7_Leave(object sender, EventArgs e)
        {
            if (this.sKindOfFaktura == "FAKTURA")
            {
                textBox9.Text = this.getPrice(textBox7.Text);
            }
        }

        private void TextBox11_Leave(object sender, EventArgs e)
        {
            if (this.sKindOfFaktura == "FAKTURA")
            {
                textBox13.Text = this.getPrice(textBox11.Text);
            }
        }

        private void TextBox15_Leave(object sender, EventArgs e)
        {
            if (this.sKindOfFaktura == "FAKTURA")
            {
                textBox17.Text = this.getPrice(textBox15.Text);
            }
        }

        private void TextBox19_Leave(object sender, EventArgs e)
        {
            if (this.sKindOfFaktura == "FAKTURA")
            {
                textBox21.Text = this.getPrice(textBox19.Text);
            }
        }

        private void TextBox23_Leave(object sender, EventArgs e)
        {
            if (this.sKindOfFaktura == "FAKTURA")
            {
                textBox25.Text = this.getPrice(textBox23.Text);
            }
        }

        private void TextBox27_Leave(object sender, EventArgs e)
        {
            if (this.sKindOfFaktura == "FAKTURA")
            {
                textBox29.Text = this.getPrice(textBox27.Text);
            }
        }

        private void TextBox31_Leave(object sender, EventArgs e)
        {
            if (this.sKindOfFaktura == "FAKTURA")
            {
                textBox33.Text = this.getPrice(textBox31.Text);
            }
        }

        private void TextBox35_Leave(object sender, EventArgs e)
        {
            if (this.sKindOfFaktura == "FAKTURA")
            {
                textBox37.Text = this.getPrice(textBox35.Text);
            }
        }

        private void TextBox39_Leave(object sender, EventArgs e)
        {
            if (this.sKindOfFaktura == "FAKTURA")
            {
                textBox41.Text = this.getPrice(textBox39.Text);
            }
        }

        private void TextBox43_Leave(object sender, EventArgs e)
        {
            if (this.sKindOfFaktura == "FAKTURA")
            {
                textBox45.Text = this.getPrice(textBox43.Text);
            }
        }

        private void TextBox47_Leave(object sender, EventArgs e)
        {
            if (this.sKindOfFaktura == "FAKTURA")
            {
                textBox49.Text = this.getPrice(textBox47.Text);
            }
        }

        private void TextBox51_Leave(object sender, EventArgs e)
        {
            if (this.sKindOfFaktura == "FAKTURA")
            {
                textBox53.Text = this.getPrice(textBox51.Text);
            }
        }

        private void TextBox55_Leave(object sender, EventArgs e)
        {
            if (this.sKindOfFaktura == "FAKTURA")
            {
                textBox57.Text = this.getPrice(textBox55.Text);
            }
        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.fakturadatum = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd");
            this.forfallodatum = dateTimePicker1.Value.AddDays(30).ToString("yyyy-MM-dd");

            label12.Text = this.fakturadatum;
            label13.Text = this.forfallodatum;
        }
    }
}

