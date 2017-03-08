namespace RememBeer.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class IsDeletedBeer : DbMigration
    {
        public override void Up()
        {
            this.AddColumn("dbo.Beers", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            this.DropColumn("dbo.Beers", "IsDeleted");
        }
    }
}
