using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantReviews.Data;
using RestaurantReviews.Data.Models;
using RestaurantReviews.DataAccess.Repositories;

namespace RestaurantReviews.Web.Controllers
{
    public class HomeController : Controller
    {
        private ICrud<Restaurant> crud;

        private IDbContext db;

        public HomeController()
        {
            db = new RestaurantReviewsContext();
            crud = new Crud<Restaurant>(db);
        }

        public ActionResult Index()
        {
            return View(crud.Table.OrderByDescending(x => x.Reviews.Average(a => a.Rating)).Take(3).ToList());
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