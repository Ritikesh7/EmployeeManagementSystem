using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.EmployeeData
{
    public interface IEmployeeData
    {
        List<Employee> GetEmployees();
        Employee GetEmployeeByID(int ID);
        Employee InsertEmployee(Employee objEmployee);
        Employee UpdateEmployee(Employee objEmployee);
        bool DeleteEmployee(int ID);
    }
}
