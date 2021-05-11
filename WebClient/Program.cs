using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClient{
    class Program{
        static string path = @"D:\GitHub repositories\WCFStreaming\dataClient\";

        public class App{
            public string UploadFile(){
                ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
                Console.WriteLine("Enter file name which you want to upload");
                var fileName = Console.ReadLine();
                fileName = path + fileName + ".txt";

                if (File.Exists(fileName)){
                    var ret = client.UploadFile(File.Open(fileName, FileMode.Open));
                    Console.WriteLine("Uploaded file name: " + ret);
                    return ret;
                }
                else{
                    Console.WriteLine("File does not exist");
                    return null;
                }
            }

            public string[] GetClientFilesList()
            {
                var filesList = Directory.GetFiles(path, "*.txt");
                return filesList.Length > 0 ? filesList : new string[] { "There is no uploaded files yet!" };
            }
            public string DownloadFile()
            {
                ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
                Console.WriteLine("Enter file name which you want to download");
                var fileName = Console.ReadLine();           
                var stream = client.DownloadFile(fileName);
                if(stream is null)
                {
                    Console.WriteLine("File does not exist");
                    return null;
                }
              
                StreamReader reader = new StreamReader(stream);
                var content = reader.ReadToEnd();
                File.WriteAllText(path + fileName + ".txt", content);
                return path + fileName + ".txt" ;
            }

            public void PrintList(string [] list)
            {
                Console.WriteLine();
                foreach (var l in list)
                    Console.WriteLine(l);
                return;
            }

            public ConsoleKeyInfo DisplayMenu()
            {

                Console.WriteLine();
                Console.WriteLine("1. Print client files list");
                Console.WriteLine("2. Print server files list");
                Console.WriteLine("3. Upload file");
                Console.WriteLine("4. Download file");
                Console.WriteLine("5. Matrix multiplication");
                Console.WriteLine("6. Mandelbrott");
                Console.WriteLine("7. Exit");
                return Console.ReadKey(false);
            }




        }

        static void Main(string[] args) {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            App app = new App();
            ConsoleKeyInfo key;
            do
            {
             
                key = app.DisplayMenu();
                switch (key.KeyChar.ToString())
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("\nClient files:");     
                        app.PrintList(app.GetClientFilesList());
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("\nServer files:");
                        app.PrintList(client.GetFilesList());
                        break;
                    case "3":
                        Console.Clear();
                        app.UploadFile();
                        break;
                    case "4":
                        Console.Clear();
                        app.DownloadFile();
                        break;

                    default:
                        Console.Clear();
          
                        break;
                }
               
            } while (key.Key != ConsoleKey.Escape);



        }
    }
}
