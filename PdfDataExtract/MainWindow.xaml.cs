using System.Windows;
using System.Text.RegularExpressions;

namespace PdfDataExtract
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var pdfPath = @"Faktura2.pdf";
            var csvPath = @"Dane do wyszukania.csv";

            TextExtract textExtract = new TextExtract();
            string pdfText = textExtract.PdfExtract(pdfPath);
            var searchData = textExtract.CsvExtract(csvPath);

            foreach ( var dataItem in searchData )
            {
                string searchPattern = dataItem.Item1 + "[^a-zA-Z0-9]*" + dataItem.Item2;
                if (Regex.IsMatch(pdfText, @searchPattern))
                    return;
            }
        }
    }
}