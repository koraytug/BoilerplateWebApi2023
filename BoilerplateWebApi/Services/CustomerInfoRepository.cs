using BoilerplateWebApi.DbContexts;
using BoilerplateWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoilerplateWebApi.Services
{
    public class CustomerInfoRepository : ICustomerInfoRepository
    {
        private readonly CustomerInfoContext context;

        public CustomerInfoRepository(CustomerInfoContext context)
        {
            this.context = context ?? throw new System.ArgumentNullException(nameof(context)); ;
        }
        public async Task<Customer?> GetCustomerAsync(int customerId, bool includeOperation)
        {
            if(includeOperation)
            {
                return await context.Customers.Include(c=> c.CustomerOperation)
                    .Where(c=> c.Id == customerId).FirstOrDefaultAsync();
            }

            return await context.Customers
                    .Where(c => c.Id == customerId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await context.Customers.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<CustomerOperation?> GetOperationForCustomerAsync(int customerId, int operationId)
        {
            return await context.CustomerOperations
                   .Where(c => c.CustomerId == customerId && c.Id == operationId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CustomerOperation>> GetOperationsForCustomerAsync(int customerId)
        {
            return await context.CustomerOperations
                  .Where(c => c.CustomerId == customerId).ToListAsync();
        }
    }
}
