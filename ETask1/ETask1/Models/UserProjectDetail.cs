using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETask1.Models
{
    public class UserProjectDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ProjectDetailID { get; set; }
 
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        
        public string ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public virtual Project Project { get; set; }
    }
}