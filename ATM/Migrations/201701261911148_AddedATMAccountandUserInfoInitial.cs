namespace ATM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedATMAccountandUserInfoInitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Balance = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AccountManagments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Withdraw = c.Double(nullable: false),
                        Deposit = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        JoinDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserInfoes");
            DropTable("dbo.AccountManagments");
            DropTable("dbo.AccountInfoes");
        }
    }
}
