namespace PHOENIX.Fakturering
{
    partial class InterfaceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaceForm));
            this.menuStripInterface = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customersBox = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbFAconto = new System.Windows.Forms.RadioButton();
            this.rbFaktura = new System.Windows.Forms.RadioButton();
            this.btnProducts = new System.Windows.Forms.Button();
            this.btnHistory = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rd0 = new System.Windows.Forms.RadioButton();
            this.rd6 = new System.Windows.Forms.RadioButton();
            this.rd12 = new System.Windows.Forms.RadioButton();
            this.rd25 = new System.Windows.Forms.RadioButton();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.countWordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDeleteCustomer = new System.Windows.Forms.Button();
            this.btnAddCustomer = new System.Windows.Forms.Button();
            this.btnEditCustomer = new System.Windows.Forms.Button();
            this.btnMyCompany = new System.Windows.Forms.Button();
            this.btnNewInvoice = new System.Windows.Forms.Button();
            this.menuStripInterface.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripInterface
            // 
            this.menuStripInterface.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripInterface.Location = new System.Drawing.Point(0, 0);
            this.menuStripInterface.Name = "menuStripInterface";
            this.menuStripInterface.Size = new System.Drawing.Size(447, 24);
            this.menuStripInterface.TabIndex = 0;
            this.menuStripInterface.Text = "menuStripInterface";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.fileToolStripMenuItem.Text = "Arkiv";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.ArkivToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.countWordsToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.helpToolStripMenuItem.Text = "Hjälp";
            // 
            // customersBox
            // 
            this.customersBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.customersBox.FormattingEnabled = true;
            this.customersBox.ItemHeight = 17;
            this.customersBox.Location = new System.Drawing.Point(11, 26);
            this.customersBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.customersBox.Name = "customersBox";
            this.customersBox.Size = new System.Drawing.Size(178, 327);
            this.customersBox.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbFAconto);
            this.groupBox2.Controls.Add(this.rbFaktura);
            this.groupBox2.Location = new System.Drawing.Point(203, 76);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(225, 46);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Faktura";
            // 
            // rbFAconto
            // 
            this.rbFAconto.AutoSize = true;
            this.rbFAconto.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rbFAconto.Location = new System.Drawing.Point(94, 17);
            this.rbFAconto.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbFAconto.Name = "rbFAconto";
            this.rbFAconto.Size = new System.Drawing.Size(101, 17);
            this.rbFAconto.TabIndex = 5;
            this.rbFAconto.TabStop = true;
            this.rbFAconto.Text = "Faktura A-conto";
            this.rbFAconto.UseVisualStyleBackColor = true;
            // 
            // rbFaktura
            // 
            this.rbFaktura.AutoSize = true;
            this.rbFaktura.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rbFaktura.Location = new System.Drawing.Point(4, 17);
            this.rbFaktura.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbFaktura.Name = "rbFaktura";
            this.rbFaktura.Size = new System.Drawing.Size(61, 17);
            this.rbFaktura.TabIndex = 4;
            this.rbFaktura.TabStop = true;
            this.rbFaktura.Text = "Faktura";
            this.rbFaktura.UseVisualStyleBackColor = true;
            // 
            // btnProducts
            // 
            this.btnProducts.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F);
            this.btnProducts.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnProducts.Location = new System.Drawing.Point(203, 182);
            this.btnProducts.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnProducts.Name = "btnProducts";
            this.btnProducts.Size = new System.Drawing.Size(111, 49);
            this.btnProducts.TabIndex = 19;
            this.btnProducts.Text = "Produkter";
            this.btnProducts.UseVisualStyleBackColor = true;
            // 
            // btnHistory
            // 
            this.btnHistory.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHistory.Location = new System.Drawing.Point(203, 129);
            this.btnHistory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(112, 49);
            this.btnHistory.TabIndex = 18;
            this.btnHistory.Text = "Tidigare fakturor";
            this.btnHistory.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rd0);
            this.groupBox1.Controls.Add(this.rd6);
            this.groupBox1.Controls.Add(this.rd12);
            this.groupBox1.Controls.Add(this.rd25);
            this.groupBox1.Location = new System.Drawing.Point(203, 26);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(225, 46);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Moms";
            // 
            // rd0
            // 
            this.rd0.AutoSize = true;
            this.rd0.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rd0.Location = new System.Drawing.Point(182, 17);
            this.rd0.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rd0.Name = "rd0";
            this.rd0.Size = new System.Drawing.Size(39, 17);
            this.rd0.TabIndex = 3;
            this.rd0.TabStop = true;
            this.rd0.Text = "0%";
            this.rd0.UseVisualStyleBackColor = true;
            // 
            // rd6
            // 
            this.rd6.AutoSize = true;
            this.rd6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rd6.Location = new System.Drawing.Point(125, 17);
            this.rd6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rd6.Name = "rd6";
            this.rd6.Size = new System.Drawing.Size(39, 17);
            this.rd6.TabIndex = 2;
            this.rd6.TabStop = true;
            this.rd6.Text = "6%";
            this.rd6.UseVisualStyleBackColor = true;
            // 
            // rd12
            // 
            this.rd12.AutoSize = true;
            this.rd12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rd12.Location = new System.Drawing.Point(63, 17);
            this.rd12.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rd12.Name = "rd12";
            this.rd12.Size = new System.Drawing.Size(45, 17);
            this.rd12.TabIndex = 1;
            this.rd12.TabStop = true;
            this.rd12.Text = "12%";
            this.rd12.UseVisualStyleBackColor = true;
            // 
            // rd25
            // 
            this.rd25.AutoSize = true;
            this.rd25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rd25.Location = new System.Drawing.Point(4, 17);
            this.rd25.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rd25.Name = "rd25";
            this.rd25.Size = new System.Drawing.Size(45, 17);
            this.rd25.TabIndex = 0;
            this.rd25.TabStop = true;
            this.rd25.Text = "25%";
            this.rd25.UseVisualStyleBackColor = true;
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.closeToolStripMenuItem.Text = "Stäng programmet";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutToolStripMenuItem.Text = "Om mig";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // countWordsToolStripMenuItem
            // 
            this.countWordsToolStripMenuItem.Name = "countWordsToolStripMenuItem";
            this.countWordsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.countWordsToolStripMenuItem.Text = "Räkna ord";
            this.countWordsToolStripMenuItem.Click += new System.EventHandler(this.CountWordsToolStripMenuItem_Click);
            // 
            // btnDeleteCustomer
            // 
            this.btnDeleteCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnDeleteCustomer.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteCustomer.Image")));
            this.btnDeleteCustomer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDeleteCustomer.Location = new System.Drawing.Point(319, 372);
            this.btnDeleteCustomer.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteCustomer.Name = "btnDeleteCustomer";
            this.btnDeleteCustomer.Size = new System.Drawing.Size(117, 119);
            this.btnDeleteCustomer.TabIndex = 17;
            this.btnDeleteCustomer.Text = "Ta bort en kund";
            this.btnDeleteCustomer.UseVisualStyleBackColor = true;
            // 
            // btnAddCustomer
            // 
            this.btnAddCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnAddCustomer.Image = ((System.Drawing.Image)(resources.GetObject("btnAddCustomer.Image")));
            this.btnAddCustomer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddCustomer.Location = new System.Drawing.Point(172, 372);
            this.btnAddCustomer.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddCustomer.Name = "btnAddCustomer";
            this.btnAddCustomer.Size = new System.Drawing.Size(117, 118);
            this.btnAddCustomer.TabIndex = 16;
            this.btnAddCustomer.Text = "Lägg till en kund";
            this.btnAddCustomer.UseVisualStyleBackColor = true;
            // 
            // btnEditCustomer
            // 
            this.btnEditCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnEditCustomer.Image = ((System.Drawing.Image)(resources.GetObject("btnEditCustomer.Image")));
            this.btnEditCustomer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEditCustomer.Location = new System.Drawing.Point(22, 372);
            this.btnEditCustomer.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditCustomer.Name = "btnEditCustomer";
            this.btnEditCustomer.Size = new System.Drawing.Size(117, 118);
            this.btnEditCustomer.TabIndex = 15;
            this.btnEditCustomer.Text = "Redigera kunduppgifter";
            this.btnEditCustomer.UseVisualStyleBackColor = true;
            // 
            // btnMyCompany
            // 
            this.btnMyCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnMyCompany.Image = ((System.Drawing.Image)(resources.GetObject("btnMyCompany.Image")));
            this.btnMyCompany.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMyCompany.Location = new System.Drawing.Point(319, 251);
            this.btnMyCompany.Margin = new System.Windows.Forms.Padding(2);
            this.btnMyCompany.Name = "btnMyCompany";
            this.btnMyCompany.Size = new System.Drawing.Size(117, 117);
            this.btnMyCompany.TabIndex = 14;
            this.btnMyCompany.Text = "Mitt företag";
            this.btnMyCompany.UseVisualStyleBackColor = true;
            // 
            // btnNewInvoice
            // 
            this.btnNewInvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnNewInvoice.Image = ((System.Drawing.Image)(resources.GetObject("btnNewInvoice.Image")));
            this.btnNewInvoice.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNewInvoice.Location = new System.Drawing.Point(319, 129);
            this.btnNewInvoice.Margin = new System.Windows.Forms.Padding(2);
            this.btnNewInvoice.Name = "btnNewInvoice";
            this.btnNewInvoice.Size = new System.Drawing.Size(117, 118);
            this.btnNewInvoice.TabIndex = 12;
            this.btnNewInvoice.Text = "Ny faktura";
            this.btnNewInvoice.UseVisualStyleBackColor = true;
            // 
            // InterfaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 511);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnProducts);
            this.Controls.Add(this.btnHistory);
            this.Controls.Add(this.btnDeleteCustomer);
            this.Controls.Add(this.btnAddCustomer);
            this.Controls.Add(this.btnEditCustomer);
            this.Controls.Add(this.btnMyCompany);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnNewInvoice);
            this.Controls.Add(this.customersBox);
            this.Controls.Add(this.menuStripInterface);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripInterface;
            this.Name = "InterfaceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PHOENIX Fakturering";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InterfaceForm_FormClosing);
            this.menuStripInterface.ResumeLayout(false);
            this.menuStripInterface.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripInterface;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ListBox customersBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbFAconto;
        private System.Windows.Forms.RadioButton rbFaktura;
        private System.Windows.Forms.Button btnProducts;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.Button btnDeleteCustomer;
        private System.Windows.Forms.Button btnAddCustomer;
        private System.Windows.Forms.Button btnEditCustomer;
        private System.Windows.Forms.Button btnMyCompany;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rd0;
        private System.Windows.Forms.RadioButton rd6;
        private System.Windows.Forms.RadioButton rd12;
        private System.Windows.Forms.RadioButton rd25;
        private System.Windows.Forms.Button btnNewInvoice;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem countWordsToolStripMenuItem;
    }
}