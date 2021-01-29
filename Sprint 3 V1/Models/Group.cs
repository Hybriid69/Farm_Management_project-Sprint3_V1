using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sprint_3_V1.Models
{
    public class Group
    {
        public int GroupID { get; set; }

        public string GName { get; set; }

        public string GDescription { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }

        public virtual ICollection<GroupTask> GroupTask { get; set; }
    }
}