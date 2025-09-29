using Microsoft.AspNetCore.Mvc;
using SkolSystem.Models;

namespace SkolSystem.Controllers
{
    public class ElevController : Controller
    {
        //konstruktor
        public ElevController() { }

        //publika metoder
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult InsertElev()
        {
            ElevDetails elevDetails = new ElevDetails();
            ElevMethods elevMethods = new ElevMethods();
            int i = 0;
            string error = "";

            elevDetails.El_Fornamn = "Melissa";
            elevDetails.El_Efternamn = "Gök";

            i = elevMethods.InsertElev(elevDetails, out error);
            ViewBag.error = error;
            return View();
        }
        public ActionResult SelectElev()
        {
            List<ElevDetails> elevDetailsList = new List<ElevDetails>();
            ElevMethods elevMethods = new ElevMethods();
            string error = "";

            elevDetailsList = elevMethods.GetElevDetailsList(out error);
            //var filtered = elevDetailsList.Where(e => e.El_Fornamn.Contains("a")).ToList();
            //ViewBag.antal = HttpContext.Session.GetString("antal");
            ViewBag.error = error;
            return View(elevDetailsList);

        }
        [HttpGet]
        public ActionResult UpdateElev(int id)
        {
            ElevDetails elevDetails = new ElevDetails();
            ElevMethods elevMethods = new ElevMethods();
            string error = "";

            elevDetails = elevMethods.GetElevDetails(id, out error);
            //var filtered = elevDetailsList.Where(e => e.El_Fornamn.Contains("a")).ToList();
            //ViewBag.antal = HttpContext.Session.GetString("antal");
            ViewBag.error = error;
            return View(elevDetails);

        }
        [HttpPost]
        public ActionResult UpdateElev(ElevDetails elevDetails, int id)
        {
            ElevMethods elevMethods = new ElevMethods();
            string error = "";
            elevDetails = elevMethods.UpdateElevDetails(elevDetails, id, out error);
        
            ViewBag.error = error;

            return View(elevDetails);

        }

        public ActionResult SelectElevAndKurs()
        {
            string errormsg;
            string errormsg2;

            ElevMethods elevMethods = new ElevMethods();
            RegistreringMethods registreringMethods = new RegistreringMethods();

            RegistreringViewModel registreringViewModel = new RegistreringViewModel
            {
                ElevDetails = elevMethods.GetElevDetailsList(out errormsg),
                RegistreringDetails = registreringMethods.GetRegistreringDetailsList(out errormsg2)
            };

            ViewBag.error = "Error1: " + errormsg + " Error2: " + errormsg2;
            return View(registreringViewModel);


        }
    }
}