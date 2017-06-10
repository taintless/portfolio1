namespace EmployersSalary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedEmployerPK : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Employers");
            AlterColumn("dbo.Employers", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Employers", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Employers");
            AlterColumn("dbo.Employers", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Employers", new[] { "FirstName", "LastName" });
        }
    }
}
