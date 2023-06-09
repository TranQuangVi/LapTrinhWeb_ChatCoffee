﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChatCoffee.Models.ModelsDefault
{
    [Table("COFFEE")]
    public partial class COFFEE
    {
        public COFFEE()
        {
            ANHs = new HashSet<ANH>();
            CTDONHANGs = new HashSet<CTDONHANG>();
            CTGIOHANGs = new HashSet<CTGIOHANG>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MACF { get; set; }
        [Required(ErrorMessage = "Tên sản phẩm không để trống")]
        [StringLength(50)]
        public string TENCF { get; set; }
        [Required(ErrorMessage = "Giá không để trống")]
        public int? GIA { get; set; }
        [Required(ErrorMessage = "Số lượng không để trống")]
        public int? SOLUONG { get; set; }

        public int ViewCount { get; set; }
        public int SLDABAN { get; set; }
        public int? KHOILUONG { get; set; }
        public bool TRANGTHAI { get; set; }

        [StringLength(20)]
        public string XUATXU { get; set; }

        public int? HSD { get; set; }

        [StringLength(10)]
        public string DANGCF { get; set; }

        [StringLength(500)]
        public string MOTA { get; set; }

        public int MALOAI { get; set; }

        public int MATH { get; set; }

        public virtual ICollection<ANH> ANHs { get; set; }

        public virtual ICollection<CTDONHANG> CTDONHANGs { get; set; }

        public virtual ICollection<CTGIOHANG> CTGIOHANGs { get; set; }

        public virtual LOAISANPHAM LOAISANPHAM { get; set; }

        public virtual THUONGHIEU THUONGHIEU { get; set; }
    }

    public enum TrangThai
    {
        True,
        False
    }
}