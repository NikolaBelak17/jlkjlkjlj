using ClientService.API.Data;
using ClientService.API.Entities;
using MongoDB.Driver;

namespace ClientService.API.Repositories
{
    public class Repository: IRepository
    {
        private readonly IClientContext _clientContext;

        public Repository(IClientContext clientContext)
        {
            _clientContext = clientContext ?? throw new ArgumentNullException(nameof(clientContext));
        }
        public async Task<IEnumerable<Client>> GetClients()
        {
            return await _clientContext.Clients.Find(p => true).ToListAsync();
        }
        public async Task<Client> GetClientById(string id)
        {
            return await _clientContext.Clients.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Client>> GetClientsByName(string name)
        {
            return await _clientContext.Clients.Find(p => p.Name == name).ToListAsync();
        }

        public async Task<IEnumerable<Client>> GetClientsBySurname(string surname)
        {
            return await _clientContext.Clients.Find(p => p.Surname == surname).ToListAsync();
        }

        public async Task<IEnumerable<Client>> GetClientsByTrainerId(string trainerId)
        {
            return await _clientContext.Clients.Find(p => p.TrainerId == trainerId).ToListAsync();
        }
        public async Task CreateClient(Client client)
        {
            await _clientContext.Clients.InsertOneAsync(client);
        }
        public async Task<bool> DeleteClient(string id)
        {
            var result = await _clientContext.Clients.DeleteOneAsync(p => p.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
        public async Task<bool> UpdateClient(Client client)
        {
            var result = await _clientContext.Clients.ReplaceOneAsync(p => p.Id == client.Id, client);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}
