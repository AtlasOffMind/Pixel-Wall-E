using System.Diagnostics;

namespace File_reader
{
    public static class TextReader
    {
        public static string? text;



        public static void TextRead(string fileName)
        {
            fileName = Path.Combine(@"C:\My things\Git\2nd Proyect Pixel Wall-E\Compiler\Module\Parser\", fileName);

            var file = File.ReadAllBytes(fileName);
            var stream = new MemoryStream(file);

            // Open the text file using a stream reader.
            StreamReader reader = new(stream);

            // Read the stream as a string.
            text = reader.ReadToEnd();            
        }

        public static void PrintText() => Console.WriteLine(text);        
    }
}