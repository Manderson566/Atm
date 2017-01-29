namespace ATM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TryingToNotLetMultipleUsersCreateTheSameAccount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccountInfoes", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccountInfoes", "DateTime");
        }
    }
}
