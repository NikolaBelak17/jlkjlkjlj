namespace ClientService.API.Entities
{
    public class WeeklySchedule
    {
        public int WeekId { get; set; }
        public Dictionary<string, List<ScheduleItem>> DailySchedules { get; set; } = new Dictionary<string, List<ScheduleItem>>();

        public WeeklySchedule(int weekId)
        {
            WeekId = weekId;

            for (int i = 1; i <= 7; ++i)
            {
                
                DailySchedules[i.ToString()] = InitializeDay();
            }
        }

        private static List<ScheduleItem> InitializeDay()
        {
            var timeslots = new List<ScheduleItem>();
            for(int i = 8; i < 20; ++i)
            {
                timeslots.Add(new ScheduleItem { StartTime=i,EndTime=i+1,IsAvailable=true});
            }
            return timeslots;
        }

    }
}

