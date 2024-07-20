using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using CNOrderApi.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace CNOrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private IConfiguration _configuration;

        public CustomerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string getCustomerSP = "[dbo].[GetCustomers]";

        [HttpGet("GetCustomer")]
        public IEnumerable<Customer> GetCustomer(int customerId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return connection.Query<Customer>(getCustomerSP, new { CustomerID = customerId },
                                    commandType: CommandType.StoredProcedure);
            }
        }

        [HttpGet("GetCustomer1")]
        public async Task<Customer> GetCustomer1(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var procedure = "GetCustomers";
                var parameters = new DynamicParameters();
                parameters.Add("@CustomerID", id);
                var results = await connection.QueryFirstAsync<Customer>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return results;
            }
        }
    }
}
