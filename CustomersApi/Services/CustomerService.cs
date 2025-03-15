using CustomersApi.Data;
using CustomersApi.Interfaces;
using CustomersApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomersApi.Services
{
    public class CustomerService:ICustomer
    {
        private ApiDbContext _dbcontext;
        public CustomerService()
        {
            _dbcontext = new ApiDbContext();
        }
        public async Task AddCustomer(Customer customer) {
            var vehiclInDb =await _dbcontext.Vehicles.FirstOrDefaultAsync(v => v.Id == customer.VehicleId);
            if(vehiclInDb == null)
            {
                await _dbcontext.Vehicles.AddAsync(customer.Vehicle);
                await _dbcontext.SaveChangesAsync();
            }
            customer.Vehicle = null;
            await _dbcontext.AddAsync(customer);
            await _dbcontext.SaveChangesAsync();

        }
    }
}
