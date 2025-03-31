using Azure.Messaging.ServiceBus;
using CustomersApi.Data;
using CustomersApi.Interfaces;
using CustomersApi.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

            //Adding code for Azure Messaging Bus
            //For conn string :azure portal->service bus->shared access policies->root manage shared access key->Primary connection string
            string connectionString = "Endpoint=...";
            string queueName = "azure queue";

            await using ServiceBusClient client = new(connectionString); // since ServiceBusClient implements IAsyncDisposable we create it with "await using"

            //This code is perfect to use but it will only send a string to the azure service bus. But we want to send the customer object. so we will first serialise the 
            //customer object to json and then we will send the customer object.

            var objectAsText = JsonConvert.SerializeObject(customer); //The serialise object will convert Customer Object into Json string

            ServiceBusSender sender = client.CreateSender(queueName);// create the sender

            ServiceBusMessage message = new ServiceBusMessage(objectAsText); // create a message that we can send. UTF-8 encoding is used when providing a string. shorthand  ServiceBusMessage message = new("Hello world!")

            // send the message
            await sender.SendMessageAsync(message);

        }
    }
}
