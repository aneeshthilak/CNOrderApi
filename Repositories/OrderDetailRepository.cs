using CNOrderApi.Data;
using CNOrderApi.Interfaces;
using CNOrderApi.Models;
//using CNOrderApi.CustomModels;
using Dapper;
using static CNOrderApi.Repositories.OrderDetailRepository;
using System.Data;

namespace CNOrderApi.Repositories
{

    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly DapperContext _context;
        public OrderDetailRepository(DapperContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method retuns order and order details for customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<OrderItem>> GetOrderDetail(int customerId)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = @"SELECT Top 1 ord.OrderId, ord.CustomerId, ord.CONTAINSGIFT, ord.OrderDate, ord.DeliveryExpected, 
                    orddtl.ORDERID, orddtl.ORDERITEMID, orddtl.PRODUCTID, orddtl.Quantity, orddtl.Price 
                    From [dbo].[ORDERITEMS] orddtl Inner Join [dbo].[ORDERS] ord On orddtl.OrderId = ord.OrderId
                    Where ord.CustomerId =  @customerId Order by OrderDate Desc";

                var parameters = new DynamicParameters();
                parameters.Add("@customerId", customerId);

                var orderItems = await connection.QueryAsync<Order, OrderItem, OrderItem>(sql, (order, orderItem) =>
                {
                    orderItem.Order = order;
                    return orderItem;
                }, parameters, null, true, splitOn: "OrderId", null, null);

                return orderItems;
            }

        }

        /// <summary>
        /// Method retuns order and order details along with customer details is in progress
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<OrderItem>> GetCustomerOrder(int customerId)
        {

            using (var connection = _context.CreateConnection())
            {

                var sql = @"SELECT Top 1 ord.OrderId, ord.CustomerId, ord.CONTAINSGIFT, ord.OrderDate, ord.DeliveryExpected, 
                    orddtl.ORDERID, orddtl.ORDERITEMID, orddtl.PRODUCTID, orddtl.Quantity, orddtl.Price,
                    c.CustomerId, c.FIRSTNAME, c.LASTNAME, c.EMAIL, c.HOUSENO, c.STREET, c.TOWN, c.POSTCODE
                    From [dbo].[ORDERITEMS] orddtl Inner Join [dbo].[ORDERS] ord On orddtl.OrderId = ord.OrderId
                    Inner Join [dbo].[CUSTOMERS] c on ord.CustomerId = c.CustomerId
                    Where ord.CustomerId =  @customerId Order by OrderDate Desc";

                var parameters = new DynamicParameters();
                parameters.Add("@customerId", customerId);

                var orderItems = await connection.QueryAsync<Order, OrderItem, OrderItem>(sql, (order, orderItem) =>
                 {
                     orderItem.Order = order;
                     return orderItem;
                 }, parameters, null, true, splitOn: "OrderId", null, null);


                return orderItems;
            }

        }

        /// <summary>
        /// A sample method for getting all customers
        /// </summary>
        /// <returns></returns>
        public async Task<List<Customer>> GetCustomer()
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
