using VehicleApi.Models;

namespace VehicleApi.Interface
{
    public interface IVehicle
    {
        //get all ,get one by id ,post 1 by id ,update one by id .,delelte 1 by id
        Task<List<Vehicle>> GetAllVehicles();  //returns list of vehicles
        Task<Vehicle> GetVehicleById(int id);
        Task AddVehicle(Vehicle vehicle);
        Task UpdateVehicle(int id, Vehicle vehicle);
        Task DeleteVehicle(int id);  //if not return anything then task
    }
}
