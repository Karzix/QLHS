using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class HocSinh
    {
        public Guid Id { get; set; }
        public string MaSV { get; set; }
        public string HoVaTen { get; set; }

        [ForeignKey("LopHocId")]
        public LopHoc? LopHoc { get; set; }
        [ForeignKey("LopHoc")]
        public Guid? LopHocId { get; set; }
    }
}
