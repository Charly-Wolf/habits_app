using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace HabitsApp.Shared.Entities
{
    public class Goal
    {
        public int Id { get; set; }
        [Required, NotNull] public int ActivityId { get; set; }
        public Activity? Activity { get; set; }
        [Required, NotNull] public DateTime? Date { get; set; }
        [Required, NotNull] public int DurationMinutes { get; set; }
        public bool IsCompleted { get; set; }
    }
}
