using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFStreaming
{
    // UWAGA: możesz użyć polecenia „Zmień nazwę” w menu „Refaktoryzuj”, aby zmienić nazwę interfejsu „IService1” w kodzie i pliku konfiguracji.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string MatrixMultiplication(string firstMatrixName, string secondMatrixName);


        [OperationContract]
        string Mandelbrot();


        [OperationContract]
        string UploadFile(Stream input);


        [OperationContract]
        Stream DownloadFile(string file);


        [OperationContract]
        string[] GetFilesList();
        


        }



}
