using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.EmployeeData
{
    public class MockEmployeeData : IEmployeeData
    {
        public bool DeleteEmployee(int ID)
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployeeByID(int ID)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetEmployees()
        {
            throw new NotImplementedException();
        }

        public Employee InsertEmployee(Employee objEmployee)
        {
            throw new NotImplementedException();
        }

        public Employee UpdateEmployee(Employee objEmployee)
        {
            throw new NotImplementedException();
        }
    }
}
