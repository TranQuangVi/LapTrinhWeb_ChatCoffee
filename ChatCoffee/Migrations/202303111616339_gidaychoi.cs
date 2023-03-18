namespace ChatCoffee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gidaychoi : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ANH",
                c => new
                {
                    MAANH = c.Int(nullable: false, identity: true),
                    LINKANH = c.String(maxLength: 100),
                    MACF = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.MAANH)
                .ForeignKey("dbo.COFFEE", t => t.MACF, cascadeDelete: true)
                .Index(t => t.MACF);

            CreateTable(
                "dbo.COFFEE",
                c => new
                {
                    MACF = c.Int(nullable: false, identity: true),
                    TENCF = c.String(maxLength: 50),
                    GIA = c.Int(),
                    SOLUONG = c.Int(),
                    KHOILUONG = c.Int(),
                    XUATXU = c.String(maxLength: 20),
                    HSD = c.Int(),
                    ViewCount = c.Int(),
                    SLDABAN =  c.Int(),
                    DANGCF = c.String(maxLength: 10),
                    MOTA = c.String(maxLength: 500),
                    MALOAI = c.Int(nullable: false),
                    MATH = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.MACF)
                .ForeignKey("dbo.LOAISANPHAM", t => t.MALOAI, cascadeDelete: true)
                .ForeignKey("dbo.THUONGHIEU", t => t.MATH, cascadeDelete: true)
                .Index(t => t.MALOAI)
                .Index(t => t.MATH);

            CreateTable(
                "dbo.CTDONHANG",
                c => new
                {
                    MACF = c.Int(nullable: false),
                    MAHD = c.Int(nullable: false),
                    SOLUONG = c.Int(),
                    GIA = c.Int(),
                })
                .PrimaryKey(t => new { t.MACF, t.MAHD })
                .ForeignKey("dbo.COFFEE", t => t.MACF, cascadeDelete: true)
                .ForeignKey("dbo.HOADON", t => t.MAHD, cascadeDelete: true)
                .Index(t => t.MACF)
                .Index(t => t.MAHD);

            CreateTable(
                "dbo.HOADON",
                c => new
                {
                    MAHD = c.Int(nullable: false, identity: true),
                    TONGDONGIA = c.Int(),
                    Id = c.String(nullable: false, maxLength: 128),
                    MAVT = c.Int(nullable: false),
                    MATT = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.MAHD)
                .ForeignKey("dbo.AspNetUsers", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.THANHTOAN", t => t.MATT, cascadeDelete: true)
                .ForeignKey("dbo.VANCHUYEN", t => t.MAVT, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.MAVT)
                .Index(t => t.MATT);

            CreateTable(
               "dbo.ThongKes",
               c => new
               {
                   Id = c.Int(nullable: false, identity: true),
                   ThoiGian = c.DateTime(nullable: false),
                   SoTruyCap = c.Long(nullable: false),
               })
               .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.DIACHIs",
                c => new
                {
                    MADC = c.Int(nullable: false, identity: true),
                    TENDC = c.String(maxLength: 20),
                })
                .PrimaryKey(t => t.MADC);

            CreateTable(
                "dbo.GIOHANG",
                c => new
                {
                    MAGH = c.Int(nullable: false, identity: true),
                    Id = c.String(nullable: false, maxLength: 128),
                    TONGTIEN = c.Double(),
                    TONGSL = c.Int(),
                    TONGSP = c.Int(),
                })
                .PrimaryKey(t => t.MAGH)
                .ForeignKey("dbo.AspNetUsers", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.CTGIOHANG",
                c => new
                {
                    MACF = c.Int(nullable: false),
                    MAGH = c.Int(nullable: false),
                    SOLUONG = c.Int(),
                    GIA = c.Int(),
                    TONGGIA = c.Double(),
                })
                .PrimaryKey(t => new { t.MACF, t.MAGH })
                .ForeignKey("dbo.COFFEE", t => t.MACF, cascadeDelete: true)
                .ForeignKey("dbo.GIOHANG", t => t.MAGH, cascadeDelete: true)
                .Index(t => t.MACF)
                .Index(t => t.MAGH);

            CreateTable(
                "dbo.THANHTOAN",
                c => new
                {
                    MATT = c.Int(nullable: false, identity: true),
                    PHUONGTHUC = c.String(maxLength: 20),
                })
                .PrimaryKey(t => t.MATT);

            CreateTable(
                "dbo.VANCHUYEN",
                c => new
                {
                    MAVT = c.Int(nullable: false, identity: true),
                    TENVT = c.String(maxLength: 20),
                    GIA = c.Int(),
                })
                .PrimaryKey(t => t.MAVT);

            CreateTable(
                "dbo.LOAISANPHAM",
                c => new
                {
                    MALOAI = c.Int(nullable: false, identity: true),
                    TENLOAI = c.String(maxLength: 20),
                })
                .PrimaryKey(t => t.MALOAI);

            CreateTable(
                "dbo.THUONGHIEU",
                c => new
                {
                    MATH = c.Int(nullable: false, identity: true),
                    TENTH = c.String(maxLength: 100),
                })
                .PrimaryKey(t => t.MATH);

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
                        FullName = c.String(),
                        Phone = c.String(),
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
                        ANH = c.String(maxLength: 100),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.COFFEE", "MATH", "dbo.THUONGHIEU");
            DropForeignKey("dbo.COFFEE", "MALOAI", "dbo.LOAISANPHAM");
            DropForeignKey("dbo.HOADON", "MAVT", "dbo.VANCHUYEN");
            DropForeignKey("dbo.HOADON", "MATT", "dbo.THANHTOAN");
            DropForeignKey("dbo.HOADON", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.GIOHANG", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CTGIOHANG", "MAGH", "dbo.GIOHANG");
            DropForeignKey("dbo.CTGIOHANG", "MACF", "dbo.COFFEE");
            DropForeignKey("dbo.AspNetUsers", "MADC", "dbo.DIACHIs");
            DropForeignKey("dbo.CTDONHANG", "MAHD", "dbo.HOADON");
            DropForeignKey("dbo.CTDONHANG", "MACF", "dbo.COFFEE");
            DropForeignKey("dbo.ANH", "MACF", "dbo.COFFEE");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.CTGIOHANG", new[] { "MAGH" });
            DropIndex("dbo.CTGIOHANG", new[] { "MACF" });
            DropIndex("dbo.GIOHANG", new[] { "Id" });
            DropIndex("dbo.AspNetUsers", new[] { "MADC" });
            DropIndex("dbo.HOADON", new[] { "MATT" });
            DropIndex("dbo.HOADON", new[] { "MAVT" });
            DropIndex("dbo.HOADON", new[] { "Id" });
            DropIndex("dbo.CTDONHANG", new[] { "MAHD" });
            DropIndex("dbo.CTDONHANG", new[] { "MACF" });
            DropIndex("dbo.COFFEE", new[] { "MATH" });
            DropIndex("dbo.COFFEE", new[] { "MALOAI" });
            DropIndex("dbo.ANH", new[] { "MACF" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.THUONGHIEU");
            DropTable("dbo.LOAISANPHAM");
            DropTable("dbo.VANCHUYEN");
            DropTable("dbo.THANHTOAN");
            DropTable("dbo.CTGIOHANG");
            DropTable("dbo.GIOHANG");
            DropTable("dbo.DIACHIs");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.HOADON");
            DropTable("dbo.CTDONHANG");
            DropTable("dbo.COFFEE");
            DropTable("dbo.ANH");
            DropTable("dbo.ThongKes");
        }
    }
}
