using Microsoft.Office.Interop.Word;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Events.PrinterApp
{
    public class Label
    {

        public async System.Threading.Tasks.Task Generate()
        {
            //get labels for print
            var labels = await this.GetLabels();
            if(labels == null)
                return;
            foreach (var label in labels)
            {
                //generate word
                GenerateWordPrint(label.Id, label.Name);
            }
          
            
        
        
        }

        private async Task<List<Assistant>>  GetLabels()
        {
            try
            {
                string url = "https://localhost:7169/Eventos/GetLabelsForPrint";
                var httpClientHandler = new HttpClientHandler();

                httpClientHandler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) => { return true; };

                using (HttpClient client = new HttpClient(httpClientHandler))
                {
                    //ErrorLog += $"Inicio is proccesing con url {url}";
                    //client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
                    client.DefaultRequestHeaders.Accept.TryParseAdd("application/json");

                    HttpResponseMessage response = await client.GetAsync(url);
                   // ErrorLog += $"Hice Get Async";
                    string data = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                       // ErrorLog += $"Recibi data";
                        var datos = JsonConvert.DeserializeObject<List<Assistant>>(data);
                        return datos;
                    }
                    else
                    {
                       // ErrorLog += $"Recibi error";
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLog += $"Error Ejecutando get order {ex.Message}";
                return null;
            }
        }

        public void GenerateWordPrint(int id, string name)
        {
            try
            {
              //  string wordPath = "C:\\Users\\rmore\\OneDrive\\Projects\\MateValley\\Repos\\Api\\Events.PrinterApp\\Resources\\labelEdit.docx";
                string path = "C:\\Users\\rmore\\OneDrive\\Projects\\MateValley\\Repos\\Api\\Events.PrinterApp\\Resources\\";
                string fileName = $"label_{id}_{name}_{DateTime.Now.ToString("ddMMyyyyhhmmss")}.docx";

                HacerCopia(id, name, path, fileName);
                // Create a new instance of Microsoft Word
                Application wordApp = new Application();

                try
                {
                    // Open the Word document
                    Document doc = wordApp.Documents.Open(path + fileName);

                    // Select the text that you want to delete.
                    Range range = doc.Range(0, 0);

                    // Delete the text.
                    //range.Delete();

                    //// Select the text that you want to edit.
                    //range = doc.Range(0, 20);

                    // Change the text to "Hello, world!".
                    range.Text = name;


                    // Save the Word document.
                    doc.Save();
                    //doc.SaveAs(@"C:\Users\rmore\OneDrive\Projects\MateValley\Repos\Api\Events.PrinterApp\Resources\labelEdit_copy.docx");
                    // Print the document.
                    doc.PrintOut();

                    // Make changes to the document
                    // For example, add text to the document:
                    //doc.Content.Text = "test";

                    //// Save the changes
                    //doc.Save();

                    //// Close the document
                    //doc.Close();

                    // Quit Word application
                    wordApp.Quit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    // Release COM objects
                    //if (doc != null)
                    //    Marshal.ReleaseComObject(doc);
                    if (wordApp != null)
                        Marshal.ReleaseComObject(wordApp);
                }
            } 
            catch 
            {
                
            }
        }

        private void HacerCopia(int id, string name, string path, string fileName)
        {
            string wordPath = "C:\\Users\\rmore\\OneDrive\\Projects\\MateValley\\Repos\\Api\\Events.PrinterApp\\Resources\\labelEdit.docx";
            // Create a new instance of Microsoft Word
            Application wordApp = new Application();

            try
            {
                
                // Open the Word document
                Document doc = wordApp.Documents.Open(wordPath);
                // Save copy
                doc.SaveAs(path + fileName);
                // Print the document.
               

                // Quit Word application
                wordApp.Quit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                // Release COM objects
                //if (doc != null)
                //    Marshal.ReleaseComObject(doc);
                if (wordApp != null)
                    Marshal.ReleaseComObject(wordApp);
            }
        }
    }
}
