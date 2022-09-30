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
    public class DepartmentController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        EmployeeAPI _api = new EmployeeAPI();
        public DepartmentController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<DepartmentData> department = new List<DepartmentData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Department/GetDepartment");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                department = JsonConvert.DeserializeObject<List<DepartmentData>>(result);
            }
            return View(department);
        }

        public async Task<IActionResult> Details(int ID)
        {
            DepartmentData department = new DepartmentData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/Department/GetDepartmentByID/{ID}");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                department = JsonConvert.DeserializeObject<DepartmentData>(result);
            }
            return View(department);
        }


        public async Task<IActionResult> DeleteDept(int ID)
        {
            DepartmentData department = new DepartmentData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/Department/DeleteDepartment/{ID}");

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
        public IActionResult Create(DepartmentData department)
        {

            HttpClient client = _api.Initial();

            var postTask = client.PostAsJsonAsync<DepartmentData>("api/Department/AddDepartment", department);
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
            DepartmentData department = new DepartmentData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/Department/GetDepartmentByID/{ID}");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                department = JsonConvert.DeserializeObject<DepartmentData>(result);
            }
            return View("Create", department);
        }

        [HttpPost]
        public IActionResult Edit(DepartmentData department)
        {

            HttpClient client = _api.Initial();

            var postTask = client.PutAsJsonAsync<DepartmentData>($"api/Department/UpdateDepartment/{department.DepartmentId}", department);
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
