namespace RestaurantReviewsData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Restaurant.Review")]
    public partial class Review
    {
        public int id { get; set; }

        public int rating { get; set; }

        [Column("review")]
        [StringLength(500)]
        public string review { get; set; }

        [StringLength(50)]
        public string user { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime date { get; set; }

        public int restaurantid { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }
}
