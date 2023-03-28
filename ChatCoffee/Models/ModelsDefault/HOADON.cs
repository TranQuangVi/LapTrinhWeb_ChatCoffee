using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChatCoffee.Models.ModelsDefault
{
    [Table("HOADON")]
    public partial class HOADON
    {
        public HOADON()
        {
            CTDONHANGs = new HashSet<CTDONHANG>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MAHD { get; set; }

        public int? TONGDONGIA { get; set; }

        public int MAKH { get; set; }

        public int MAVT { get; set; }

        public int MATT { get; set; }

        //thêm mới
        public DateTime NGAYDAT { get; set; }
        public DateTime NGAYNHAN { get; set; }
/*        public bool XACNHAN { get; set; }
        public bool STATUS { get; set; }*/

        //////
        public virtual ICollection<CTDONHANG> CTDONHANGs { get; set; }

        public virtual KHACHHANG KHACHHANG { get; set; }

        public virtual THANHTOAN THANHTOAN { get; set; }

        public virtual VANCHUYEN VANCHUYEN { get; set; }
    }
}