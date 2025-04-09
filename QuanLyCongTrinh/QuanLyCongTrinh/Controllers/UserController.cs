using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Data;
using System.Data.SqlClient;
using QuanLyCongTrinh.Model;
using Newtonsoft.Json;
using System.Linq;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SqlConnection _connection;
        private readonly AppDbContext _context;

        public UserController(connection connection, AppDbContext context)
        {
            _connection = connection.GetConnection();
            _context = context;
        }

        [HttpGet]
        [Route("/GetListUser")]
        public dynamic GetListUser(ThamSo thamso)
        {
            var listUser = _connection.Query<dynamic>(
                "sp_GetListUser", new
                {
                    name = thamso.name
                }, commandType: CommandType.StoredProcedure 
            ).ToList();
            return Ok(listUser); // trả về JSON luôn

        }

        [HttpPost]
        [Route("/PostUser")]
        public async Task<IActionResult> PostUser(User user)
        {
            try
            {
                User newuser = new User
                {
                    ten = user.ten,
                    matkhau = user.matkhau,
                    ChucVu = user.ChucVu,
                    PhongDoi = user.PhongDoi,
                    taikhoan = user.taikhoan,
                    quyen = user.quyen
                };

                await _context.NGUOIDUNG.AddAsync(newuser);
                await _context.SaveChangesAsync();

                return Ok("Thêm thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost]
        [Route("/DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _context.NGUOIDUNG.Where(x => x.IDND == id).FirstOrDefaultAsync();
                if (user != null) {
                    _context.NGUOIDUNG.Remove(user);
                    await _context.SaveChangesAsync();
                    return Ok("Xóa thành công");
                }
                else
                {
                    return BadRequest("Không tìm thấy");
                }  
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost]
        [Route("/UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(User newuser, int id)
        {
            try
            {
                var user = await _context.NGUOIDUNG.Where(x => x.IDND == id).FirstOrDefaultAsync();
                if (user != null)
                {
                    user.ten = newuser.ten;
                    user.matkhau = newuser.matkhau;
                    user.ChucVu = newuser.ChucVu;
                    user.PhongDoi = newuser.PhongDoi;
                    user.taikhoan = newuser.taikhoan;
                    user.quyen = newuser.quyen;

                    await _context.SaveChangesAsync();
                    return Ok("Update thành công");
                }
                else
                {
                    return BadRequest("Không tìm thấy");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
