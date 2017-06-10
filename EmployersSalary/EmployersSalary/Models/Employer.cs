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
        [Key]
        [Column(Order = 1)]
        [Required]
        [StringLength(225)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(225)]
        [Key]
        [Column(Order = 2)]
        public string LastName { get; set; }
        public float? NetSalary { get; private set; }
        [Required]
        public bool IsDisabled { get; private set; } = false;
        public DateTime? DisabledOn { get; private set; }

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