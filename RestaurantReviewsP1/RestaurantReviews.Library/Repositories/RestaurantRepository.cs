using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Data;
using RestaurantReviews.Library.Models;

namespace RestaurantReviews.Library.Repositories
{
    public class RestaurantRepository : IRestaurantReviewsRepository<Restaurant>
    {
        private readonly RestaurantReviewsContext _context;
        //private DbSet<Restaurant> _entities;

        public RestaurantRepository(RestaurantReviewsContext context)
        {
            this._context = context;
        }

        public Restaurant GetById(object id)
        {
            return DataToLibrary(this._context.Restaurants.Find(id));
        }

        public void Insert(Restaurant entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                //this.Entities.Add(entity);
                this._context.Restaurants.Add(LibraryToData(entity));
                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                //Call Nlog Code...
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public void Update(Restaurant entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public void Delete(Restaurant entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                //this.Entities.Remove(entity);
                this._context.Restaurants.Remove(LibraryToData(entity));
                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public virtual IEnumerable<Restaurant> Table
        {
            get
            {
                //return this._context.Restaurant;
                var list =  this._context.Restaurants.ToList();
                return list.Select(x => DataToLibrary(x)).ToList();
            }
        }

        //private IDbSet<RestaurantReviews.Data.Models.Restaurant> Entities
        //{
        //    get
        //    {
        //        if (_entities == null)
        //        {
        //            _entities = _context.Set<RestaurantReviews.Data.Models.Restaurant>();
        //        }
        //        return _entities;
        //    }
        //}

        public static Restaurant DataToLibrary(RestaurantReviews.Data.Models.Restaurant dataModel)
        {
            double rating = 0;
            using (var db = new RestaurantReviewsContext())
            {
                var reviews = db.Reviews.Where(r => r.RestaurantId == dataModel.Id);
                if (reviews.Count() != 0)
                    rating = reviews.Average(r => r.Rating);
            }

            var list = dataModel.Reviews.ToList();

            var libModel = new Restaurant()
            {
                Id = dataModel.Id,
                Name = dataModel.Name,
                Street = dataModel.Street,
                City = dataModel.City,
                State = dataModel.State,
                Country = dataModel.Country,
                Zipcode = dataModel.Zipcode,
                Phone = dataModel.Phone,
                Website = dataModel.Website,
                Created = dataModel.Created,
                Modified = dataModel.Modified,
                AverageRating = Math.Truncate(rating * 100) / 100,
                Reviews = list.Select(x => ReviewRepository.DataToLibrary(x)).ToList()
                //reviews = (List<Review>) dataModel.Reviews
            };
            return libModel;
        }

        public static RestaurantReviews.Data.Models.Restaurant LibraryToData(Restaurant libModel)
        {
            var dataModel = new RestaurantReviews.Data.Models.Restaurant()
            {
                Name = libModel.Name,
                Street = libModel.Street,
                City = libModel.City,
                State = libModel.State,
                Country = libModel.Country,
                Zipcode = libModel.Zipcode,
                Phone = libModel.Phone,
                Website = libModel.Website
            };
            return dataModel;
        }

    }
}
