using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChatCoffee.Models.ModelsDefault
{
    [Table("CTGIOHANG")]
    public partial class CTGIOHANG
    {
        public int? SOLUONG { get; set; }

        public int? GIA { get; set; }

        [Key]
        [Column(Order = 0)]
        public int MACF { get; set; }

        [Key]
        [Column(Order = 1)]
        public int MAGH { get; set; }

        public virtual COFFEE COFFEE { get; set; }

        public virtual GIOHANG GIOHANG { get; set; }
    }
}