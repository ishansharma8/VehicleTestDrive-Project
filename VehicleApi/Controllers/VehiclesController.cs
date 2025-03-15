using Microsoft.AspNetCore.Mvc;
using VehicleApi.Interface;
using VehicleApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VehicleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        //inject the interface in the construtor of the controller
        private IVehicle _vehicleService;
        public VehiclesController(IVehicle vehicleService)
        {
            _vehicleService=vehicleService;
            //with _vehicleService ,we will pick all methods written in IVehicle interface
        }
        // GET: api/<VehiclesController>

        [HttpGet]
        public async Task<IEnumerable<Vehicle>>GetVehicles()
        {
            return await _vehicleService.GetAllVehicles();
        }



        // GET api/<VehiclesController>/5
        [HttpGet("{id}")]
        public async Task<Vehicle> GetParticularVehicle(int id)
        {
             return await _vehicleService.GetVehicleById(id);
        }

        // POST api/<VehiclesController>
        [HttpPost]
        public async Task Post([FromBody] Vehicle vehicle)
        {
            await _vehicleService.AddVehicle(vehicle);
        }

        // PUT api/<VehiclesController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Vehicle vehicle)
        {
            await _vehicleService.UpdateVehicle(id, vehicle);
        }

        // DELETE api/<VehiclesController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await (_vehicleService.DeleteVehicle(id));
        }
    }
}
