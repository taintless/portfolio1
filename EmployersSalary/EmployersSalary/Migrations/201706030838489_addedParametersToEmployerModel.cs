namespace EmployersSalary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedParametersToEmployerModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employers", "IsDisabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Employers", "DisabledOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employers", "DisabledOn");
            DropColumn("dbo.Employers", "IsDisabled");
        }
    }
}
