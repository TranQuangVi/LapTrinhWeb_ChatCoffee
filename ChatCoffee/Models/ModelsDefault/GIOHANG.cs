using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChatCoffee.Models.ModelsDefault
{
    [Table("GIOHANG")]
    public partial class GIOHANG
    {
        public GIOHANG()
        {
            CTGIOHANGs = new HashSet<CTGIOHANG>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MAGH { get; set; }

        public int MAKH { get; set; }

        public virtual ICollection<CTGIOHANG> CTGIOHANGs { get; set; }

        public virtual KHACHHANG KHACHHANG { get; set; }
    }
}