﻿using System.Linq;
using System.Web.Mvc;
using RestaurantReviews.Library;

namespace RestaurantReviews.Web.Controllers
{
    public class HomeController : Controller
    {
        private Service service;

        public HomeController()
        {
            service = new Service();
        }

        public ActionResult Index()
        {
            return View(service.Top3());
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
    }
}