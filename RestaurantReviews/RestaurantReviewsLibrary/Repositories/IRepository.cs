using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewsLibrary.Repositories
{
    interface IRepository<T>
    {
        void Add(T entity); // create
        IEnumerable<T> GetAll(); // read
        T GetById(int id);
        void Update(T entity); //update
        void Remove(T entity); //delete

    }
}
