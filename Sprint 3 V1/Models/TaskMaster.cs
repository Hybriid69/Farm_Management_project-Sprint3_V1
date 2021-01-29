using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sprint_3_V1.Models
{
    public class TaskMaster
    {
        [Key]
        public int MTaskID { get; set; }

        public string TaskName { get; set; }

        public string description { get; set; }

        public int Duration { get; set; }

        public virtual ICollection<CropTask> CropTask { get; set; }
    }
}