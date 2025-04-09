using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Model;
using Newtonsoft.Json;
using System.Linq;
using Dapper;

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
        public dynamic GetListUser()
        {
            var listUser = _connection.Query<User>( 
                "sp_GetListUser",
                commandType: CommandType.StoredProcedure 
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
                    quyen = user.quyen,
                    IDND = user.IDND
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
    }
}
