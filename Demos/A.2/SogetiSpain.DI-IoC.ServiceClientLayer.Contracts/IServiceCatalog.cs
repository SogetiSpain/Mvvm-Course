using System.Collections.Generic;
using SogetiSpain.DI_IoC.SharedObjects;

namespace SogetiSpain.DI_IoC.ServiceClientLayer
{
    public interface IServiceCatalog
    {
        IEnumerable<Employee> GetAllEmployees();
    }
}