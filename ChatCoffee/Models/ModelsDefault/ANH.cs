using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChatCoffee.Models.ModelsDefault
{
    [Table("ANH")]
    public partial class ANH
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MAANH { get; set; }

        [StringLength(100)]
        public string LINKANH { get; set; }

        public int MACF { get; set; }

        public virtual COFFEE COFFEE { get; set; }
    }
}