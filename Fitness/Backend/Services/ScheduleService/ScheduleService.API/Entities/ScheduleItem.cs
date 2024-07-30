namespace ScheduleService.API.Entities
{
    public class ScheduleItem
    {
        public string StartTime { get; set; }
        public string EndTime{ get; set; }
        public string TrainingType { get; set; }
        public bool IsOccupied { get; set; }
        public string OccupiedById { get; set; }
    }
}
