namespace SogetiSpain.DI_IoC.ServiceLayer
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using SharedObjects;

    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        IEnumerable<Employee> GetAll();
    }
}
