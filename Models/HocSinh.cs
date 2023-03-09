using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace WebApplication1.Models
{
    public class HocSinh
    {
        public Guid Id { get; set; }
        [Display(Name = "Mã số sinh viên")]
        public string MaSV { get; set; }
        [Display(Name ="Họ và tên")]
        public string HoVaTen { get; set; }

        [ForeignKey("LopHocId")]
        [Display(Name ="Lớp học")]
        public LopHoc? LopHoc { get; set; }
        [ForeignKey("LopHoc")]
        public Guid? LopHocId { get; set; }
    }
}
