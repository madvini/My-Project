using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using ETask1.Models;

namespace ETask1.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserID { get; set; }

        [Required(ErrorMessage = "Enter UserName")]
        [MaxLength(20)]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Enter valid UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Password Should be 8-16 characters Long")]
        [RegularExpression(@"^[0-9a-zA-Z.@*$%*]{8,16}$", ErrorMessage = "Enter Valid Password.")]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Password Should be 8-16 characters Long")]
        [RegularExpression(@"^[0-9a-zA-Z.@*$%*]{8,16}$", ErrorMessage = "Enter Valid Password.")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password should match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage="Enter Email")]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-z]{2,4}$", ErrorMessage = "Enter valid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Phone No")]
        [Display(Name = "Phone Number")]      
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Enter valid Phone Number")]
        public Int64 PhoneNo { get; set; }

        [Required(ErrorMessage="Select Department")]
        public string DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        [Required(ErrorMessage="Select Designation")]
        public string DesignationID { get; set; }
        public virtual Designation Designation { get; set; }

        [Required(ErrorMessage="Select Usertype")]
        [Display(Name = "User Type")]
        public string UserType { get; set; }

        [Required(ErrorMessage = "Enter DOJ")]
        [Display(Name = "Joining Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [EtaskValidation.InputLessThanFutureDate(ErrorMessage = "Enter valid Date Of Join")]
        public DateTime DOJ { get; set; }

        public string Status { get; set; }

        
        public virtual ICollection<Project> Projects { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }

        public virtual ICollection<Watcher> Watchers { get; set; }

        public virtual ICollection<UserProjectDetail> UserProjectDetails { get; set; }
    }
   
}