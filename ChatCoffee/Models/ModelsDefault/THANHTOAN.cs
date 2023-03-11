using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChatCoffee.Models.ModelsDefault
{
    [Table("THANHTOAN")]
    public partial class THANHTOAN
    {
        public THANHTOAN()
        {
            HOADONs = new HashSet<HOADON>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MATT { get; set; }

        [StringLength(20)]
        public string PHUONGTHUC { get; set; }

        public virtual ICollection<HOADON> HOADONs { get; set; }
    }
}