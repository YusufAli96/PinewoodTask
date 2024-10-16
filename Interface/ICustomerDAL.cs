using PinewoodTask.Models;

namespace PinewoodTask.Interface
{
    public interface ICustomerDAL
    {
        Task<List<Customer>> GetAll();
        Task<Customer?> Get(Guid id);
        Task<Guid> Create(Customer customer);
        Task<Guid> Update(Customer customer);
        Task<bool> SoftDelete(Customer customer);

    }
}
