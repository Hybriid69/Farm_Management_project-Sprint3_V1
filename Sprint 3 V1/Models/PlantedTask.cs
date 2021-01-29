using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprint_3_V1.Models
{
    [Table("Task")]
    public class PlantedTask
    {
        

        [Key]
        public int PTaskID { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public DateTime Assigned { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartOn { get; set; }

        public DateTime ExpectedCompletion { get; set; }

        [DataType(DataType.Date)]
        public DateTime CompletedOn { get; set; }

        public bool flag { get; set; }
     
        public int PlantedID { get; set; }

        [ForeignKey("PlantedID")]
        public virtual Planted Planted { get; set; }


        public virtual ICollection<GroupTask> GroupTask { get; set; }

    }
}