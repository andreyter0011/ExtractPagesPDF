using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace PDF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            HideControls();
        }
        private void HideControls()
        {
            btnExtractPages.Visible = false;
            txtPdfPath.Visible = false;
            numericUpDownPagesPerFile.Visible = false;
            label1.Visible = false;
            txtSearchPhrase.Visible = false;
            labelSearchPhrase.Visible = false;
        }
        private void ShowControls()
        {
            btnExtractPages.Visible = true;
            txtPdfPath.Visible = true;
            numericUpDownPagesPerFile.Visible = true;
            label1.Visible = true;
            txtSearchPhrase.Visible = true;
            labelSearchPhrase.Visible = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnLoadPdf_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "PDF Files|*.pdf";
                openFileDialog.Title = "Выбран PDF файл";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtPdfPath.Text = openFileDialog.FileName;
                    ShowControls();
                }
            }
        }

        private void btnExtractPages_Click(object sender, EventArgs e)
        {
            string pdfPath = txtPdfPath.Text;

            if (string.IsNullOrEmpty(pdfPath) || !File.Exists(pdfPath))
            {
                MessageBox.Show("Пожалуйста, выберите PDF файл.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int pagesPerFile = (int)numericUpDownPagesPerFile.Value;

            try
            {
                // Устанавливаем глобальную кодировку для приложения
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                using (PdfDocument inputDocument = PdfReader.Open(pdfPath, PdfDocumentOpenMode.Import))
                {
                    int totalPages = inputDocument.PageCount;

                    for (int startPage = 0; startPage < totalPages; startPage += pagesPerFile)
                    {
                        int endPage = Math.Min(startPage + pagesPerFile - 1, totalPages - 1);
                        string outputFilePath = Path.Combine(Path.GetDirectoryName(pdfPath), $"{Path.GetFileNameWithoutExtension(pdfPath)}_{startPage + 1}-{endPage + 1}.pdf");

                        using (PdfDocument outputDocument = new PdfDocument())
                        {
                            for (int currentPage = startPage; currentPage <= endPage; currentPage++)
                            {
                                outputDocument.AddPage(inputDocument.Pages[currentPage]);
                            }

                            // Сохраняем файл с использованием глобальной кодировки
                            outputDocument.Save(outputFilePath);
                        }
                    }
                }

                MessageBox.Show("Страницы извлечены и успешно сохранены!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
