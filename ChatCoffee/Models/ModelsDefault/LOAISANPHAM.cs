using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChatCoffee.Models.ModelsDefault
{
    [Table("LOAISANPHAM")]
    public partial class LOAISANPHAM
    {
        public LOAISANPHAM()
        {
            COFFEEs = new HashSet<COFFEE>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MALOAI { get; set; }

        [StringLength(20)]
        public string TENLOAI { get; set; }

        public virtual ICollection<COFFEE> COFFEEs { get; set; }
    }
}