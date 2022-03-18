namespace AR_IS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tbl12300aa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAccessCopies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Form_id = c.Int(nullable: false),
                        SubForm_id = c.Int(nullable: false),
                        SuperForm_id = c.Int(nullable: false),
                        Comid = c.Int(nullable: false),
                        Username = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserAccessCopies");
        }
    }
}
