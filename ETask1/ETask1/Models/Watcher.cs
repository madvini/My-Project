using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ETask1.Models
{
    public class Watcher
    {
        [Key]
        
        public string SerialNo { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User{ get; set; }

       
        public string TaskID { get; set; }
         [ForeignKey("TaskID")]
        public virtual Task Task { get; set; }

        

    }
}