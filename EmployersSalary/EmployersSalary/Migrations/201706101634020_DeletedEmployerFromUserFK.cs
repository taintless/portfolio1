namespace EmployersSalary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedEmployerFromUserFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", new[] { "Employer_FirstName", "Employer_LastName" }, "dbo.Employers");
            DropIndex("dbo.AspNetUsers", new[] { "Employer_FirstName", "Employer_LastName" });
            DropColumn("dbo.AspNetUsers", "Employer_FirstName");
            DropColumn("dbo.AspNetUsers", "Employer_LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Employer_LastName", c => c.String(maxLength: 225));
            AddColumn("dbo.AspNetUsers", "Employer_FirstName", c => c.String(maxLength: 225));
            CreateIndex("dbo.AspNetUsers", new[] { "Employer_FirstName", "Employer_LastName" });
            AddForeignKey("dbo.AspNetUsers", new[] { "Employer_FirstName", "Employer_LastName" }, "dbo.Employers", new[] { "FirstName", "LastName" });
        }
    }
}
