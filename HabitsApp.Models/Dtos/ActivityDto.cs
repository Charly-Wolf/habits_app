namespace HabitsApp.Models.Dtos
{
    public class ActivityDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? Name { get; set; }
    }
}
