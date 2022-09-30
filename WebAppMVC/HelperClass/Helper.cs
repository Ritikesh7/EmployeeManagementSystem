using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebAppMVC.HelperClass
{
    public class EmployeeAPI
    {
        public HttpClient Initial() {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61615/");
            return client;
        }
    }
}
