using Sprint_3_V1.Models;
using Sprint_3_V1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sprint_3_V1.Controllers
{
[AllowAnonymous]
    public class ShowdataController : Controller
    {
        // GET: Showdata
        public ActionResult Index()
        {
            Sprint_3_V1Context db = new Sprint_3_V1Context();
            var mymodel = new HomeScreenVM();
            mymodel.plantedsss = db.Planteds.ToList().FindAll(x => x.Status == "Growing");
            mymodel.etasksss = db.PlantedTasks.ToList().FindAll(x => x.Status == "On-Going");

            return View(mymodel);
        }
    }
}