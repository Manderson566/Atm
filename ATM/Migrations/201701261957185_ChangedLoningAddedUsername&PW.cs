namespace ATM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedLoningAddedUsernamePW : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logins", "Username", c => c.String());
            AddColumn("dbo.Logins", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Logins", "Password");
            DropColumn("dbo.Logins", "Username");
        }
    }
}
