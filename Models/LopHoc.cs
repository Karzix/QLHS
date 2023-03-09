using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class LopHoc
    {
        public Guid Id { get; set; }
        [Display(Name = "Mã lớp học")]
        public string MaLopHoc { get; set; }
        [Display(Name ="Tên lớp học")]
        public string TenLop { get; set; }
    }
}
