namespace S3.Train.WebPerFume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Banner",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Image = c.String(maxLength: 30),
                        Link = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Brand",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                        Summary = c.String(nullable: false, maxLength: 400),
                        Logo = c.String(nullable: false, maxLength: 160),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Vendor_Id = c.Guid(nullable: false),
                        Brand_Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 60),
                        Description = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vendor", t => t.Vendor_Id, cascadeDelete: true)
                .ForeignKey("dbo.Brand", t => t.Brand_Id, cascadeDelete: true)
                .Index(t => t.Vendor_Id)
                .Index(t => t.Brand_Id);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                        Summary = c.String(nullable: false, maxLength: 400),
                        ParentId = c.Guid(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.ProductVariation",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Product_Id = c.Guid(nullable: false),
                        SKU = c.String(nullable: false, maxLength: 20),
                        Volume = c.String(nullable: false, maxLength: 10),
                        StockQuantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShoppingCartDetail", t => t.Id)
                .ForeignKey("dbo.Product", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.ProductImage",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductVariation_Id = c.Guid(nullable: false),
                        ImagePath = c.String(nullable: false, maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductVariation", t => t.ProductVariation_Id, cascadeDelete: true)
                .Index(t => t.ProductVariation_Id);
            
            CreateTable(
                "dbo.ShoppingCartDetail",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ShoppingCart_Id = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShoppingCart", t => t.ShoppingCart_Id, cascadeDelete: true)
                .Index(t => t.ShoppingCart_Id);
            
            CreateTable(
                "dbo.ShoppingCart",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ShoppingCart_Id = c.Guid(nullable: false),
                        DeliveryName = c.String(nullable: false, maxLength: 30),
                        DeliveryAddress = c.String(nullable: false, maxLength: 60),
                        DeliveryPhone = c.String(nullable: false, maxLength: 12),
                        OrderDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShoppingCart", t => t.ShoppingCart_Id, cascadeDelete: true)
                .Index(t => t.ShoppingCart_Id);
            
            CreateTable(
                "dbo.Vendor",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                        Address = c.String(nullable: false, maxLength: 120),
                        Email = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(maxLength: 12),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductAdvertisement",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 100),
                        Description = c.String(maxLength: 500),
                        EventUrl = c.String(nullable: false, maxLength: 50),
                        EventUrlCaption = c.String(maxLength: 10),
                        ImagePath = c.String(nullable: false, maxLength: 200),
                        AdType = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProductCategory",
                c => new
                    {
                        ProductId = c.Guid(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.CategoryId })
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Product", "Brand_Id", "dbo.Brand");
            DropForeignKey("dbo.Product", "Vendor_Id", "dbo.Vendor");
            DropForeignKey("dbo.ProductVariation", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.ShoppingCartDetail", "ShoppingCart_Id", "dbo.ShoppingCart");
            DropForeignKey("dbo.Order", "ShoppingCart_Id", "dbo.ShoppingCart");
            DropForeignKey("dbo.ProductVariation", "Id", "dbo.ShoppingCartDetail");
            DropForeignKey("dbo.ProductImage", "ProductVariation_Id", "dbo.ProductVariation");
            DropForeignKey("dbo.ProductCategory", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.ProductCategory", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Category", "ParentId", "dbo.Category");
            DropIndex("dbo.ProductCategory", new[] { "CategoryId" });
            DropIndex("dbo.ProductCategory", new[] { "ProductId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Order", new[] { "ShoppingCart_Id" });
            DropIndex("dbo.ShoppingCartDetail", new[] { "ShoppingCart_Id" });
            DropIndex("dbo.ProductImage", new[] { "ProductVariation_Id" });
            DropIndex("dbo.ProductVariation", new[] { "Product_Id" });
            DropIndex("dbo.ProductVariation", new[] { "Id" });
            DropIndex("dbo.Category", new[] { "ParentId" });
            DropIndex("dbo.Product", new[] { "Brand_Id" });
            DropIndex("dbo.Product", new[] { "Vendor_Id" });
            DropTable("dbo.ProductCategory");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ProductAdvertisement");
            DropTable("dbo.Vendor");
            DropTable("dbo.Order");
            DropTable("dbo.ShoppingCart");
            DropTable("dbo.ShoppingCartDetail");
            DropTable("dbo.ProductImage");
            DropTable("dbo.ProductVariation");
            DropTable("dbo.Category");
            DropTable("dbo.Product");
            DropTable("dbo.Brand");
            DropTable("dbo.Banner");
        }
    }
}
