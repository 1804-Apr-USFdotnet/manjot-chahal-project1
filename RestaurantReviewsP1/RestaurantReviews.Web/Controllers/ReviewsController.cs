using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestaurantReviews.Data;
using RestaurantReviews.Data.Models;
using RestaurantReviews.DataAccess.Repositories;

namespace RestaurantReviews.Web.Controllers
{
    public class ReviewsController : Controller
    {
        private ICrud<Review> crud;
        private IDbContext db;

        public ReviewsController()
        {
                db = new RestaurantReviewsContext();
                crud = new Crud<Review>(db);
        }

        public ReviewsController(IDbContext otherContext)
        {
            db = otherContext;
            crud = new Crud<Review>(db);
        }

        // GET: Reviews
        public ActionResult Index(int id)
        {
            var revs = crud.Table.Where(x => x.Restaurant.Id == id).OrderByDescending(x => x.Modified).ToList();
            TempData["RestaurantId"] = id;
            return View(revs);
        }

        // GET: Reviews/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Reviews/Create
        public ActionResult Create()
        {
            TempData.Keep("RestaurantId");
            ViewBag.RestaurantId = TempData.Peek("RestaurantId");
            TempData.Keep("RestaurantId");
            return View();
        }

        // POST: Reviews/Create
        [HttpPost]
        public ActionResult Create(Review review)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    int id = Convert.ToInt32(TempData.Peek("RestaurantId"));
                    review.RestaurantId = id;
                    //TempData.Keep("RestaurantId");
                    crud.Insert(review);
                    return RedirectToAction("Index", new {id = id});
                }
                else
                    return View(review);
            }
            catch
            {
                return View();
            }
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int id)
        {
            TempData.Keep("RestaurantId");
            ViewBag.RestaurantId = TempData.Peek("RestaurantId");
            TempData.Keep("RestaurantId");
            return View(crud.GetById(id));
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Review review)
        {
            try
            {
                // TODO: Add update logic here

                if (ModelState.IsValid)
                {
                    var rev = crud.GetById(id);

                    rev.Rating = review.Rating;
                    rev.User = review.User;
                    rev.Comment = review.Comment;

                    crud.Update(review);

                    int restid = Convert.ToInt32(TempData.Peek("RestaurantId"));
                    return RedirectToAction("Index", new {id = restid});
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

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Review rest = crud.GetById(id);
            if (rest == null)
            {
                return HttpNotFound();
            }

            TempData.Keep("RestaurantId");
            ViewBag.RestaurantId = TempData.Peek("RestaurantId");
            TempData.Keep("RestaurantId");
            return View(rest);
        }

        // POST: Reviews/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                Review rev = crud.GetById(id);
                if (rev != null)
                {
                    crud.Delete(rev);
                }

                int restid = Convert.ToInt32(TempData.Peek("RestaurantId"));
                return RedirectToAction("Index", new {id = restid});
            }
            catch
            {
                return View();
            }
        }
    }
}
