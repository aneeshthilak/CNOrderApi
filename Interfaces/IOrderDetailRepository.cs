﻿//using CNOrderApi.CustomModels;
using CNOrderApi.Models;

namespace CNOrderApi.Interfaces
{
    public interface IOrderDetailRepository
    {
        public Task<IEnumerable<OrderItem>> GetOrderDetail(int customerId);
        public Task<IEnumerable<OrderItem>> GetCustomerOrder(int customerId);

        public Task<List<Customer>> GetCustomer();
    }
}
