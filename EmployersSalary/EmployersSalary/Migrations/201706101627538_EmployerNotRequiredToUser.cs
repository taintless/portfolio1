namespace EmployersSalary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployerNotRequiredToUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", new[] { "Employer_FirstName", "Employer_LastName" }, "dbo.Employers");
            DropIndex("dbo.AspNetUsers", new[] { "Employer_FirstName", "Employer_LastName" });
            AlterColumn("dbo.AspNetUsers", "Employer_FirstName", c => c.String(maxLength: 225));
            AlterColumn("dbo.AspNetUsers", "Employer_LastName", c => c.String(maxLength: 225));
            CreateIndex("dbo.AspNetUsers", new[] { "Employer_FirstName", "Employer_LastName" });
            AddForeignKey("dbo.AspNetUsers", new[] { "Employer_FirstName", "Employer_LastName" }, "dbo.Employers", new[] { "FirstName", "LastName" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", new[] { "Employer_FirstName", "Employer_LastName" }, "dbo.Employers");
            DropIndex("dbo.AspNetUsers", new[] { "Employer_FirstName", "Employer_LastName" });
            AlterColumn("dbo.AspNetUsers", "Employer_LastName", c => c.String(nullable: false, maxLength: 225));
            AlterColumn("dbo.AspNetUsers", "Employer_FirstName", c => c.String(nullable: false, maxLength: 225));
            CreateIndex("dbo.AspNetUsers", new[] { "Employer_FirstName", "Employer_LastName" });
            AddForeignKey("dbo.AspNetUsers", new[] { "Employer_FirstName", "Employer_LastName" }, "dbo.Employers", new[] { "FirstName", "LastName" }, cascadeDelete: true);
        }
    }
}
