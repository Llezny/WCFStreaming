using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFStreaming;

namespace WebClient
{
    class Program
    {
        static string path = @"D:\GitHub repositories\WCFStreaming\dataClient\";

        public class App
        {
            //https://www.somacon.com/p576.php
            public string GetBytesReadable(long i)
            {
                // Get absolute value
                long absolute_i = (i < 0 ? -i : i);
                // Determine the suffix and readable value
                string suffix;
                double readable;
                if (absolute_i >= 0x1000000000000000) // Exabyte
                {
                    suffix = "EB";
                    readable = (i >> 50);
                }
                else if (absolute_i >= 0x4000000000000) // Petabyte
                {
                    suffix = "PB";
                    readable = (i >> 40);
                }
                else if (absolute_i >= 0x10000000000) // Terabyte
                {
                    suffix = "TB";
                    readable = (i >> 30);
                }
                else if (absolute_i >= 0x40000000) // Gigabyte
                {
                    suffix = "GB";
                    readable = (i >> 20);
                }
                else if (absolute_i >= 0x100000) // Megabyte
                {
                    suffix = "MB";
                    readable = (i >> 10);
                }
                else if (absolute_i >= 0x400) // Kilobyte
                {
                    suffix = "KB";
                    readable = i;
                }
                else
                {
                    return i.ToString("0 B"); // Byte
                }
                readable = (readable / 1024);
                return readable.ToString("0.### ") + suffix;
            }


            public string UploadFile()
            {
                ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
                Console.WriteLine("Enter file name which you want to upload");
                var fileName = Console.ReadLine();
                fileName = path + fileName + ".txt";

                if (File.Exists(fileName))
                {
                    var file = File.Open(fileName, FileMode.Open);
                    var ret = client.UploadFile(file);
                    Console.WriteLine("Uploaded file name: " + ret);
                    return ret;
                }
                else
                {
                    Console.WriteLine("File does not exist");
                    return null;
                }
            }

            public string[] GetClientFilesList()
            {
                var filesList = Directory.GetFiles(path);
                return filesList.Length > 0 ? filesList : new string[] { "There is no uploaded files yet!" };
            }
            public string DownloadFile()
            {
                ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
                Console.WriteLine("Enter file name which you want to download");
                var fileName = Console.ReadLine();
               
                if (!File.Exists(fileName))
                {
                    Console.WriteLine("File does not exist");
                    return null;
                }
                var stream = client.DownloadFile(fileName);
                StreamReader reader = new StreamReader(stream);
                var content = reader.ReadToEnd();
                File.WriteAllText(path + fileName + ".txt", content);
                return path + fileName + ".txt";
            }


            public void PrintClientFiles()
            {
                DirectoryInfo di = new DirectoryInfo(path);
                FileInfo[] fiArr = di.GetFiles();
                foreach (FileInfo f in fiArr)
                    Console.WriteLine($"The size of { f.Name} is {GetBytesReadable(f.Length)}.");
            }


            public void PrintServerFiles()
            {
                ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
                var fileList = client.GetFilesList();
                foreach (FileInfo f in fileList)
                    Console.WriteLine($"The size of { f.Name} is {GetBytesReadable(f.Length)}.");
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

            public string MatrixMultiplication()
            {
                ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();

                this.PrintClientFiles();

                Console.WriteLine("Enter name of first matrix");
                var matrixNameA = Console.ReadLine();

                Console.WriteLine("Enter name of second matrix");
                var matrixNameB = Console.ReadLine();

                return client.MatrixMultiplication(matrixNameA, matrixNameB);
            }

            public string Mandelbrot()
            {
                ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
                MandelbrotData data = new MandelbrotData();

                Console.WriteLine("Enter size of image");
                data.size = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter x offset");
                data.xOffset = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter y offset");
                data.yOffset = int.Parse(Console.ReadLine());

                Console.WriteLine("Generating mandelbrot image...");

                return client.Mandelbrot(data);
            }

            static void Main(string[] args)
            {
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
                            app.PrintClientFiles();
                            break;
                        case "2":
                            Console.Clear();
                            Console.WriteLine("\nServer files:");
                            app.PrintServerFiles();
                            break;
                        case "3":
                            Console.Clear();
                            Console.WriteLine("\nClient files:");
                            app.PrintClientFiles();
                            app.UploadFile();
                            break;
                        case "4":
                            Console.Clear();
                            Console.WriteLine("\nServer files:");
                            app.PrintServerFiles();
                            app.DownloadFile();
                            break;
                        case "5":
                            Console.Clear();
                            Console.WriteLine("\nFile name: " + app.MatrixMultiplication());
                            break;

                        case "6":
                            Console.Clear();
                            Console.WriteLine("\nFile name: " + app.Mandelbrot());
                            break;

                        case "7":
                            Console.Clear();
                            return;

                        default:
                            Console.Clear();
                            break;
                    }

                } while (key.Key != ConsoleKey.Escape);


                
            }
        }
    }
}
