using Microsoft.EntityFrameworkCore;
using PinewoodTask.Constants;
using PinewoodTask.Data;
using PinewoodTask.Interface;
using PinewoodTask.Models;
using System.Text.RegularExpressions;

namespace PinewoodTask.DAL
{
    public class CustomerDAL : ICustomerDAL
    {
        private readonly ApplicationDbContext _context;

        public CustomerDAL(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAll()
        {

            List<Customer> customer = [];

            try
            {
                customer = await _context.Customers.OrderBy(c => c.AuditAction).ToListAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return customer;
        }

        public async Task<Customer?> Get(Guid id)
        {
            Customer? customer;

            try
            {
                customer = await _context.Customers.FirstOrDefaultAsync(g => g.CustomerID == id);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return customer;
        }

        public async Task<Guid> Create(Customer customer)
        {
            try
            {
                customer.CreatedDateTime = DateTime.Now;
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return customer.CustomerID;
        }

        public async Task<Guid> Update(Customer customer)
        {
            Guid customerID = Guid.Empty;

            try
            {
                var dbCustomer = await Get(customer.CustomerID);

                if (dbCustomer != null)
                { 
                    dbCustomer.FirstName = customer.FirstName;
                    dbCustomer.LastName = customer.LastName;
                    dbCustomer.MiddleName = customer.MiddleName;
                    dbCustomer.PhoneNumber = customer.PhoneNumber;
                    dbCustomer.EmailAddress = customer.EmailAddress;
                    dbCustomer.OptInMarketing = customer.OptInMarketing;
                    dbCustomer.AuditAction = AuditActionTypes.Update;

                    await _context.SaveChangesAsync();
                    customerID = dbCustomer.CustomerID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return customerID;
        }

        public async Task<bool> SoftDelete(Customer customer)
        {
            bool deleted = false;

            try
            {
                var dbCustomer = await Get(customer.CustomerID);

                if (dbCustomer == null)
                    return deleted;

                dbCustomer.AuditAction = AuditActionTypes.Delete;

                await _context.SaveChangesAsync();
                deleted = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return deleted;
        }
    }
}
