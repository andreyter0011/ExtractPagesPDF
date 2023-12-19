namespace PDF
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnLoadPdf = new Button();
            openFileDialog1 = new OpenFileDialog();
            numericUpDownPagesPerFile = new NumericUpDown();
            btnExtractPages = new Button();
            txtPdfPath = new TextBox();
            label1 = new Label();
            txtSearchPhrase = new TextBox();
            labelSearchPhrase = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPagesPerFile).BeginInit();
            SuspendLayout();
            // 
            // btnLoadPdf
            // 
            btnLoadPdf.Location = new Point(295, 8);
            btnLoadPdf.Name = "btnLoadPdf";
            btnLoadPdf.Size = new Size(177, 47);
            btnLoadPdf.TabIndex = 0;
            btnLoadPdf.Text = "Выбрать PDF";
            btnLoadPdf.UseVisualStyleBackColor = true;
            btnLoadPdf.Click += btnLoadPdf_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // numericUpDownPagesPerFile
            // 
            numericUpDownPagesPerFile.Location = new Point(25, 105);
            numericUpDownPagesPerFile.Name = "numericUpDownPagesPerFile";
            numericUpDownPagesPerFile.Size = new Size(164, 23);
            numericUpDownPagesPerFile.TabIndex = 1;
            // 
            // btnExtractPages
            // 
            btnExtractPages.Location = new Point(295, 144);
            btnExtractPages.Name = "btnExtractPages";
            btnExtractPages.Size = new Size(177, 49);
            btnExtractPages.TabIndex = 2;
            btnExtractPages.Text = "Извлечь и сохранить";
            btnExtractPages.UseVisualStyleBackColor = true;
            btnExtractPages.Click += btnExtractPages_Click;
            // 
            // txtPdfPath
            // 
            txtPdfPath.Location = new Point(25, 61);
            txtPdfPath.Name = "txtPdfPath";
            txtPdfPath.ReadOnly = true;
            txtPdfPath.Size = new Size(763, 23);
            txtPdfPath.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 87);
            label1.Name = "label1";
            label1.Size = new Size(119, 15);
            label1.TabIndex = 4;
            label1.Text = "количество страниц";
            // 
            // txtSearchPhrase
            // 
            txtSearchPhrase.Location = new Point(25, 242);
            txtSearchPhrase.Name = "txtSearchPhrase";
            txtSearchPhrase.Size = new Size(100, 23);
            txtSearchPhrase.TabIndex = 5;
            // 
            // labelSearchPhrase
            // 
            labelSearchPhrase.AutoSize = true;
            labelSearchPhrase.Location = new Point(25, 279);
            labelSearchPhrase.Name = "labelSearchPhrase";
            labelSearchPhrase.Size = new Size(38, 15);
            labelSearchPhrase.TabIndex = 6;
            labelSearchPhrase.Text = "label2";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(800, 394);
            Controls.Add(labelSearchPhrase);
            Controls.Add(txtSearchPhrase);
            Controls.Add(label1);
            Controls.Add(txtPdfPath);
            Controls.Add(btnExtractPages);
            Controls.Add(numericUpDownPagesPerFile);
            Controls.Add(btnLoadPdf);
            Name = "Form1";
            RightToLeft = RightToLeft.No;
            Text = "PDF";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDownPagesPerFile).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLoadPdf;
        private OpenFileDialog openFileDialog1;
        private NumericUpDown numericUpDownPagesPerFile;
        private Button btnExtractPages;
        private TextBox txtPdfPath;
        private Label label1;
        private TextBox txtSearchPhrase;
        private Label labelSearchPhrase;
    }
}
