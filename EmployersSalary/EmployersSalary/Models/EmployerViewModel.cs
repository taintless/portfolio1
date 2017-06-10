using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployersSalary.Models
{
    public class EmployerViewModel
    {
        public int Id { get; set; }
        [StringLength(225)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(225)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Net Salary")]
        public float? NetSalary { get; set; }
    }
}