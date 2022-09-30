using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMVC.Models
{
    public class DepartmentData
    {
        [Key]
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }
    }
}
