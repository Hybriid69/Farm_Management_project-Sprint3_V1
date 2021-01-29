using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sprint_3_V1.Models;

namespace Sprint_3_V1.ViewModels
{
    public class HomeScreenVM
    {
        public IEnumerable<Planted> plantedsss { get; set; }

        public IEnumerable<PlantedTask> etasksss { get; set; }

    }
}