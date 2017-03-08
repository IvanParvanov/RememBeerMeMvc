namespace RememBeer.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DateTimeFixes : DbMigration
    {
        public override void Up()
        {
            this.AlterColumn("dbo.BeerReviews", "CreatedAt", c => c.DateTime(nullable: false));
            this.AlterColumn("dbo.BeerReviews", "ModifiedAt", c => c.DateTime(nullable: false));
        }

        public override void Down()
        {
            this.AlterColumn("dbo.BeerReviews", "ModifiedAt", c => c.DateTime());
            this.AlterColumn("dbo.BeerReviews", "CreatedAt", c => c.DateTime());
        }
    }
}
