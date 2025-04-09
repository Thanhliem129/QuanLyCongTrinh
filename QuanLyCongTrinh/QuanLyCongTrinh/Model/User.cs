using System.ComponentModel.DataAnnotations;

namespace QuanLyCongTrinh.Model
{
    public class User
    {
        [Key]
        public int IDND { get; set; }
        public string? taikhoan { get; set; }
        public string? PhongDoi { get; set; }
        public string? ChucVu { get; set; }
        public string? ten { get; set; }
        public string? quyen { get; set; }
        public string? matkhau { get; set; }


    }
}
