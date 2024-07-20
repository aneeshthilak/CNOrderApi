using System.Data.SqlClient;
using System.Data;

namespace CNOrderApi.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString(_configuration.GetConnectionString("DefaultConnection"));
        }
        //public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
        public IDbConnection CreateConnection() => new SqlConnection(_configuration.GetConnectionString(_configuration.GetConnectionString("DefaultConnection")));
    }
}
