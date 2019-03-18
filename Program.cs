using CommandLine;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.IO;

namespace PDFSplitter
{
    // Define a class to receive parsed values
    class Options
    {
        [Option('f', "folder", HelpText = "the folder containing the pdf files to split", Required = true)]
        public string folder { get; set; }

    }

    class Program
    {
       
        static void Main(string[] args)
        {


            Parser.Default.ParseArguments<Options>(args)
                               .WithParsed<Options>(o =>
                               {
                                   processFolder(o.folder);
                               });
        }

        private static void processFolder(string folder)
        {
            string[] files = Directory.GetFiles(folder, "*.pdf");

            string outputPath = Path.Combine(folder, "ouput");

            // Make output folder
            if (Directory.Exists(outputPath))
            {
                Directory.Delete(outputPath, true);
            }

            Directory.CreateDirectory(outputPath);

            foreach (var file in files)
            {
                try
                {
                    splitPDF(file, outputPath);
                    Console.WriteLine("Processed: " + Path.GetFileName(file));
                }
                catch (Exception)
                {

                    Console.WriteLine("ERROR: Unable to process: " + Path.GetFileName(file));
                }
               
            }

        }

        private static void splitPDF(string filename, string outputPath)
        {
            string name = Path.GetFileNameWithoutExtension(filename);
           


            // Open the file
            PdfDocument inputDocument = PdfReader.Open(filename, PdfDocumentOpenMode.Import);

            if (inputDocument.PageCount > 1)
            {
               
                for (int idx = 0; idx < inputDocument.PageCount; idx++)
                {
                    // Create new document
                    PdfDocument outputDocument = new PdfDocument();
                    outputDocument.Version = inputDocument.Version;
                    outputDocument.Info.Title =
                      String.Format("Page {0} of {1}", idx + 1, inputDocument.Info.Title);
                    outputDocument.Info.Creator = inputDocument.Info.Creator;

                    // Add the page and save it
                    outputDocument.AddPage(inputDocument.Pages[idx]);
                    outputDocument.Save(Path.Combine(outputPath, String.Format("{0} - Page {1}_split.pdf", name, idx + 1)));
                }
            }
            else
            {
                // just copy the file
                File.Copy(filename, Path.Combine(outputPath,name));
            }
            
        }
    }
}
