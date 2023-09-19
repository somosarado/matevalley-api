using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Microsoft.Office.Interop.Word;
using Application = Microsoft.Office.Interop.Word.Application;

namespace Events.PrinterApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Label label = new Label();
            label.Generate();
            //string wordPath = "C:\\Users\\rmore\\OneDrive\\Projects\\MateValley\\Repos\\Api\\Events.PrinterApp\\Resources\\labelEdit.docx";
            //// Create a new instance of Microsoft Word
            //Application wordApp = new Application();

            //try
            //{
            //    // Open the Word document
            //    Document doc = wordApp.Documents.Open(wordPath);

            //    // Select the text that you want to delete.
            //    Range range = doc.Range(0, 0);

            //    // Delete the text.
            //    range.Delete();

            //    // Select the text that you want to edit.
            //    range = doc.Range(0, 20);

            //    // Change the text to "Hello, world!".
            //    range.Text = "Santiago";

                
            //    // Save the Word document.
            //    doc.Save();
            //    //doc.SaveAs(@"C:\Users\rmore\OneDrive\Projects\MateValley\Repos\Api\Events.PrinterApp\Resources\labelEdit_copy.docx");
            //    // Print the document.
            //    doc.PrintOut();

            //    // Make changes to the document
            //    // For example, add text to the document:
            //    //doc.Content.Text = "test";

            //    //// Save the changes
            //    //doc.Save();

            //    //// Close the document
            //    //doc.Close();

            //    // Quit Word application
            //    wordApp.Quit();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error: " + ex.Message);
            //}
            //finally
            //{
            //    // Release COM objects
            //    //if (doc != null)
            //    //    Marshal.ReleaseComObject(doc);
            //    if (wordApp != null)
            //        Marshal.ReleaseComObject(wordApp);
            //}
        }
    }
}
