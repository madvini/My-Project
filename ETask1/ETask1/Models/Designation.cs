using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETask1.Models
{
    public class Designation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string DesignationID { get; set; }

        [Required(ErrorMessage="Enter Designation")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Enter valid Designation")]
        [Display(Name="Designation")]
        public string Title { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}