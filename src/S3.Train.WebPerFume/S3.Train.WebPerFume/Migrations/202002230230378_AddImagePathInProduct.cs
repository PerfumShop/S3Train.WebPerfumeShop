namespace S3.Train.WebPerFume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImagePathInProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "ImagePath", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "ImagePath");
        }
    }
}
