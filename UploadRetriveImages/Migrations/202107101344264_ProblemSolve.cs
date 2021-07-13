namespace UploadRetriveImages.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProblemSolve : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Standard = c.Int(nullable: false),
                        Imagepath = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Students");
        }
    }
}
