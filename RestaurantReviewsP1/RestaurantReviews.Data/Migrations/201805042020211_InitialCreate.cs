namespace RestaurantReviews.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Restaurant.Restaurant",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Street = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        Zipcode = c.String(nullable: false),
                        Phone = c.String(),
                        Website = c.String(),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Restaurant.Review",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rating = c.Int(nullable: false),
                        User = c.String(),
                        Comment = c.String(maxLength: 500),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        Restaurant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Restaurant.Restaurant", t => t.Restaurant_Id)
                .Index(t => t.Restaurant_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Restaurant.Review", "Restaurant_Id", "Restaurant.Restaurant");
            DropIndex("Restaurant.Review", new[] { "Restaurant_Id" });
            DropTable("Restaurant.Review");
            DropTable("Restaurant.Restaurant");
        }
    }
}
