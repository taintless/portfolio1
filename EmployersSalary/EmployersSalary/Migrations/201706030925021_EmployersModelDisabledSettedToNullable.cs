namespace EmployersSalary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployersModelDisabledSettedToNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employers", "DisabledOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employers", "DisabledOn", c => c.DateTime(nullable: false));
        }
    }
}
