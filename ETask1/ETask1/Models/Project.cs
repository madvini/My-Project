using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETask1.Models
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ProjectID { get; set; }

        [Required(ErrorMessage="Enter Project Name")]
        [Display(Name = "Project Name")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Enter valid PriojectName")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage="Enter Project Description")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Enter valid Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Enter Client Name")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Enter valid Client Name")]
        public string ClientName { get; set; }

        [Required(ErrorMessage="Please Select User")]        
        public string UserID { get; set; }        
        public virtual User ProjectManager { get; set; }

        [Required(ErrorMessage = "Enter StartDate")]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[EtaskValidation.InputGreaterThanFutureDate(ErrorMessage = "Enter valid Start Date")]
        public DateTime EstimatedStartDate { get; set; }

        [Required(ErrorMessage = "Enter DueDate")]
        [Display(Name = "Due Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [EtaskValidation.InputGreaterThanFutureDate(ErrorMessage = "Enter valid EstimatedDueDate")]
        [EtaskValidation.DateGreaterThan("EstimatedStartDate","Estimated Due Date cannot be less than Start Date ")]
        public DateTime EstimatedDueDate { get; set; }

        [Required(ErrorMessage = "Enter Actual Start Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [EtaskValidation.InputGreaterThanFutureDate(ErrorMessage = "Enter valid ActualDate")]
        public DateTime ActualStartDate { get; set; }

        [Required(ErrorMessage="Enter Actual Due Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [EtaskValidation.InputGreaterThanFutureDate(ErrorMessage = "Enter valid ActualEndDate")]
        [EtaskValidation.DateGreaterThan("EstimatedStartDate", "Actual Due Date cannot be less than Start Date ")]
        public DateTime ActualDueDate { get; set; }


        public virtual ICollection<Task> TasksProject { get; set; }

        public virtual ICollection<UserProjectDetail> UserProjectDetails { get; set; }

    }
}