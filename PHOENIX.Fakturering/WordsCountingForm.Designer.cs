namespace PHOENIX.Fakturering
{
    partial class WordsCountingForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WordsCountingForm));
            this.richTextBoxWords = new System.Windows.Forms.RichTextBox();
            this.btnCount = new System.Windows.Forms.Button();
            this.contextMenuStripWord = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripWord.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxWords
            // 
            this.richTextBoxWords.ContextMenuStrip = this.contextMenuStripWord;
            this.richTextBoxWords.Location = new System.Drawing.Point(10, 11);
            this.richTextBoxWords.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBoxWords.Name = "richTextBoxWords";
            this.richTextBoxWords.Size = new System.Drawing.Size(543, 340);
            this.richTextBoxWords.TabIndex = 3;
            this.richTextBoxWords.Text = "";
            // 
            // btnCount
            // 
            this.btnCount.Location = new System.Drawing.Point(461, 362);
            this.btnCount.Margin = new System.Windows.Forms.Padding(2);
            this.btnCount.Name = "btnCount";
            this.btnCount.Size = new System.Drawing.Size(92, 35);
            this.btnCount.TabIndex = 2;
            this.btnCount.Text = "Count";
            this.btnCount.UseVisualStyleBackColor = true;
            this.btnCount.Click += new System.EventHandler(this.BtnCount_Click);
            // 
            // contextMenuStripWord
            // 
            this.contextMenuStripWord.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pasteToolStripMenuItem});
            this.contextMenuStripWord.Name = "contextMenuStripWord";
            this.contextMenuStripWord.Size = new System.Drawing.Size(181, 48);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.pasteToolStripMenuItem.Text = "Klastra in";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.PasteToolStripMenuItem_Click);
            // 
            // WordsCountingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 407);
            this.Controls.Add(this.richTextBoxWords);
            this.Controls.Add(this.btnCount);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(580, 446);
            this.MinimumSize = new System.Drawing.Size(580, 446);
            this.Name = "WordsCountingForm";
            this.Text = "Räkna ord";
            this.Load += new System.EventHandler(this.WordsCountingForm_Load);
            this.contextMenuStripWord.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxWords;
        private System.Windows.Forms.Button btnCount;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripWord;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
    }
}