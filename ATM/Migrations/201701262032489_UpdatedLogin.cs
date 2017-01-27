namespace ATM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedLogin : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AccountManagments", "Username");
            DropColumn("dbo.AccountManagments", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccountManagments", "Password", c => c.String());
            AddColumn("dbo.AccountManagments", "Username", c => c.String());
        }
    }
}
