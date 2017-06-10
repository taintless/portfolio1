namespace EmployersSalary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmployerToUserFK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Employer_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "Employer_Id");
            AddForeignKey("dbo.AspNetUsers", "Employer_Id", "dbo.Employers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Employer_Id", "dbo.Employers");
            DropIndex("dbo.AspNetUsers", new[] { "Employer_Id" });
            DropColumn("dbo.AspNetUsers", "Employer_Id");
        }
    }
}
