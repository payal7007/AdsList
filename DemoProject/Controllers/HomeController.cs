using DemoProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoProject.Controllers
{
    public class HomeController : Controller
    {
        dataAccess data = new dataAccess();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ProductList(string SearchItem, int? i)
        {
            
            IEnumerable<MyAdvertiseModel> products = data.GetAllProductList();
            return View(products);
        }
        //public ActionResult Details(int? advertiseId)
        //{
        //    if (advertiseId == null)
        //    {
        //        return View("Error");
        //    }
        //    try
        //    {
        //        MyAdvertiseModel advertise = data.GetAllDetails(advertiseId);

        //        if (advertise != null)
        //        {
        //            return View(advertise);
        //        }
        //        else
        //        {
        //            return View("Error");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return View("NotFound");
        //    }
        //}
        //public ActionResult ProductListEdit(int? advertiseId)
        //{
        //    if (advertiseId == null)
        //    {
        //        return View("Error");
        //    }

        //    // Retrieve the data for the specified advertiseId and pass it to the view
        //    MyAdvertiseModel advertise = data.GetDetailsById(advertiseId);

        //    if (advertise != null)
        //    {
        //        return View(advertise);
        //    }
        //    else
        //    {
        //        return View("Error");
        //    }
        //}
        //public ActionResult DeleteProject(long id)
        //{
        //    if (Session["UserID"] == null)
        //    {
        //        return RedirectToAction("Login", "Home");
        //    }
        //    ProjectRepository repository = new ProjectRepository();
        //    repository.DeleteProject(id);
        //    return RedirectToAction("Project", "Home");
        //}
        public ActionResult Edit(int? advertiseId)
        {
            if (advertiseId == null)
            {
                return View("Error");
            }

            // Retrieve the record from the database based on the advertiseId
            MyAdvertiseModel advertise = data.GetAdvertiseById(advertiseId);

            if (advertise != null)
            {
                return View(advertise);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Edit(MyAdvertiseModel model)
        {
            // Update the record in the database with the edited data
            bool isUpdated = data.UpdateAdvertise(model);

            if (isUpdated)
            {
                TempData["AlertMessage"] = "Record updated successfully.";
                return RedirectToAction("Details");
            }
            else
            {
                return View("Error");
            }
        }
        public ActionResult Delete(int? advertiseId)
        {
            if (advertiseId == null)
            {
                return View("Error");
            }

            // Delete the record from the database based on the advertiseId
            bool isDeleted = data.DeleteAdvertise(advertiseId);

            if (isDeleted)
            {
                TempData["AlertMessage"] = "Record deleted successfully.";
                return RedirectToAction("Details");
            }
            else
            {
                return View("Error");
            }
        }
    }
}