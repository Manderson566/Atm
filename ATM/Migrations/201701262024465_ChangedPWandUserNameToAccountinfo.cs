namespace ATM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPWandUserNameToAccountinfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccountManagments", "Username", c => c.String());
            AddColumn("dbo.AccountManagments", "Password", c => c.String());
            DropColumn("dbo.Logins", "Username");
            DropColumn("dbo.Logins", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Logins", "Password", c => c.String());
            AddColumn("dbo.Logins", "Username", c => c.String());
            DropColumn("dbo.AccountManagments", "Password");
            DropColumn("dbo.AccountManagments", "Username");
        }
    }
}
