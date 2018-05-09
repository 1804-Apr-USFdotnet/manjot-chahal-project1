using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RestaurantReviews.Library;
using RestaurantReviews.Library.Models;
using NLog;

namespace RestaurantReviews.Web.Controllers
{
    public class ReviewsController : Controller
    {
        private Service service;
        public Logger logger;

        public ReviewsController()
        {
            service = new Service();
            logger = NLog.LogManager.GetCurrentClassLogger();
        }

        // GET: Reviews
        //add sorting
        public ActionResult Index(int id)
        {
            var revs = service.GetAllReviewsByRestaurant(id).OrderByDescending(x => x.Modified).ToList();
            TempData["RestaurantId"] = id;
            return View(revs);
        }

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
                if (ModelState.IsValid)
                {
                    int id = Convert.ToInt32(TempData.Peek("RestaurantId"));
                    review.RestaurantId = id;
                    //TempData.Keep("RestaurantId");
                    service.InsertReview(review);
                    logger.Info("Successfully added new review: " + review.Rating + " " + review.Comment);
                    return RedirectToAction("Index", new {id = id});
                }
                else
                    return View(review);
            }
            catch
            {
                logger.Error("Failed to add review: " + review.User + " " + review.Rating + " " + review.Comment);
                return View();
            }
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int id)
        {
            TempData.Keep("RestaurantId");
            ViewBag.RestaurantId = TempData.Peek("RestaurantId");
            TempData.Keep("RestaurantId");
            return View(service.GetReviewById(id));
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Review review)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.UpdateReview(review);
                    logger.Info("Successfully updated review: " + review.User + " " + review.Rating + " " + review.Comment);
                    int restid = Convert.ToInt32(TempData.Peek("RestaurantId"));
                    return RedirectToAction("Index", new {id = restid});
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                logger.Error("Failed to edit review: " + review.User + " " + review.Rating + " " + review.Comment);
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

            Review rev = service.GetReviewById(id);
            if (rev == null)
            {
                return HttpNotFound();
            }

            TempData.Keep("RestaurantId");
            ViewBag.RestaurantId = TempData.Peek("RestaurantId");
            TempData.Keep("RestaurantId");
            return View(rev);
        }

        // POST: Reviews/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var rev = service.GetReviewById(id);
            string name = rev.User + " " + rev.Rating + " " + rev.Comment;
            try
            {
                service.DeleteReview(id);
                logger.Info("Successfully deleted review: " + name);
                int restid = Convert.ToInt32(TempData.Peek("RestaurantId"));
                return RedirectToAction("Index", new {id = restid});
            }
            catch
            {
                logger.Info("Failed to delete review: " + name);
                return View();
            }
        }
    }
}
