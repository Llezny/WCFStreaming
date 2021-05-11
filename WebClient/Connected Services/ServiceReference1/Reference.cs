﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebClient.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/MatrixMultiplication", ReplyAction="http://tempuri.org/IService1/MatrixMultiplicationResponse")]
        string MatrixMultiplication(string firstMatrixName, string secondMatrixName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/MatrixMultiplication", ReplyAction="http://tempuri.org/IService1/MatrixMultiplicationResponse")]
        System.Threading.Tasks.Task<string> MatrixMultiplicationAsync(string firstMatrixName, string secondMatrixName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/UploadFile", ReplyAction="http://tempuri.org/IService1/UploadFileResponse")]
        string UploadFile(System.IO.Stream input);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/UploadFile", ReplyAction="http://tempuri.org/IService1/UploadFileResponse")]
        System.Threading.Tasks.Task<string> UploadFileAsync(System.IO.Stream input);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DownloadFile", ReplyAction="http://tempuri.org/IService1/DownloadFileResponse")]
        System.IO.Stream DownloadFile(string file);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DownloadFile", ReplyAction="http://tempuri.org/IService1/DownloadFileResponse")]
        System.Threading.Tasks.Task<System.IO.Stream> DownloadFileAsync(string file);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetFilesList", ReplyAction="http://tempuri.org/IService1/GetFilesListResponse")]
        string[] GetFilesList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetFilesList", ReplyAction="http://tempuri.org/IService1/GetFilesListResponse")]
        System.Threading.Tasks.Task<string[]> GetFilesListAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : WebClient.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<WebClient.ServiceReference1.IService1>, WebClient.ServiceReference1.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string MatrixMultiplication(string firstMatrixName, string secondMatrixName) {
            return base.Channel.MatrixMultiplication(firstMatrixName, secondMatrixName);
        }
        
        public System.Threading.Tasks.Task<string> MatrixMultiplicationAsync(string firstMatrixName, string secondMatrixName) {
            return base.Channel.MatrixMultiplicationAsync(firstMatrixName, secondMatrixName);
        }
        
        public string UploadFile(System.IO.Stream input) {
            return base.Channel.UploadFile(input);
        }
        
        public System.Threading.Tasks.Task<string> UploadFileAsync(System.IO.Stream input) {
            return base.Channel.UploadFileAsync(input);
        }
        
        public System.IO.Stream DownloadFile(string file) {
            return base.Channel.DownloadFile(file);
        }
        
        public System.Threading.Tasks.Task<System.IO.Stream> DownloadFileAsync(string file) {
            return base.Channel.DownloadFileAsync(file);
        }
        
        public string[] GetFilesList() {
            return base.Channel.GetFilesList();
        }
        
        public System.Threading.Tasks.Task<string[]> GetFilesListAsync() {
            return base.Channel.GetFilesListAsync();
        }
    }
}
