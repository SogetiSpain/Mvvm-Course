namespace SogetiSpain.DI_IoC.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using SharedObjects;

    public class EmployeeService : IEmployeeService
    {
        public IEnumerable<Employee> GetAll()
        {
            return new List<Employee>()
            {
                new Employee() { FirstName="John", LastName="Koenig", BirthDate = new DateTime(1975, 10, 17), Email="john.koenig@example.com" },
                new Employee() { FirstName="Dylan", LastName="Hunt", BirthDate = new DateTime(2000, 10, 2), Email="dylan.hunt@example.com" },
                new Employee() { FirstName="John", LastName="Crichton", BirthDate= new DateTime(1999, 3, 19), Email="john.crichton@example.com" },
                new Employee() { FirstName="Dave", LastName="Lister", BirthDate = new DateTime(1988, 2, 15), Email="dave.lister@example.com" },
                new Employee() { FirstName="John", LastName="Sheridan", BirthDate = new DateTime(1994, 1, 26), Email="john.sheridan@example.com" },
                new Employee() { FirstName="Dante", LastName="Montana", BirthDate = new DateTime(2000, 11, 1), Email="dante.montana@example.com" },
                new Employee() { FirstName="Isaac", LastName="Gampu", BirthDate = new DateTime(1977, 9, 10), Email="isaac.gampu@example.com" }
            };
        }
    }
}
