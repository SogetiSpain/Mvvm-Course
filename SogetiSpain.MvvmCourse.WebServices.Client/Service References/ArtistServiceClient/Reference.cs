﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SogetiSpain.MvvmCourse.WebServices.Client.ArtistServiceClient {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ArtistServiceClient.IArtistService")]
    public interface IArtistService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IArtistService/GetAll", ReplyAction="http://tempuri.org/IArtistService/GetAllResponse")]
        SogetiSpain.MvvmCourse.WebServices.ArtistDto[] GetAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IArtistService/GetAll", ReplyAction="http://tempuri.org/IArtistService/GetAllResponse")]
        System.Threading.Tasks.Task<SogetiSpain.MvvmCourse.WebServices.ArtistDto[]> GetAllAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IArtistService/GetById", ReplyAction="http://tempuri.org/IArtistService/GetByIdResponse")]
        SogetiSpain.MvvmCourse.WebServices.ArtistDto GetById(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IArtistService/GetById", ReplyAction="http://tempuri.org/IArtistService/GetByIdResponse")]
        System.Threading.Tasks.Task<SogetiSpain.MvvmCourse.WebServices.ArtistDto> GetByIdAsync(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IArtistServiceChannel : SogetiSpain.MvvmCourse.WebServices.Client.ArtistServiceClient.IArtistService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ArtistServiceClient : System.ServiceModel.ClientBase<SogetiSpain.MvvmCourse.WebServices.Client.ArtistServiceClient.IArtistService>, SogetiSpain.MvvmCourse.WebServices.Client.ArtistServiceClient.IArtistService {
        
        public ArtistServiceClient() {
        }
        
        public ArtistServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ArtistServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ArtistServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ArtistServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public SogetiSpain.MvvmCourse.WebServices.ArtistDto[] GetAll() {
            return base.Channel.GetAll();
        }
        
        public System.Threading.Tasks.Task<SogetiSpain.MvvmCourse.WebServices.ArtistDto[]> GetAllAsync() {
            return base.Channel.GetAllAsync();
        }
        
        public SogetiSpain.MvvmCourse.WebServices.ArtistDto GetById(int id) {
            return base.Channel.GetById(id);
        }
        
        public System.Threading.Tasks.Task<SogetiSpain.MvvmCourse.WebServices.ArtistDto> GetByIdAsync(int id) {
            return base.Channel.GetByIdAsync(id);
        }
    }
}