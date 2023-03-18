using ChatCoffee.Models.ModelsDefault;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ChatCoffee.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
            : base("ChatCoffeeDBContext", throwIfV1Schema: false)
        {
        }
        public virtual DbSet<ThongKe> THONGKEs { get; set; }
        public virtual DbSet<ANH> ANHs { get; set; }
        public virtual DbSet<COFFEE> COFFEEs { get; set; }
        public virtual DbSet<CTDONHANG> CTDONHANGs { get; set; }
        public virtual DbSet<CTGIOHANG> CTGIOHANGs { get; set; }
        public virtual DbSet<GIOHANG> GIOHANGs { get; set; }
        public virtual DbSet<HOADON> HOADONs { get; set; }
        public virtual DbSet<KHACHHANG> KHACHHANGs { get; set; }
        public virtual DbSet<LOAISANPHAM> LOAISANPHAMs { get; set; }
        public virtual DbSet<THANHTOAN> THANHTOANs { get; set; }
        public virtual DbSet<THUONGHIEU> THUONGHIEUs { get; set; }
        public virtual DbSet<VANCHUYEN> VANCHUYENs { get; set; }
        public virtual DbSet<DIACHI> DIACHIs { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}