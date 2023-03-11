using System;
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

        [StringLength(50)]
        public string TENCF { get; set; }

        public int? GIA { get; set; }

        public int? SOLUONG { get; set; }

        public int? KHOILUONG { get; set; }

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
}