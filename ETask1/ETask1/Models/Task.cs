using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETask1.Models
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string TaskID { get; set; }


        [Required]
        public string ProjectID { get; set; }
        public virtual Project Project { get; set; }

        [Required(ErrorMessage = "Enter the Subject")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Enter StartDate")]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[EtaskValidation.InputGreaterThanFutureDate(ErrorMessage = "Enter valid Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Select Priority")]
        public string Priority { get; set; }



        [Required(ErrorMessage = "Please Select User")]
        public string AssignedTo { get; set; }
       [ForeignKey("AssignedTo")]
        public virtual User Assignee { get; set; }

        [Required(ErrorMessage = "Enter DueDate")]
        [Display(Name = "Due Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[EtaskValidation.InputGreaterThanFutureDate(ErrorMessage = "Enter valid DueDate")]
        [EtaskValidation.DateGreaterThan("StartDate", "Due Date cannot be less than Start Date ")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "Enter Task Description")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Enter valid Description")]
        public string Description { get; set; }

      
        public string File { get; set; }

        
        public virtual ICollection<Watcher> Watchers { get; set; }


        [Required]
        public float PercentageCompleted { get; set; }

        [Required(ErrorMessage = "Please Select the Status")]
        public string Status { get; set; }
    }
}