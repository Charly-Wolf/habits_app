namespace HabitsApp.Models.Dtos
{
    public class GoalDto
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public string? ActivityName { get; set; }
        public DateTime Date { get; set; }
        public int DurationMinutes { get; set; }
        public bool IsCompleted { get; set; }
    }
}
