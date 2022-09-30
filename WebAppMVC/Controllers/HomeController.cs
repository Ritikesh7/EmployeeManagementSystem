using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Server.Models;
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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        EmployeeAPI _api = new EmployeeAPI();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }

        public async Task<IActionResult> Index()
        {
            List<EmployeeData> employee = new List<EmployeeData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Employee/GetEmployee");

            if (res.IsSuccessStatusCode) {
                var result = res.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<List<EmployeeData>>(result);
            }
            return View(employee);
        }

        public async Task<IActionResult> Details(int ID)
        {
            EmployeeData employee = new EmployeeData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/Employee/GetEmployeeByID/{ID}");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<EmployeeData>(result);
            }
            return View(employee);
        }

   
        public async Task<IActionResult> Delete(int ID)
        {
            EmployeeData employee = new EmployeeData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/Employee/DeleteEmployee/{ID}");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeData employee)
        {
            
            HttpClient client = _api.Initial();
            
            var postTask = client.PostAsJsonAsync<EmployeeData>("api/Employee/AddEmployee", employee);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); 
                
            }
            return View();
        }

        public async Task<ActionResult> Edit(int ID)
        {
            EmployeeData employee = new EmployeeData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/Employee/GetEmployeeByID/{ID}");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<EmployeeData>(result);
            }
            return View("Create",employee);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeData employee)
        {

            HttpClient client = _api.Initial();

            var postTask = client.PutAsJsonAsync<EmployeeData>($"api/Employee/UpdateEmployee/{employee.EmployeeID}", employee);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
