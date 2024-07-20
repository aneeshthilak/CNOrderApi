using CNOrderApi.Data;
using CNOrderApi.Interfaces;
using CNOrderApi.Models;
using Dapper;
using static CNOrderApi.Repositories.OrderDetailRepository;

namespace CNOrderApi.Repositories
{

    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly DapperContext _context;
        public OrderDetailRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetCustomer()
        {
            var query = "SELECT * FROM Customers";
            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Customer>(query);
                return companies.ToList();
            }
        }




    }
}
