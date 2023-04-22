using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitsApp.Shared.Entities
{
    public class CalendarEntry
    {
        public int Id { get; set; }
        [Required, NotNull] 
        public int ActivityId { get; set; }
        public Activity? Activity { get; set; }
        [Required, NotNull] 
        public DateTime Date { get; set; }
        [Required, NotNull]
        public DateTime Start { get; set; }
        [Required, NotNull]
        public DateTime End { get; set; }
        public string? Comment { get; set; }
    }
}
