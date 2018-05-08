using RestaurantReviews.Data.Models;
using RestaurantReviews.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestaurantReviews.Data;
using RestaurantReviews.Library;

namespace RestaurantReviews.Web.Controllers
{
    public class RestaurantsController : Controller
    {
        private ICrud<Restaurant> crud;

        private Service service;

        private IDbContext db;

        public RestaurantsController()
        {
            db = new RestaurantReviewsContext();
            crud = new Crud<Restaurant>(db);
            service = new Service();
        }

        public RestaurantsController(IDbContext otherContext)
        {
            db = otherContext;
            crud = new Crud<Restaurant>(db);
        }

        // GET: Restaurants
        //public ActionResult Index()
        //{
        //    var rests = crud.Table.ToList().OrderBy(x => x.Name);
        //    return View(rests);
        //}

        public ActionResult Index(int? id, string q)
        {
            IEnumerable< Restaurant > rests;

            if (!string.IsNullOrEmpty(q))
            {
                switch (id)
                {
                    case 1:
                        //rests = crud.Table.Where(s => s.Name.Contains(q) || s.Street.Contains(q) || s.City.Contains(q) || s.State.Contains(q) || s.Zipcode.Contains(q) || s.Phone.Contains(q))
                        //    .OrderByDescending(x => x.Name).ToList();
                        //rests = service.RestaurantTable().Where(s => (s.Name != null && s.Name.Contains(q)) || (s.Street != null && s.Street.Contains(q)) || (s.City != null && s.City.Contains(q)) || (s.State != null && s.State.Contains(q)) || (s.Zipcode != null && s.Zipcode.Contains(q)) || (s.Phone != null && s.Phone.Contains(q)))
                        rests = crud.Table.Where(s => s.Name.Contains(q) || s.Street.Contains(q) || s.City.Contains(q) || s.State.Contains(q) || s.Zipcode.Contains(q) || s.Phone.Contains(q))
                        .OrderByDescending(x => x.Name).ToList();
                        break;
                    case 2:
                        //rests = service.RestaurantTable().Where(s => (s.Name != null && s.Name.Contains(q)) || (s.Street != null && s.Street.Contains(q)) || (s.City != null && s.City.Contains(q)) || (s.State != null && s.State.Contains(q)) || (s.Zipcode != null && s.Zipcode.Contains(q)) || (s.Phone != null && s.Phone.Contains(q)))
                        rests = crud.Table.Where(s => s.Name.Contains(q) || s.Street.Contains(q) || s.City.Contains(q) || s.State.Contains(q) || s.Zipcode.Contains(q) || s.Phone.Contains(q))
                        .OrderByDescending(x => x.Reviews.Average(a => a.Rating))
                            .ThenByDescending(x => x.Reviews.Count).ToList();
                        break;
                    case 3:
                        //rests = service.RestaurantTable().Where(s => (s.Name != null && s.Name.Contains(q)) || (s.Street != null && s.Street.Contains(q)) || (s.City != null && s.City.Contains(q)) || (s.State != null && s.State.Contains(q)) || (s.Zipcode != null && s.Zipcode.Contains(q)) || (s.Phone != null && s.Phone.Contains(q)))
                        rests = crud.Table.Where(s => s.Name.Contains(q) || s.Street.Contains(q) || s.City.Contains(q) || s.State.Contains(q) || s.Zipcode.Contains(q) || s.Phone.Contains(q))
                        .OrderByDescending(x => x.Reviews.Count)
                            .ThenByDescending(x => x.Reviews.Average(a => a.Rating)).ToList();
                        break;
                    default:
                        //rests = service.RestaurantTable().Where(s => (s.Name != null && s.Name.Contains(q)) || (s.Street != null && s.Street.Contains(q)) || (s.City != null && s.City.Contains(q)) || (s.State != null && s.State.Contains(q)) || (s.Zipcode != null && s.Zipcode.Contains(q)) || (s.Phone != null && s.Phone.Contains(q)))
                        rests = crud.Table.Where(s => s.Name.Contains(q) || s.Street.Contains(q) || s.City.Contains(q) || s.State.Contains(q) || s.Zipcode.Contains(q) || s.Phone.Contains(q))
                        .OrderBy(x => x.Name).ToList();
                        break;
                }
            }
            else
            {
                switch (id)
                {
                    case 1:
                        //rests = service.RestaurantTable().OrderByDescending(x => x.Name).ToList();
                        rests = crud.Table.OrderByDescending(x => x.Name).ToList();
                        break;
                    case 2:
                        rests = crud.Table.OrderByDescending(x => x.Reviews.Average(a => a.Rating))
                            .ThenByDescending(x => x.Reviews.Count).ToList();
                        break;
                    case 3:
                        rests = crud.Table.OrderByDescending(x => x.Reviews.Count)
                            .ThenByDescending(x => x.Reviews.Average(a => a.Rating)).ToList();
                        break;
                    default:
                        rests = crud.Table.OrderBy(x => x.Name).ToList();
                        break;
                }
            }

            foreach (Restaurant item in rests)
            {
                double rating = 0;
                if (item.Reviews.Count != 0)
                {
                    rating = item.Reviews.Average(x => x.Rating);
                }
                item.AverageRating = Math.Truncate(rating * 100) / 100;
            }

            ViewBag.Query = q;
            return View(rests);
        }

        //public ActionResult Search(string query)
        //{
        //    return 
        //}

        // GET: Restaurants/Details/5
        public ActionResult Details(int id)
        {
            return View(crud.GetById(id));
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
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    crud.Insert(restaurant);
                    return RedirectToAction("Index");
                }
                else
                    return View(restaurant);
            }
            //log success 
            catch
            {
                //log problem
                return View();
            }
        }

        // GET: Restaurants/Edit/5
        public ActionResult Edit(int id)
        {
            return View(crud.GetById(id));
        }

        // POST: Restaurants/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Restaurant restaurant)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    //    crud.Update(restaurant);

                    var rest = crud.GetById(id);

                    //db.Entry(rest).state = EntityState.Modified;
                    rest.Name = restaurant.Name;
                    rest.Street = restaurant.Street;
                    rest.City = restaurant.City;
                    rest.State = restaurant.State;
                    rest.Country = restaurant.Country;
                    rest.Zipcode = restaurant.Zipcode;
                    rest.Phone = restaurant.Phone;
                    rest.Website = restaurant.Website;

                    crud.Update(restaurant);
                    return RedirectToAction("Index");
                }
                else
                {
                    //return View(ModelState);
                    return View();
                }
            }
            catch
            {
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

            Restaurant rest = crud.GetById(id);
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
            try
            {
                // TODO: Add delete logic here
                Restaurant rest = crud.GetById(id);
                if (rest != null)
                {
                    crud.Delete(rest);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
