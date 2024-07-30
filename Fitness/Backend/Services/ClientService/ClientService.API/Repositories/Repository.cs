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

        public async Task<IEnumerable<ClientSchedule>> GetClientsSchedules()
        {
            return await _clientContext.Schedules.Find(p => true).ToListAsync();
        }
        public async Task<Client> GetClientById(string id)
        {
            return await _clientContext.Clients.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task<ClientSchedule> GetClientScheduleByClientId(string id)
        {
            return await _clientContext.Schedules.Find(s=>s.ClientId==id).FirstOrDefaultAsync();
        }
        public async Task<WeeklySchedule> GetClientWeekSchedule(string clientId, int weekId)
        {
            var clientSchedule = await GetClientScheduleByClientId(clientId);
            return clientSchedule?.WeeklySchedules.FirstOrDefault(ws => ws.WeekId == weekId);
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

            var clientSchedule = new ClientSchedule(client.Id);
            await _clientContext.Schedules.InsertOneAsync(clientSchedule);
        }
        public async Task<bool> DeleteClient(string id)
        {
            var resultClient = await _clientContext.Clients.DeleteOneAsync(p => p.Id == id);
            var resultSchedule = await _clientContext.Schedules.DeleteOneAsync(s=> s.ClientId == id);

            return resultClient.IsAcknowledged && resultClient.DeletedCount > 0 
                && resultSchedule.IsAcknowledged && resultClient.DeletedCount>0;
        }
        public async Task DeleteAllClients()
        {
            await _clientContext.Clients.DeleteManyAsync(p => true);
        }
        public async Task<bool> UpdateClient(Client client)
        {
            var result = await _clientContext.Clients.ReplaceOneAsync(p => p.Id == client.Id, client);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
        public async Task<bool> UpdateClientSchedule(ClientSchedule clientSchedule)
        {
            var result = await _clientContext.Schedules.ReplaceOneAsync(cs => cs.ClientId == clientSchedule.ClientId, clientSchedule, new ReplaceOptions { IsUpsert = true });
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
        public async Task<bool> TrainingBooking(string clientId, string trainerId, string trainingType,
                                                int weekId, string dayId, int startTime, bool isBooking)
        {
            var clientSchedule = await GetClientScheduleByClientId(clientId);
            if (clientSchedule == null)
            {
                return false;
            }

            var weeklySchedule = clientSchedule.WeeklySchedules.FirstOrDefault(ws => ws.WeekId == weekId);
            if (weeklySchedule == null)
            {
                return false;
            }

            var daySchedule = weeklySchedule.DailySchedules.ContainsKey(dayId) ? weeklySchedule.DailySchedules[dayId] : null;
            if (daySchedule == null)
            {
                return false;
            }

            var scheduleItem = daySchedule.FirstOrDefault(si => si.StartTime == startTime);
            if (scheduleItem == null)
            {
                return false;
            }

            if(isBooking)
            {
                if(!scheduleItem.IsAvailable)
                {
                    return false;
                }

                scheduleItem.IsAvailable = false;
                scheduleItem.TrainerId = trainerId;
                scheduleItem.TrainingType = trainingType;
            }
            else
            {
                scheduleItem.IsAvailable = true;
                scheduleItem.TrainerId = null;
                scheduleItem.TrainingType = null;
            }

            return await UpdateClientSchedule(clientSchedule);
        }
    }
}
