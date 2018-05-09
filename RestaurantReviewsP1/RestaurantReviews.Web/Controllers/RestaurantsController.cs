using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RestaurantReviews.Library;
using RestaurantReviews.Library.Models;
using NLog;

namespace RestaurantReviews.Web.Controllers
{
    public class RestaurantsController : Controller
    {
        private Service service;
        public Logger logger;

        public RestaurantsController()
        {
            service = new Service();
            logger = NLog.LogManager.GetCurrentClassLogger();
        }

        public ActionResult Index(int? id, string q)
        {
            IEnumerable<Restaurant > rests;

            if (!string.IsNullOrEmpty(q))
            {
                switch (id)
                {
                    case 1:
                        rests = service.SortByNameDescending(q);
                        break;
                    case 2:
                        rests = service.SortByRating(q);
                        break;
                    case 3:
                        rests = service.SortByNumberOfReviews(q);
                        break;
                    default:
                        rests = service.SortByNameAscending(q);
                        break;
                }
            }
            else
            {
                switch (id)
                {
                    case 1:
                        rests = service.SortByNameDescending();
                        break;
                    case 2:
                        rests = service.SortByRating();
                        break;
                    case 3:
                        rests = service.SortByNumberOfReviews();
                        break;
                    default:
                        rests = service.SortByNameAscending();
                        break;
                }
            }

            ViewBag.Query = q;
            return View(rests);
        }

        // GET: Restaurants/Details/5
        public ActionResult Details(int id)
        {
            return View(service.GetRestaurantById(id));
        }

        // GET: Restaurants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        [HttpPost]
        public ActionResult Create(Restaurant restaurant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.InsertRestaurant(restaurant);
                    logger.Info("Successfully added new restaurant: " + restaurant.Name);
                    return RedirectToAction("Index");
                }
                else
                    return View(restaurant);
            }
            //log success 
            catch
            {
                //log problem
                logger.Error("Failed to add restaurant: " + restaurant.Name);
                return View();
            }
        }

        // GET: Restaurants/Edit/5
        public ActionResult Edit(int id)
        {
            return View(service.GetRestaurantById(id));
        }

        // POST: Restaurants/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Restaurant restaurant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.UpdateRestaurant(restaurant);
                    logger.Info("Successfully updated restaurant: " + restaurant.Name);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                logger.Error("Failed to edit restaurant: " + restaurant.Name);
                return View();
            }
        }

        // GET: Restaurants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Restaurant rest = service.GetRestaurantById(id);

            if (rest == null)
            {
                return HttpNotFound();
            }
            return View(rest);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            string name = service.GetRestaurantById(id).Name;
            try
            {
                service.DeleteRestaurant(id);
                logger.Info("Successfully deleted restaurant: " + name);
                return RedirectToAction("Index");
            }
            catch
            {
                logger.Info("Failed to delete restaurant: " + name);
                return View();
            }
        }
    }
}
