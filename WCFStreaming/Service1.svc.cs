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
        public string Mandelbrot()
        {
            var filename = dataPath + RandomString(7) + ".pgm";
            int iX, iY;

            const int iXmax = 300;
            const int iYmax = 300;

  
            /* Zoom in */
            const double CxMin = -2.5;
            const double CxMax = 1.5;
            const double CyMin = -2.0;
            const double CyMax = 2.0;
            double Cx, Cy;

            double PixelWidth = (CxMax - CxMin) / iXmax;
            double PixelHeight = (CyMax - CyMin) / iYmax;

            File.WriteAllLines(filename, new string[] {$"P2\n#com\n{iXmax} {iYmax}\n255" });

            double Zx, Zy;
            double Zx2, Zy2; 
  
            int Iteration;
            const int IterationMax = 50;

            const double EscapeRadius = 2;
            double ER2 = EscapeRadius * EscapeRadius;
            for (iY = 0; iY < iYmax; iY++)
            {
                Cy = CyMin + iY * PixelHeight;
                if (Math.Abs(Cy) < PixelHeight / 2) Cy = 0.0;
                for (iX = 0; iX < iXmax; iX++)
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
                        File.AppendAllText(filename, "0 " );
    
                    else
                        File.AppendAllText(filename, "255 " );

                }
                File.AppendAllText(filename, "\n");
            }

            return filename;
        }


        }
}
