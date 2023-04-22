namespace HabitsApp.Models.Dtos
{
    public class CalendarEntryDto
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public string? ActivityName { get; set; }
        public string? ActivityCategoryName { get; set; }
        public DateTime Date { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string? Comment { get; set; }
    }
}
