namespace RestaurantReviews.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeForeignKeyName : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Restaurant.Review", "Restaurant_Id", "Restaurant.Restaurant");
            DropIndex("Restaurant.Review", new[] { "Restaurant_Id" });
            RenameColumn(table: "Restaurant.Review", name: "Restaurant_Id", newName: "RestaurantId");
            AlterColumn("Restaurant.Review", "RestaurantId", c => c.Int(nullable: false));
            CreateIndex("Restaurant.Review", "RestaurantId");
            AddForeignKey("Restaurant.Review", "RestaurantId", "Restaurant.Restaurant", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("Restaurant.Review", "RestaurantId", "Restaurant.Restaurant");
            DropIndex("Restaurant.Review", new[] { "RestaurantId" });
            AlterColumn("Restaurant.Review", "RestaurantId", c => c.Int());
            RenameColumn(table: "Restaurant.Review", name: "RestaurantId", newName: "Restaurant_Id");
            CreateIndex("Restaurant.Review", "Restaurant_Id");
            AddForeignKey("Restaurant.Review", "Restaurant_Id", "Restaurant.Restaurant", "Id");
        }
    }
}
