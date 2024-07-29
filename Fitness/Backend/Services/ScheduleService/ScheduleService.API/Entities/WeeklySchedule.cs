namespace ScheduleService.API.Entities
{
    public class WeeklySchedule
    {
       
        public int WeekId { get; set; }
        public Dictionary<int, List<ScheduleItem>> DailySchedules { get; set; } = new Dictionary<int, List<ScheduleItem>>();

        public WeeklySchedule(int weekId)
        {
            WeekId = weekId;

            for(int i = 1; i<=7;++i)
            {
                DailySchedules[i] = InitializeDay();
            }
        }

        private static List<ScheduleItem> InitializeDay()
        {
            var timeslots = new List<ScheduleItem>();
            
        }

    }
}
