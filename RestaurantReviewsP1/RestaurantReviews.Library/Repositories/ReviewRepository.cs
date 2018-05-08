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
    public class ReviewRepository : IRestaurantReviewsRepository<Review>
    {
        private readonly RestaurantReviewsContext _context;
        //private IDbSet<Restaurant> _entities;

        public ReviewRepository(RestaurantReviewsContext context)
        {
            this._context = context;
        }

        public Review GetById(object id)
        {
            //return this.Entities.Find(id);
            return DataToLibrary(this._context.Reviews.Find(id));
        }

        public void Insert(Review entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                //this.Entities.Add(entity);
                this._context.Reviews.Add(LibraryToData(entity));
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

        public void Update(Review entity)
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

        public void Delete(Review entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                //this.Entities.Remove(entity);
                this._context.Reviews.Remove(LibraryToData(entity));
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

        public virtual IEnumerable<Review> Table
        {
            get
            {
                //return this.Entities;
                var list = this._context.Reviews.ToList();
                return list.Select(x => DataToLibrary(x)).ToList();
            }
        }

        //private IDbSet<Review> Entities
        //{
        //    get
        //    {
        //        if (_entities == null)
        //        {
        //            _entities = _context.Set<Review>();
        //        }
        //        return _entities;
        //    }
        //}

        public static Review DataToLibrary(RestaurantReviews.Data.Models.Review dataModel)
        {
            var libModel = new Review()
            {
                Id = dataModel.Id,
                Rating = dataModel.Rating,
                Comment = dataModel.Comment,
                User = dataModel.User,
                Created = dataModel.Created,
                Modified = dataModel.Modified,
                RestaurantId = dataModel.RestaurantId
            };
            return libModel;
        }

        public static RestaurantReviews.Data.Models.Review LibraryToData(Review libModel)
        {
            var dataModel = new RestaurantReviews.Data.Models.Review()
            {
                Rating = libModel.Rating,
                Comment = libModel.Comment,
                User = libModel.User,
                RestaurantId = libModel.RestaurantId
            };
            return dataModel;
        }
    }

}
