using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace WCFStreaming
{
    public class Service1 : IService1
    {
        private string dataPath = @"D:\GitHub repositories\WCFStreaming\dataServer\";
        private static Random random = new Random();
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        public string UploadFile(Stream input) {
            var newPath = dataPath + RandomString(5) + ".txt";
            StreamReader reader = new StreamReader(input);
            var content = reader.ReadToEnd();
            File.WriteAllText(newPath, content);
            return newPath;
        }

        public Stream DownloadFile(string fileName){

            if (File.Exists(dataPath+fileName + ".txt")){
                MemoryStream stream = new MemoryStream();
                var bytes = File.ReadAllBytes(dataPath+fileName + ".txt");
                stream.Write(bytes, 0, bytes.Length);
                stream.Position = 0;
                return stream;
            }

            else{
                return null;
            }
        }

        public string[] GetFilesList(){
            var filesList = Directory.GetFiles(dataPath, "*.txt");
            return filesList.Length > 0 ? filesList : new string[]{"There is no uploaded files yet!"};
         }

        public string MatrixMultiplication(string firstMatrixName, string secondMatrixName){
            var firstPath = dataPath + firstMatrixName + ".txt";
            var secondPath = dataPath + secondMatrixName + ".txt";
            if (!File.Exists(firstPath))
                return "There is no first file!" ; 
            if (!File.Exists(secondPath))
                return "There is no second file!";
            
            int[][] A = File.ReadAllLines(firstPath)
                   .Select(l => l.Split(' ').Select(i => int.Parse(i)).ToArray())
                   .ToArray();

            int[][] B = File.ReadAllLines(secondPath)
                   .Select(l => l.Split(' ').Select(i => int.Parse(i)).ToArray())
                   .ToArray();


            int rowSizeA = A.Length;
            int rowSizeB = B.Length;
            int columnSizeA = A[0].Length;
            int columnSizeB = B[0].Length;

            int temp = 0;
            int [][] kHasil = new int[rowSizeA][];
            if (columnSizeA != rowSizeB){
                return "dimensions does not match";
            }
            else{
                for(int i = 0; i < kHasil.Length; i++){
                    kHasil[i] = new int[columnSizeB];
                }
                Parallel.For(0, rowSizeA, i =>
                {
                    for (int j = 0; j < columnSizeB; j++)
                    {
                        temp = 0;
                        for (int k = 0; k < columnSizeA; k++)
                        {
                            temp += A[i][k] * B[k][j];
                        }
                        kHasil[i][j] = temp;
                    }
                });

                var fileName = "mul_" + RandomString(7) + ".txt";
                File.WriteAllLines(dataPath + fileName, kHasil
                    .Select(line => String.Join(" ", line)));

                return fileName;
            }
        }
        public string Mandelbrot(MandelbrotData data)
        {
           
            var filename = dataPath + RandomString(7) + ".pgm";
            

            int iXmax = data.size;
            int iYmax = data.size;

  
            /* Zoom in */
            double CxMin = -2.5 + data.xOffset;
            double CxMax = 1.5 + data.xOffset;
            double CyMin = -2.0 + data.yOffset;
            double CyMax = 2.0 + data.yOffset;
           

            double PixelWidth = (CxMax - CxMin) / iXmax;
            double PixelHeight = (CyMax - CyMin) / iYmax;

            File.WriteAllLines(filename, new string[] {$"P2\n#com\n{iXmax} {iYmax}\n255" });

 
            const int IterationMax = 100;

            int[][] image = new int[iYmax][];
            for (int i = 0; i < image.Length; i++)
                image[i] = new int[iXmax];
            
            const double EscapeRadius = 2;
            double ER2 = EscapeRadius * EscapeRadius;
            int threadsCount = Environment.ProcessorCount;
            Parallel.For(0, iYmax-1, new ParallelOptions { MaxDegreeOfParallelism = threadsCount }, (iY,state) =>
            {

                double Zx, Zy;
                double Zx2, Zy2;
                double Cx, Cy;
                int Iteration;
                Cy = CyMin + iY * PixelHeight;
                if (Math.Abs(Cy) < PixelHeight / 2) Cy = 0.0;
                for (int iX = 0; iX < iXmax-1; iX++)
                {
                    Cx = CxMin + iX * PixelWidth;
                    Zx = 0.0;
                    Zy = 0.0;
                    Zx2 = Zx * Zx;
                    Zy2 = Zy * Zy;

                    for (Iteration = 0; Iteration < IterationMax && ((Zx2 + Zy2) < ER2); Iteration++)
                    {
                        Zy = 2 * Zx * Zy + Cy;
                        Zx = Zx2 - Zy2 + Cx;
                        Zx2 = Zx * Zx;
                        Zy2 = Zy * Zy;
                    };

                    if (Iteration == IterationMax)
                        image[iY][iX] = ((int)255 / threadsCount) * (int)Task.CurrentId;

                    else
                        image[iY][iX] = 0;

                }
            });
            File.AppendAllLines(filename, image
                    .Select(line => String.Join(" ", line)));
            return filename;
        }


        }
}
