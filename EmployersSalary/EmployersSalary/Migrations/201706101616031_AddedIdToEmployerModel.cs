namespace EmployersSalary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIdToEmployerModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employers", "Id", c => c.Int(nullable: false));
            AddColumn("dbo.Employers", "RegularEmployer", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employers", "RegularEmployer");
            DropColumn("dbo.Employers", "Id");
        }
    }
}
