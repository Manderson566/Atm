namespace ATM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLoginChangedAllTableData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserInfoes", t => t.Account_Id)
                .Index(t => t.Account_Id);
            
            AddColumn("dbo.AccountInfoes", "Withdraw", c => c.Double(nullable: false));
            AddColumn("dbo.AccountInfoes", "Deposit", c => c.Double(nullable: false));
            AddColumn("dbo.AccountInfoes", "UserInfo_Id", c => c.Int());
            AddColumn("dbo.AccountManagments", "Account_Id", c => c.Int());
            CreateIndex("dbo.AccountInfoes", "UserInfo_Id");
            CreateIndex("dbo.AccountManagments", "Account_Id");
            AddForeignKey("dbo.AccountInfoes", "UserInfo_Id", "dbo.UserInfoes", "Id");
            AddForeignKey("dbo.AccountManagments", "Account_Id", "dbo.AccountInfoes", "Id");
            DropColumn("dbo.AccountManagments", "Withdraw");
            DropColumn("dbo.AccountManagments", "Deposit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccountManagments", "Deposit", c => c.Double(nullable: false));
            AddColumn("dbo.AccountManagments", "Withdraw", c => c.Double(nullable: false));
            DropForeignKey("dbo.AccountManagments", "Account_Id", "dbo.AccountInfoes");
            DropForeignKey("dbo.Logins", "Account_Id", "dbo.UserInfoes");
            DropForeignKey("dbo.AccountInfoes", "UserInfo_Id", "dbo.UserInfoes");
            DropIndex("dbo.AccountManagments", new[] { "Account_Id" });
            DropIndex("dbo.Logins", new[] { "Account_Id" });
            DropIndex("dbo.AccountInfoes", new[] { "UserInfo_Id" });
            DropColumn("dbo.AccountManagments", "Account_Id");
            DropColumn("dbo.AccountInfoes", "UserInfo_Id");
            DropColumn("dbo.AccountInfoes", "Deposit");
            DropColumn("dbo.AccountInfoes", "Withdraw");
            DropTable("dbo.Logins");
        }
    }
}
