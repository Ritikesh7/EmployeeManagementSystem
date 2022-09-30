using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _department;
        public DepartmentController(IDepartmentRepository department)
        {
            _department = department ??
                throw new ArgumentNullException(nameof(department));
        }
        [HttpGet]
        [Route("GetDepartment")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _department.GetDepartment());
        }
        [HttpGet]
        [Route("GetDepartmentByID/{Id}")]
        public async Task<IActionResult> GetDeptById(int Id)
        {
            return Ok(await _department.GetDepartmentByID(Id));
        }
        [HttpPost]
        [Route("AddDepartment")]
        public async Task<IActionResult> Post(Department dep)
        {
            var result = await _department.InsertDepartment(dep);
            if (result.DepartmentId == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added Successfully");
        }
        [HttpPut]
        [Route("UpdateDepartment")]
        public async Task<IActionResult> Put(Department dep)
        {
            await _department.UpdateDepartment(dep);
            return Ok("Updated Successfully");
        }
        [HttpDelete]
        [Route("DeleteDepartment/{Id}")]
        public JsonResult Delete(int id)
        {
            _department.DeleteDepartment(id);
            return new JsonResult("Deleted Successfully");
        }
    }
}
