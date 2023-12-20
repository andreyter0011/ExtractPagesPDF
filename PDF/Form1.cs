using System.Text;
using PdfSharp.Pdf.IO;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.Text.RegularExpressions;

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
            btnSearchAndRename.Visible = false;
        }
        private void ShowControls()
        {
            btnExtractPages.Visible = true;
            txtPdfPath.Visible = true;
            numericUpDownPagesPerFile.Visible = true;
            label1.Visible = true;
            txtSearchPhrase.Visible = true;
            labelSearchPhrase.Visible = true;
            btnSearchAndRename.Visible = true;
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
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                using (PdfSharp.Pdf.PdfDocument inputDocument = PdfSharp.Pdf.IO.PdfReader.Open(pdfPath, PdfDocumentOpenMode.Import))
                {
                    int totalPages = inputDocument.PageCount;

                    for (int startPage = 0; startPage < totalPages; startPage += pagesPerFile)
                    {
                        int endPage = Math.Min(startPage + pagesPerFile - 1, totalPages - 1);
                        string outputFilePath = Path.Combine(Path.GetDirectoryName(pdfPath), $"{Path.GetFileNameWithoutExtension(pdfPath)}_{startPage + 1}-{endPage + 1}.pdf");

                        using (PdfSharp.Pdf.PdfDocument outputDocument = new PdfSharp.Pdf.PdfDocument())
                        {
                            for (int currentPage = startPage; currentPage <= endPage; currentPage++)
                            {
                                outputDocument.AddPage(inputDocument.Pages[currentPage]);
                            }

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

        private void btnSearchAndRename_Click(object sender, EventArgs e)
        {
            string pdfPath = txtPdfPath.Text;

            if (string.IsNullOrEmpty(pdfPath) || !File.Exists(pdfPath))
            {
                MessageBox.Show("Пожалуйста, выберите PDF файл.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string searchPhrase = txtSearchPhrase.Text.Trim();

            if (string.IsNullOrEmpty(searchPhrase))
            {
                MessageBox.Show("Пожалуйста, введите фразу для поиска.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (iText.Kernel.Pdf.PdfReader pdfReader = new iText.Kernel.Pdf.PdfReader(pdfPath))
                {
                    using (iText.Kernel.Pdf.PdfDocument pdfDocument = new iText.Kernel.Pdf.PdfDocument(pdfReader))
                    {
                        for (int pageNum = 1; pageNum <= pdfDocument.GetNumberOfPages(); pageNum++)
                        {
                            var strategy = new SimpleTextExtractionStrategy();
                            string currentPageText = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(pageNum), strategy);

                            int startIndex = currentPageText.IndexOf(searchPhrase);

                            if (startIndex != -1)
                            {
                                int endIndex = startIndex + searchPhrase.Length + 10;
                                string newName = currentPageText.Substring(startIndex, endIndex - startIndex).Trim();
                                string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
                                string validName = Regex.Replace(newName, "[" + invalidChars + "]", "");
                                string newFilePath = Path.Combine(Path.GetDirectoryName(pdfPath), $"{validName}.pdf");

                                pdfDocument.Close();
                                pdfReader.Close();

                                File.Move(pdfPath, newFilePath);

                                MessageBox.Show($"Файл успешно переименован в {validName}.pdf", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                return;
                            }
                        }

                        MessageBox.Show("Фраза не найдена в PDF файле.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
 

