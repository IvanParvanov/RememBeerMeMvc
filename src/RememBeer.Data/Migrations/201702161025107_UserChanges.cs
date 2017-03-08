namespace RememBeer.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UserChanges : DbMigration
    {
        public override void Up()
        {
            this.DropForeignKey("dbo.BeerReviews", "UserId", "dbo.AspNetUsers");
            this.RenameColumn(table: "dbo.BeerReviews", name: "UserId", newName: "ApplicationUserId");
            this.RenameIndex(table: "dbo.BeerReviews", name: "IX_UserId", newName: "IX_ApplicationUserId");
            this.DropColumn("dbo.AspNetUsers", "Discriminator");
        }
        
        public override void Down()
        {
            this.AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            this.RenameIndex(table: "dbo.BeerReviews", name: "IX_ApplicationUserId", newName: "IX_UserId");
            this.RenameColumn(table: "dbo.BeerReviews", name: "ApplicationUserId", newName: "UserId");
            this.AddForeignKey("dbo.BeerReviews", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
