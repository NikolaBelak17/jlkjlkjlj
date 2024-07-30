using ClientService.API.Entities;
using MongoDB.Driver;

namespace ClientService.API.Data
{
    public interface IClientContext
    {
        IMongoCollection<Client> Clients { get; }
        IMongoCollection<ClientSchedule> Schedules { get; }
    }
}
