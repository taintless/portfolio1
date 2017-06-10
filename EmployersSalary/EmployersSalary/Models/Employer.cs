using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployersSalary.Models
{
    public class Employer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(225)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(225)]
        public string LastName { get; set; }
        public float? NetSalary { get; private set; }
        [Required]
        public bool IsDisabled { get; private set; } = false;
        public DateTime? DisabledOn { get; private set; }
        public bool RegularEmployer { get; set; }

        public void UpdateSalary(float netSalary)
        {
            this.NetSalary = netSalary;
        }

        public void Disable()
        {
            IsDisabled = true;
            DisabledOn = DateTime.Now;
        }
    }
}