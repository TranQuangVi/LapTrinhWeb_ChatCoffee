using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChatCoffee.Models.ModelsDefault
{
    public class DIACHI
    {
        public DIACHI()
        {
            KHACHHANGs = new HashSet<KHACHHANG>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MADC { get; set; }

        [StringLength(20)]
        public string TENDC { get; set; }

        public virtual ICollection<KHACHHANG> KHACHHANGs { get; set; }
    }
}