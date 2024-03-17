using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using System.IO;

namespace PdfDataExtract
{
    public class TextExtract
    {

        public static string PdfExtract(string filePath)
        {
            
            FileStream docStream = new(filePath, FileMode.Open, FileAccess.Read);

            //Load the PDF document.
            PdfLoadedDocument loadedDocument = new (docStream);
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

        public static IEnumerable<(string, string)> CsvExtract(string csvPath) {

            var separator = ";";

            var lines = File.ReadLines(csvPath);

            if (lines.Any())
            {
                string[] indexedKeys = lines
                    .First()
                    .Split(separator, StringSplitOptions.None);

                var values = lines
                    .Skip(1)
                    .SelectMany(l => l.Split(separator, StringSplitOptions.None).Select((k, i) =>
                (
                    Index: i,
                    Value: k
                ))
                ).Where(l => l.Index <= indexedKeys.Length);

                IEnumerable<(string, string)> result = values
                   .Select((ik) =>
                (
                    indexedKeys[ik.Index],
                    ik.Value
                ));
                return result;
            }
            return [];
        }
    }
}
