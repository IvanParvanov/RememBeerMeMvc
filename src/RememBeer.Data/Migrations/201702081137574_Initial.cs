namespace RememBeer.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                             "dbo.BeerReviews",
                             c => new
                                  {
                                      Id = c.Int(nullable: false, identity: true),
                                      BeerId = c.Int(nullable: false),
                                      UserId = c.String(nullable: false, maxLength: 128),
                                      Overall = c.Int(nullable: false),
                                      Look = c.Int(nullable: false),
                                      Smell = c.Int(nullable: false),
                                      Taste = c.Int(nullable: false),
                                      Description = c.String(nullable: false),
                                      CreatedAt = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                                      ModifiedAt = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                                      IsPublic = c.Boolean(nullable: false, defaultValue: true),
                                      IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                                      Place = c.String(),
                                  })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Beers", t => t.BeerId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.BeerId)
                .Index(t => t.UserId);

            this.CreateTable(
                             "dbo.Beers",
                             c => new
                                  {
                                      Id = c.Int(nullable: false, identity: true),
                                      BeerTypeId = c.Int(nullable: false),
                                      Name = c.String(nullable: false),
                                      BreweryId = c.Int(nullable: false),
                                  })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BeerTypes", t => t.BeerTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Breweries", t => t.BreweryId, cascadeDelete: true)
                .Index(t => t.BeerTypeId)
                .Index(t => t.BreweryId);

            this.CreateTable(
                             "dbo.BeerTypes",
                             c => new
                                  {
                                      Id = c.Int(nullable: false, identity: true),
                                      Type = c.String(nullable: false),
                                  })
                .PrimaryKey(t => t.Id);

            this.CreateTable(
                             "dbo.Breweries",
                             c => new
                                  {
                                      Id = c.Int(nullable: false, identity: true),
                                      Name = c.String(nullable: false),
                                      Description = c.String(nullable: false),
                                      Country = c.String(nullable: false),
                                  })
                .PrimaryKey(t => t.Id);

            this.CreateTable(
                             "dbo.AspNetUsers",
                             c => new
                                  {
                                      Id = c.String(nullable: false, maxLength: 128),
                                      Email = c.String(maxLength: 256),
                                      EmailConfirmed = c.Boolean(nullable: false),
                                      PasswordHash = c.String(),
                                      SecurityStamp = c.String(),
                                      PhoneNumber = c.String(),
                                      PhoneNumberConfirmed = c.Boolean(nullable: false),
                                      TwoFactorEnabled = c.Boolean(nullable: false),
                                      LockoutEndDateUtc = c.DateTime(),
                                      LockoutEnabled = c.Boolean(nullable: false),
                                      AccessFailedCount = c.Int(nullable: false),
                                      UserName = c.String(nullable: false, maxLength: 256),
                                      Discriminator = c.String(nullable: false, maxLength: 128),
                                  })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            this.CreateTable(
                             "dbo.AspNetUserClaims",
                             c => new
                                  {
                                      Id = c.Int(nullable: false, identity: true),
                                      UserId = c.String(nullable: false, maxLength: 128),
                                      ClaimType = c.String(),
                                      ClaimValue = c.String(),
                                  })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            this.CreateTable(
                             "dbo.AspNetUserLogins",
                             c => new
                                  {
                                      LoginProvider = c.String(nullable: false, maxLength: 128),
                                      ProviderKey = c.String(nullable: false, maxLength: 128),
                                      UserId = c.String(nullable: false, maxLength: 128),
                                  })
                .PrimaryKey(t => new
                                 {
                                     t.LoginProvider,
                                     t.ProviderKey,
                                     t.UserId
                                 })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            this.CreateTable(
                             "dbo.AspNetUserRoles",
                             c => new
                                  {
                                      UserId = c.String(nullable: false, maxLength: 128),
                                      RoleId = c.String(nullable: false, maxLength: 128),
                                  })
                .PrimaryKey(t => new
                                 {
                                     t.UserId,
                                     t.RoleId
                                 })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            this.CreateTable(
                             "dbo.AspNetRoles",
                             c => new
                                  {
                                      Id = c.String(nullable: false, maxLength: 128),
                                      Name = c.String(nullable: false, maxLength: 256),
                                  })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
        }

        public override void Down()
        {
            this.DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            this.DropForeignKey("dbo.BeerReviews", "UserId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.BeerReviews", "BeerId", "dbo.Beers");
            this.DropForeignKey("dbo.Beers", "BreweryId", "dbo.Breweries");
            this.DropForeignKey("dbo.Beers", "BeerTypeId", "dbo.BeerTypes");
            this.DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            this.DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            this.DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            this.DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            this.DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            this.DropIndex("dbo.AspNetUsers", "UserNameIndex");
            this.DropIndex("dbo.Beers", new[] { "BreweryId" });
            this.DropIndex("dbo.Beers", new[] { "BeerTypeId" });
            this.DropIndex("dbo.BeerReviews", new[] { "UserId" });
            this.DropIndex("dbo.BeerReviews", new[] { "BeerId" });
            this.DropTable("dbo.AspNetRoles");
            this.DropTable("dbo.AspNetUserRoles");
            this.DropTable("dbo.AspNetUserLogins");
            this.DropTable("dbo.AspNetUserClaims");
            this.DropTable("dbo.AspNetUsers");
            this.DropTable("dbo.Breweries");
            this.DropTable("dbo.BeerTypes");
            this.DropTable("dbo.Beers");
            this.DropTable("dbo.BeerReviews");
        }
    }
}
