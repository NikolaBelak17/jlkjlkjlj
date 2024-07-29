using ClientService.API.Entities;
using MongoDB.Driver;

namespace ClientService.API.Data
{
    public class ClientContext: IClientContext
    {
        public ClientContext()
        {
            
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("FitDB");

            Clients = database.GetCollection<Client>("Clients");
            Schedules = database.GetCollection<ClientSchedule>("ClientSchedules");
        }

        public IMongoCollection<Client> Clients { get; }
        public IMongoCollection<ClientSchedule> Schedules { get; }
    }
}
