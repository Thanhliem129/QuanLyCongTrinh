using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace WebApplication1.Model
{
    public class connection

    {
        private readonly IConfiguration _configuration;

        public connection(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
