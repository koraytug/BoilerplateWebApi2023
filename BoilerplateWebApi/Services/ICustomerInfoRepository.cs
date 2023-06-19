using BoilerplateWebApi.Entities;

namespace BoilerplateWebApi.Services
{
    public interface ICustomerInfoRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer?> GetCustomerAsync(int customerId, bool includeOperation);
        Task<IEnumerable<CustomerOperation>> GetOperationsForCustomerAsync(int customerId);
        Task<CustomerOperation?> GetOperationForCustomerAsync(int customerId, int operationId);
    }
}
