using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChatCoffee.Models.ModelsDefault
{
    [Table("THUONGHIEU")]
    public partial class THUONGHIEU
    {
        public THUONGHIEU()
        {
            COFFEEs = new HashSet<COFFEE>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MATH { get; set; }

        [StringLength(100)]
        public string TENTH { get; set; }
        public string ANH { get; set; }
        public virtual ICollection<COFFEE> COFFEEs { get; set; }
    }
}