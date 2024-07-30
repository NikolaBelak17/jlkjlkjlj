using ClientService.API.Entities;

namespace ClientService.API.Repositories
{
    public interface IRepository
    {
        Task<IEnumerable<Client>> GetClients();
        Task<IEnumerable<ClientSchedule>> GetClientsSchedules();

        Task<Client> GetClientById(string id);
        Task<ClientSchedule> GetClientScheduleByClientId(string id);
        Task<WeeklySchedule> GetClientWeekSchedule(string clientId, int weekId);
        Task<IEnumerable<Client>> GetClientsByName(string name);
        Task<IEnumerable<Client>> GetClientsBySurname(string surname);
        Task<IEnumerable<Client>> GetClientsByTrainerId(string trainerId);
        Task CreateClient(Client client);
        Task<bool> UpdateClient(Client client);
        Task<bool> UpdateClientSchedule(ClientSchedule clientSchedule);
        Task<bool> DeleteClient(string id);
        Task<bool> TrainingBooking(string clientId, string trainerId, string trainingType, int weekId, string day, int startTime, bool isBooking);
        Task DeleteAllClients();
    }
}
