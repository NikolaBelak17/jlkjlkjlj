namespace ClientService.API.Entities
{
    public class ScheduleItem
    {
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public string TrainingType { get; set; }
        public bool IsAvailable { get; set; }
        public string TrainerId { get; set; }
        public string TrainerName { get; set; }

    }
}
