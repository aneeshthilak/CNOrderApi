using CNOrderApi.Data;
using CNOrderApi.Interfaces;

namespace CNOrderApi.Repositories
{
    public class DapperRepository : IDapperRepository
    {

        private readonly DapperContext _dbContext;

        public DapperRepository(DapperContext databaseContext)
        {
            _dbContext = databaseContext;
        }



    }
}
