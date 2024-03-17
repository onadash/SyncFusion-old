using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using System.IO;

namespace PdfDataExtract
{
    internal class TextExtract
    {
        public TextExtract() { }

        public string PdfExtract(string filePath) {
            
            FileStream docStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            //Load the PDF document.
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(docStream);
            // Loading page collections
            PdfLoadedPageCollection loadedPages = loadedDocument.Pages;

            string extractedText = string.Empty;
            // Extract text from existing PDF document pages
            foreach (PdfLoadedPage loadedPage in loadedPages)
            {
                extractedText += loadedPage.ExtractText();
            }
            //Close the document.
            loadedDocument.Close(true);
            return extractedText;

        }

        public IEnumerable<(string?, string)> CsvExtract(string csvPath) {

            var separator = ";";

            var lines = File.ReadLines(csvPath);

            var indexedKeys = lines
                .First()
                .Split(separator, StringSplitOptions.None)
                .ToList();

            var values = lines
                .Skip(1)
                .SelectMany(l=>l.Split(separator, StringSplitOptions.None).Select((k, i) =>
            (
                Index: i,
                Value: k
            ))
            );

            var result = values
               .Select((ik) =>
            (
                indexedKeys[ik.Index],
                ik.Value
            ));
            return result;
        }
    }
}
