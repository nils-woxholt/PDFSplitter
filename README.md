# PDFSplitter
C# console app to split PDF documents into individual pages

Uses the following NuGet libraries:
- CommandLineParser https://github.com/commandlineparser/commandline
- PDFSharp http://www.pdfsharp.net/MainPage.ashx

Usage:
- Call the console app by passing -f and then the folder name.
- The app looks in that folder, finds any PDF files and splits them into a file per page.
- The split pages are saved into a new "output" folder in that folder.
