namespace ATM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedAccountManagement : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccountManagments", "Account_Id", "dbo.AccountInfoes");
            DropIndex("dbo.AccountManagments", new[] { "Account_Id" });
            DropTable("dbo.AccountManagments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AccountManagments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.AccountManagments", "Account_Id");
            AddForeignKey("dbo.AccountManagments", "Account_Id", "dbo.AccountInfoes", "Id");
        }
    }
}
