namespace SogetiSpain.DI_IoC.ServiceClientLayer
{
    using System.Collections.Generic;
    using SharedObjects;

    public sealed class ServiceCatalog : IServiceCatalog
    {
        private readonly EmployeeServiceClient.EmployeeServiceClient employeeServiceProxy;

        public ServiceCatalog()
        {
            this.employeeServiceProxy = new EmployeeServiceClient.EmployeeServiceClient();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return this.employeeServiceProxy.GetAll();
        }
    }
}