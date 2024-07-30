namespace ScheduleService.API.Entities
{
    public abstract class Schedule
    {
        public string Id { get; set; }
        public List<WeeklySchedule> WeeklySchedules { get; set; } = new List<WeeklySchedule>();
        public Schedule(string id)
        {
            var start_week = 1;
            var end_week = 5;
            Id = id;
            for (int i = start_week; i <= start_week; ++i)
            {
                WeeklySchedules.Add(new WeeklySchedule { });
            }
        }
    }
}
