using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETask1.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string DepartmentID { get; set; }

        [Required(ErrorMessage="Enter DepartmentName")]
        [Display(Name = "Department Name")]
        [StringLength(20, ErrorMessage = "Department Name cannot be greater than 20 characters.")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Enter valid DepartmentName")]
        public String DepartmentName { get; set; }

        //[Required(ErrorMessage = "Enter DepartmentHead")]
        //[Display(Name = "Department Head")]
        //[MaxLength(20)]
        //[RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Enter valid Name")]
        //public String DepartmentHead { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}