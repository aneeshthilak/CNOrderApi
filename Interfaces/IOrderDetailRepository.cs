using CNOrderApi.Models;

namespace CNOrderApi.Interfaces
{
    public interface IOrderDetailRepository
    {
        public Task<IEnumerable<Customer>> GetCustomer();
    }
}
