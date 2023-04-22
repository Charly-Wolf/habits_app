using HabitsApp.Client.Services.Contracts;
using HabitsApp.Models.Dtos;
using Microsoft.AspNetCore.Components;

namespace HabitsApp.Client.Components
{
    public partial class ActivityStats
    {
        [Parameter] public IEnumerable<CalendarEntryDto>? ActivityCalendarEntries { get; set; }
    }
}
