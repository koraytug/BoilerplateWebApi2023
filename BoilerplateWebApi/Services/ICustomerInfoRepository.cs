using BoilerplateWebApi.Entities;

namespace BoilerplateWebApi.Services
{
    public interface ICustomerInfoRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<IEnumerable<Customer>> GetCustomersAsync(string? name, string? searchQuery);
        Task<Customer?> GetCustomerAsync(int customerId, bool includeOperation);
        Task<bool> CustomerExistsAsync(int customerId);
        Task<IEnumerable<CustomerOperation>> GetOperationsForCustomerAsync(int customerId);
        Task<CustomerOperation?> GetOperationForCustomerAsync(int customerId, int operationId);
        Task AddCustomerOperationForCustomerAsync(int customerId, CustomerOperation operation);
        void DeleteCustomerOperation(CustomerOperation operation);
        Task<bool> SaveChangesAsync();
    }
}
