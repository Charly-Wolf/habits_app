using HabitsApp.Client;
using HabitsApp.Client.Services;
using HabitsApp.Client.Services.Contracts;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7065/") });

// Injections
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICalendarEntryService, CalendarEntryService>();
builder.Services.AddScoped<IGoalService, GoalService>();

// Radzen DialogService Component for Modals
builder.Services.AddScoped<DialogService>();

await builder.Build().RunAsync();
