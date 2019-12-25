namespace SaveTime.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeService : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Services", "SpendTimeInMinutes", c => c.Double(nullable: false));
            DropColumn("dbo.Services", "ApproximatelySpendTimeInMinutes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Services", "ApproximatelySpendTimeInMinutes", c => c.Double(nullable: false));
            DropColumn("dbo.Services", "SpendTimeInMinutes");
        }
    }
}
