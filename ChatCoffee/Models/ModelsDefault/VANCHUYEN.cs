using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChatCoffee.Models.ModelsDefault
{
    [Table("VANCHUYEN")]
    public partial class VANCHUYEN
    {
        public VANCHUYEN()
        {
            HOADONs = new HashSet<HOADON>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MAVT { get; set; }

        [StringLength(20)]
        public string TENVT { get; set; }

        public int? GIA { get; set; }

        public virtual ICollection<HOADON> HOADONs { get; set; }
    }

}