using Microsoft.EntityFrameworkCore;
using VehicleApi.Data;
using VehicleApi.Interface;
using VehicleApi.Models;

namespace VehicleApi.Services
{
    public class VehicleService:IVehicle
    {
        private ApiDbContext _dbcontext;
        public VehicleService()
        {
            _dbcontext = new ApiDbContext();
        }
        public async Task<List<Vehicle>> GetAllVehicles()
        {
            var vehicles = await _dbcontext.Vehicles.ToListAsync();
            return vehicles;
        }

        public async Task<Vehicle> GetVehicleById(int id)
        {
            var vehicle = await _dbcontext.Vehicles.FindAsync(id);
            return vehicle;
        } 

        public async Task AddVehicle(Vehicle vehicle)
        {
            await _dbcontext.Vehicles.AddAsync(vehicle);//add the data inside the database
            await _dbcontext.SaveChangesAsync(); //For insert ,update & delete ->Save changes
        }

        public async Task UpdateVehicle(int id, Vehicle vehicle)
        {
            var vehicleToUpdate =await _dbcontext.Vehicles.FindAsync(id);
            vehicleToUpdate.Name = vehicle.Name;
            vehicleToUpdate.ImageUrl=vehicle.ImageUrl;
            vehicleToUpdate.Height = vehicle.Height;
            vehicleToUpdate.Width = vehicle.Width;
            vehicleToUpdate.MaxSpeed = vehicle.MaxSpeed;
            vehicleToUpdate.Price=vehicle.Price;
            vehicleToUpdate.Displacement=vehicle.Displacement;

            await _dbcontext.SaveChangesAsync();
            
        }

        public async Task DeleteVehicle(int id)
        {
            var vehicleToDelete = await _dbcontext.Vehicles.FindAsync(id);
             _dbcontext.Vehicles.Remove(vehicleToDelete); //No remove async method
            _dbcontext.SaveChangesAsync();

        }
    }
}
