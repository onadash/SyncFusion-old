using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace PdfDataExtract
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string? pdfFilePath;
        private string? csvFilePath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void UploadPdfFile_Click(object sender, RoutedEventArgs e)
        {
            pdfFilePath = OpenFilePicker("PDF Files (*.pdf)|*.pdf");
            if (pdfFilePath != null)
                PdfFileNameText.Text = Path.GetFileName(pdfFilePath);
        }

        private void UploadCsvFile_Click(object sender, RoutedEventArgs e)
        {
            csvFilePath = OpenFilePicker("CSV Files (*.csv)|*.csv");
            if (csvFilePath != null)
                CsvFileNameText.Text = Path.GetFileName(csvFilePath);
        }

        private static string? OpenFilePicker(string filter)
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = filter
            };
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            return null;
        }

        private void ProcessFiles_Click(object sender, RoutedEventArgs e)
        {
            // Check if both files have been uploaded
            if (string.IsNullOrEmpty(pdfFilePath) || string.IsNullOrEmpty(csvFilePath))
            {
                ResultText.Text = "Please upload both files.";
                return;
            }

            string pdfText = TextExtract.PdfExtract(pdfFilePath);
            var searchData = TextExtract.CsvExtract(csvFilePath);

            var containsData = DataSearch.ContainsData(pdfText, searchData);

            // Display processed result
            ResultText.Text = containsData ? "Pdf file contains searching data" : "Pdf file doesn't contain searching data";
        }
    }
}