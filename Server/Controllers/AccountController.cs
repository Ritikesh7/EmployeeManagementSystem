using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly APIDbContext _context;

        public AccountController(APIDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        [Route("Login")]
        public async Task<ActionResult> Login([FromQuery] string Email, [FromQuery] string Password)
        {
           
                var userdetails = await _context.Userdetails
                .SingleOrDefaultAsync(m => m.Email == Email && m.Password == Password);

                if (userdetails == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Invalid login attempt.");
                }

            return Ok("login Successfully");


        }
        [HttpPost]
        [Route("Registar")]
        public async Task<ActionResult> Registar(RegistrationViewModel model)
        {

            if (ModelState.IsValid)
            {
                Userdetails user = new Userdetails
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                    Mobile = model.Mobile

                };
                _context.Add(user);
                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Invalid login attempt.");
            }
          
        }
        
    }
}
