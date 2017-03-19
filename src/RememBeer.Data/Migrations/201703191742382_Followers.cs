using System.Data.Entity.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace RememBeer.Data.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class Followers : DbMigration
    {
        public override void Up()
        {
            this.DropForeignKey("dbo.BeerReviews", "BeerId", "dbo.Beers");
            this.DropForeignKey("dbo.BeerReviews", "ApplicationUserId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.Beers", "BeerTypeId", "dbo.BeerTypes");
            this.DropForeignKey("dbo.Beers", "BreweryId", "dbo.Breweries");
            this.DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            this.CreateTable(
                "dbo.Followers",
                c => new
                    {
                        FirstUserId = c.String(nullable: false, maxLength: 128),
                        SecondUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.FirstUserId, t.SecondUserId })
                .ForeignKey("dbo.AspNetUsers", t => t.FirstUserId)
                .ForeignKey("dbo.AspNetUsers", t => t.SecondUserId)
                .Index(t => t.FirstUserId)
                .Index(t => t.SecondUserId);

            this.AddForeignKey("dbo.BeerReviews", "BeerId", "dbo.Beers", "Id");
            this.AddForeignKey("dbo.BeerReviews", "ApplicationUserId", "dbo.AspNetUsers", "Id");
            this.AddForeignKey("dbo.Beers", "BeerTypeId", "dbo.BeerTypes", "Id");
            this.AddForeignKey("dbo.Beers", "BreweryId", "dbo.Breweries", "Id");
            this.AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id");
            this.AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id");
            this.AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id");
            this.AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id");
        }
        
        public override void Down()
        {
            this.DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            this.DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.Beers", "BreweryId", "dbo.Breweries");
            this.DropForeignKey("dbo.Beers", "BeerTypeId", "dbo.BeerTypes");
            this.DropForeignKey("dbo.BeerReviews", "ApplicationUserId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.BeerReviews", "BeerId", "dbo.Beers");
            this.DropForeignKey("dbo.Followers", "SecondUserId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.Followers", "FirstUserId", "dbo.AspNetUsers");
            this.DropIndex("dbo.Followers", new[] { "SecondUserId" });
            this.DropIndex("dbo.Followers", new[] { "FirstUserId" });
            this.DropTable("dbo.Followers");
            this.AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
            this.AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            this.AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            this.AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            this.AddForeignKey("dbo.Beers", "BreweryId", "dbo.Breweries", "Id", cascadeDelete: true);
            this.AddForeignKey("dbo.Beers", "BeerTypeId", "dbo.BeerTypes", "Id", cascadeDelete: true);
            this.AddForeignKey("dbo.BeerReviews", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            this.AddForeignKey("dbo.BeerReviews", "BeerId", "dbo.Beers", "Id", cascadeDelete: true);
        }
    }
}
