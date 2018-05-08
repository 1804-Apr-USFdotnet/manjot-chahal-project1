using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Data.Models
{
    public abstract class BaseModel
    {
        DateTime Created { get; set; }
        DateTime? Modified { get; set; }
    }
}
