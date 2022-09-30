using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.EmployeeData;
using Server.Models;
using Server.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employee;
     
        public EmployeeController(IEmployeeRepository employee, IDepartmentRepository department)
        {
            _employee = employee ??
                throw new ArgumentNullException(nameof(employee));
            
        }
        [HttpGet]
        [Route("GetEmployee")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _employee.GetEmployees());
        }
        [HttpGet]
        [Route("GetEmployeeByID/{Id}")]
        public async Task<IActionResult> GetEmpByID(int Id)
        {
            return Ok(await _employee.GetEmployeeByID(Id));
        }
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> Post(Employee emp)
        {
            var result = await _employee.InsertEmployee(emp);
            if (result.EmployeeID == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added Successfully");
        }

        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> Put(Employee emp)
        {
            await _employee.UpdateEmployee(emp);
            return Ok("Updated Successfully");
        }


        [HttpDelete]
        [Route("DeleteEmployee/{Id}")]
        public bool Delete(int id)
        {
            bool emp = _employee.DeleteEmployee(id);
            return emp;
        }
    
    }
}
