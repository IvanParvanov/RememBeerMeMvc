namespace RememBeer.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ReviewModelChanges : DbMigration
    {
        public override void Up()
        {
            this.AlterColumn("dbo.BeerReviews", "Place", c => c.String(nullable: false, maxLength: 128));
        }

        public override void Down()
        {
            this.AlterColumn("dbo.BeerReviews", "Place", c => c.String(nullable: false, maxLength: 512));
        }
    }
}
