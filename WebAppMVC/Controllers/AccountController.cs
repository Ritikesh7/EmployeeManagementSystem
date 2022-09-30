using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebAppMVC.HelperClass;
using WebAppMVC.Models;
namespace WebAppMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        EmployeeAPI _api = new EmployeeAPI();
        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;

        }
       
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            HttpClient client = _api.Initial();

            var postTask =  client.PostAsJsonAsync<LoginViewModel>("api/Account/Login", model);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Registar(RegistrationViewModel model)
        {

            if (ModelState.IsValid)
            {
                Userdetails employee = new Userdetails();
                HttpClient client = _api.Initial();
                HttpResponseMessage res = await client.GetAsync("api/Account/Login");

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Account");
                }
                return RedirectToAction("Index", "Home");

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        // registration Page load
        public IActionResult Registration()
        {
            ViewData["Message"] = "Registration Page";

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }
        //public void ValidationMessage(string key, string alert, string value)
        //{
        //    try
        //    {
        //        TempData.Remove(key);
        //        TempData.Add(key, value);
        //        TempData.Add("alertType", alert);
        //    }
        //    catch
        //    {
        //        Debug.WriteLine("TempDataMessage Error");
        //    }

        //}
    }
}
