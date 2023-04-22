﻿using HabitsApp.Client.Services;
using HabitsApp.Client.Services.Contracts;
using HabitsApp.Models.Dtos;
using HabitsApp.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Radzen;

namespace HabitsApp.Client.Components
{
    public partial class ActivityPickerCard
    {
        [Parameter] public IEnumerable<ActivityDto>? Activities { get; set; }
        public List<ActivityDto>? ActivitiesInSelectedCategory = new List<ActivityDto>();
        [Parameter] public IEnumerable<Category>? Categories { get; set; }
        [Parameter] public IEnumerable<GoalDto>? Goals { get; set; }
        [Parameter] public ActivityDto? SelectedActivity { get; set; }
        [Parameter] public Category? SelectedCategory { get; set; }
        //public Category? SelectedCatOfNewAct { get; set; }
        [Parameter] public EventCallback<ActivityDto> OnSelectedActivityChanged { get; set; } // For passing Data to Parent Component
        [Parameter] public EventCallback<Category> OnSelectedCategoryChanged { get; set; } // For passing Data to Parent Component

        [Inject] public ICalendarEntryService? CalendarEntryService { get; set; }
        [Inject] public IGoalService? GoalService { get; set; } // TODO: Move the PUT Goal logic to the Razor file
        public CalendarEntryDto? newCalendarEntry { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string? CardTitle = "Let's build a Habit";
        public string? InitialCardTitle = "Let's build a Habit";
        public const string PickedTitle = "Let's build a habit for ";
        public const string OngoingTitle = "Building a Habit for ";
        public const string DoneTitle = "Excellent! You built a Habit for ";
        public string GoalCompletedText = "Great! You completed a goal for today!";
        public string GoalCompletedTextInitial = "Great! You completed a goal for today!";
        public string GoalCompletedTextAgain = "Amazing! You did it again!";
        //public const string CreateActTitle = "Create new Activity";
        //public const string CreateCatTitle = "Create new Category";
        public string? ActTextBoxText { get; set; }
        public string? CatTextBoxText { get; set; }

        public bool StartBtnVisible = false;
        public bool StopBtnVisible = false;
        public bool CatDropDownVisible = true;
        public bool CatOfNewActDropDownVisible = false;
        public bool ActDropDownVisible = false;
        public bool ActTextBoxVisible = false;
        public bool CatTextBoxVisible = false;
        public bool GoalCompletedVisible = false;
        public bool BackAndRestartBtnsVisible = false;
        public bool StatsBtnVisible = false;

        public string? PathToActStats { get; set; }

        // TODO: Edit, Create and Delete Cat and Act should be moved to the Categories and Activities Pages
        //public bool AddActBtnVisible = false;
        //public bool AddCatBtnVisible = true;
        //public bool DeleteActBtnVisible = false;
        //public bool DeleteCatBtnVisible = false;
        //public bool CancelActBtnVisible = false;
        //public bool AcceptActBtnVisible = false;
        //public bool CancelCatBtnVisible = false;
        //public bool AcceptCatBtnVisible = false;

        ////public bool EditActMode = false;
        //public bool AddActMode = false;
        ////public bool EditCatMode = false;
        //public bool AddCatMode = false;

        public bool StartTextVisible = false;
        public bool EndTextVisible = false;
        public string? StartText { get; set; }
        public string? EndText { get; set; }

        private DisplayTimer? timerClock;

        public async void OnChangeCatDropdownOption(object value)
        {
            //if (!AddActMode)
            //{
                ActDropDownVisible = true;
                //AddActBtnVisible = true;
                //if (!DeleteCatBtnVisible) DeleteCatBtnVisible = true;
            //}

            //if (!AddCatMode)
            //{
                CardTitle = InitialCardTitle;
            //}

            if (Categories != null && value != null && Activities != null)
            {
                foreach (var category in Categories) 
                {
                    if (category.Name == value.ToString())
                    {
                        await OnSelectedCategoryChanged.InvokeAsync(category); // Pass Data (category) to Parent Component
                        
                        SelectedActivity = null; // Erase selected activity
                        await OnSelectedActivityChanged.InvokeAsync(SelectedActivity); 
                       
                        break;
                    }          
                }

                ActivitiesInSelectedCategory = new List<ActivityDto>();
                foreach (var activity in Activities)
                {
                    if (activity.CategoryName == SelectedCategory?.Name)
                    {
                        ActivitiesInSelectedCategory?.Add(activity); // Activites in ActDropDown are only the ones belonging to the selected Category
                    }
                }
            }
        }

        //public void OnChangeCatOfNewActDropdownOption(object value)
        //{
        //    if (Categories != null)
        //    {
        //        foreach (var category in Categories)
        //        {
        //            if (category.Name == value.ToString())
        //            {
        //                SelectedCatOfNewAct = category;
        //            }
        //        }
        //    }
        //}

        public async void OnChangeActDropdownOption(object value)
        {
            if (ActivitiesInSelectedCategory != null && value != null)
            {
                StartBtnVisible = true;

                foreach (var activity in ActivitiesInSelectedCategory)
                {
                    if (activity.Name == value.ToString())
                    {
                        await OnSelectedActivityChanged.InvokeAsync(activity); // Pass Data (activity) to Parent Component
                        PathToActStats = "/ActivityDetails/" + activity.Id;
                        break;
                    }
                }
                CardTitle = PickedTitle + SelectedActivity?.Name;

                if (!StatsBtnVisible) StatsBtnVisible = true;
                //if(!DeleteActBtnVisible) DeleteActBtnVisible = true;
            }
        }

        public async Task AddEntryToCalendar(CalendarEntryDto calendarEntryToAddDt)
        {
            try
            {
                if (CalendarEntryService != null)
                {
                    var calendarEntryDto = await CalendarEntryService.AddCalendarEntry(calendarEntryToAddDt); // POST Method
                }
            }
            catch (Exception)
            {
                // Log Exception
                throw;
            }
        }

        //public void StartTimer()
        //{
        //    timerClock.OnTimerStarted();
        //}
        public void OnClickStart()
        {
            StartBtnVisible = false;
            StopBtnVisible = true;
            ActDropDownVisible = false;
            CatDropDownVisible = false;
            StartTextVisible = true;
            StatsBtnVisible = false;
            //AddActBtnVisible = false;
            //AddCatBtnVisible = false;
            //DeleteActBtnVisible = false;
            //DeleteCatBtnVisible = false;

            SetStartDate();

            CardTitle = OngoingTitle + SelectedActivity?.Name;

            if (SelectedActivity != null && SelectedCategory != null)
            {
                newCalendarEntry = new CalendarEntryDto();

                newCalendarEntry.ActivityId = SelectedActivity.Id;
                newCalendarEntry.ActivityName = SelectedActivity.Name;
                newCalendarEntry.ActivityCategoryName = SelectedCategory.Name;
                newCalendarEntry.Date = StartTime;
                newCalendarEntry.Start = StartTime;
                //TODO: IMPLEMENT COMMENT -> newCalendarEntry.Comment = ;
            }

            //StartTimer();
        }

        public void StopTimer()
        {
            //timerClock.OnTimerStopped();
        }

        public async void MarkGoalAsCompleted()
        {
            if (Goals != null) 
            {
                Console.WriteLine($"Total minutes elapsed: {(newCalendarEntry.End - newCalendarEntry.Start).TotalMinutes}");
                foreach (var goalToUpdate in Goals)
                {
                     if (goalToUpdate.ActivityId == newCalendarEntry?.ActivityId && GoalService != null)
                    {

                        if (goalToUpdate.Date.Date == DateTime.Now.Date
                        && goalToUpdate.DurationMinutes <= (newCalendarEntry.End - newCalendarEntry.Start).TotalMinutes) 
                        {
                            // TODO: Move the PUT Goal logic to the Razor file

                            if (goalToUpdate.IsCompleted)
                            {
                                GoalCompletedText = GoalCompletedTextAgain;
                            }
                            else
                            {
                                GoalCompletedText = GoalCompletedTextInitial;
                                goalToUpdate.IsCompleted = true;
                            }
                            GoalCompletedVisible = true;
                            await GoalService.UpdateGoal(goalToUpdate);
                        }
                        Console.WriteLine($"Duration of goal: {goalToUpdate.DurationMinutes}");
                        break;     
                    }      
                }
            }   
        }
        public async void OnClickStop()
        {
            StopBtnVisible = false;
            BackAndRestartBtnsVisible = true;
            EndTextVisible = true;
            StatsBtnVisible = true;

            SetEndDate();
            if (newCalendarEntry != null)
            {
                newCalendarEntry.End = EndTime;
                MarkGoalAsCompleted();
                StopTimer();
                CardTitle = DoneTitle + SelectedActivity?.Name; // BUG: Not working  
                await AddEntryToCalendar(newCalendarEntry);
            }                 
        }

        public void OnClickBack()
        {
            ActDropDownVisible = true;
            CatDropDownVisible = true;
            BackAndRestartBtnsVisible = false;
            StartBtnVisible = true;
            StartTextVisible = false;
            EndTextVisible = false;
            GoalCompletedVisible = false;
            //AddActBtnVisible = true;
            //AddCatBtnVisible = true;
            //DeleteActBtnVisible = true;
            //DeleteCatBtnVisible = true;

            CardTitle = PickedTitle + SelectedActivity?.Name;
        }

        public void OnClickRestart()
        {
            BackAndRestartBtnsVisible = false;
            StopBtnVisible = true;
            EndTextVisible = false;
            GoalCompletedVisible = false;
            StatsBtnVisible = false;

            SetStartDate();

            CardTitle = OngoingTitle + SelectedActivity?.Name;
        }

        public void SetStartDate()
        {
            StartTime = DateTime.Now;
            StartText = "Activity " + SelectedActivity?.Name
                + " started on " + DateTime.Now.ToString("dd/MM/yy")
                + " at " + DateTime.Now.ToString("hh:mm:ss tt") + "\n";
        }

        public void SetEndDate()
        {
            EndTime = DateTime.Now;
            EndText = " and stopped at " + DateTime.Now.ToString("hh:mm:ss tt");
        }

        //public void OnClickAddActBtn()
        //{
        //    AddActMode = true;
        //    SelectedCatOfNewAct = SelectedCategory;

        //    AddActBtnVisible = false;
        //    AddCatBtnVisible = false;
        //    StatsBtnVisible = false;
        //    StartBtnVisible = false;
        //    DeleteActBtnVisible = false;
        //    DeleteCatBtnVisible = false;
        //    CatDropDownVisible = false;
        //    CatOfNewActDropDownVisible = true;
        //    ActDropDownVisible = false;
        //    ActTextBoxVisible = true;
        //    AcceptActBtnVisible = true;
        //    CancelActBtnVisible = true;

        //    CardTitle = CreateActTitle;
        //}

        //public void OnClickAddCatBtn()
        //{
        //    AddCatMode = true;

        //    ActDropDownVisible = false;
        //    AddActBtnVisible = false;
        //    AddCatBtnVisible = false;
        //    AcceptCatBtnVisible = true;
        //    CancelCatBtnVisible = true;
        //    DeleteActBtnVisible = false;
        //    DeleteCatBtnVisible = false;
        //    StatsBtnVisible = false;
        //    StartBtnVisible = false;
        //    CatTextBoxVisible = true;
        //    CatDropDownVisible = false;

        //    CardTitle = CreateCatTitle;
        //}

        public async void OnClickDeleteActBtn()
        {
            // TODO: Implement Confirm Dialog
            var confirmDelete = await  DialogService.Confirm(
                "Are you sure to DELETE the activity " + SelectedActivity?.Name + "?", 
                "Delete " + SelectedActivity?.Name, 
                new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" }
                );
            if (confirmDelete.Value)
            {
                // TODO: Implement Delete Activity from DB if selected "Yes" on Delete Dialog 
            }

            CardTitle = InitialCardTitle;
        }

        public void OnClickDeleteCatBtn()
        {
            DialogService.Confirm(
                "Are you sure to DELETE the Category " + SelectedCategory?.Name + " and its XXXX activities?", // Replace XXXX with count of activities in that category 
                "Delete " + SelectedCategory?.Name,
                new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" }
                );
            // TODO: Implement Delete Category from DB if selected "Yes" on Delete Dialog 
            CardTitle = InitialCardTitle;
        }

        //public void OnClickCancelActBtn()
        //{
        //    AddActMode = false;

        //    // TODO: Implement Confirmation Dialog 
        //    AddActBtnVisible = true;
        //    AddCatBtnVisible = true;
        //    ActDropDownVisible = true;
        //    ActTextBoxVisible = false;
        //    CatOfNewActDropDownVisible = false;
        //    CatDropDownVisible = true;
        //    AcceptActBtnVisible = false;
        //    CancelActBtnVisible = false;
        //    DeleteCatBtnVisible = true;

        //    if (SelectedActivity != null)
        //    {
        //        StartBtnVisible = true;
        //        StatsBtnVisible = true;
        //        DeleteActBtnVisible = true;

        //        CardTitle = PickedTitle + SelectedActivity?.Name;
        //    }
        //    else CardTitle = InitialCardTitle;

        //    //SelectedCatOfNewAct = SelectedCategory;
        //}

        //public void OnClickAcceptActBtn()
        //{
        //    AddActMode = false;

        //    // TODO: validate if this activity already exists or if string is empty
        //    // TODO: implement code to add new activity to DB
        //    AddActBtnVisible = true;
        //    AddCatBtnVisible = true;
        //    ActDropDownVisible = true;
        //    ActTextBoxVisible = false;
        //    CatOfNewActDropDownVisible = false;
        //    CatDropDownVisible = true;
        //    AcceptActBtnVisible = false;
        //    CancelActBtnVisible = false;
        //    DeleteCatBtnVisible = true;

        //    if (SelectedActivity != null)
        //    {
        //        StartBtnVisible = true;
        //        StatsBtnVisible = true;
        //        DeleteActBtnVisible = true;

        //        CardTitle = PickedTitle + SelectedActivity?.Name;
        //    }

        //    else CardTitle = InitialCardTitle;

        //    //SelectedCatOfNewAct = SelectedCategory;
        //}

        //public void OnClickCancelCatBtn()
        //{
        //    AddCatMode = false;

        //    AddCatBtnVisible = true;
        //    if (SelectedCategory != null)
        //    {
        //        ActDropDownVisible = true;
        //        AddActBtnVisible = true;
        //    }
        //    else CardTitle = InitialCardTitle;

        //    CatOfNewActDropDownVisible = false;
        //    CatDropDownVisible = true;
        //    CatTextBoxVisible = false;
        //    AcceptCatBtnVisible = false;
        //    CancelCatBtnVisible = false;

        //    if (SelectedCategory != null)
        //    {
        //        DeleteCatBtnVisible = true;

        //        if (SelectedActivity != null)
        //        {
        //            StartBtnVisible = true;
        //            StatsBtnVisible = true;
        //            DeleteActBtnVisible = true;

        //            CardTitle = PickedTitle + SelectedActivity?.Name;
        //        }
        //        else CardTitle = InitialCardTitle;
        //    }
        //}

        //public void OnClickAcceptCatBtn()
        //{
        //    AddCatMode = false;

        //    AddCatBtnVisible = true;
        //    if (SelectedCategory != null)
        //    {
        //        ActDropDownVisible = true;
        //        AddActBtnVisible = true;
        //    }
        //    else CardTitle = InitialCardTitle;

        //    CatOfNewActDropDownVisible = false;
        //    CatDropDownVisible = true;
        //    CatTextBoxVisible = false;
        //    AcceptCatBtnVisible = false;
        //    CancelCatBtnVisible = false;

        //    if (SelectedCategory != null)
        //    {
        //        DeleteCatBtnVisible = true;

        //        if (SelectedActivity != null)
        //        {
        //            StartBtnVisible = true;
        //            StatsBtnVisible = true;
        //            DeleteActBtnVisible = true;

        //            CardTitle = PickedTitle + SelectedActivity?.Name;
        //        }
        //        else CardTitle = InitialCardTitle;
        //    }
        //}

        //public void ToggleActivityDropDown()
        //{
        //    ActDropDownVisible = !ActDropDownVisible;
        //}

        //public void ToggleStartBtn()
        //{
        //    StartBtnVisible = !StartBtnVisible;
        //}

        //public void ToggleStopBtn()
        //{
        //    StopBtnVisible = !StopBtnVisible;
        //}

        //public void ToggleStartText()
        //{
        //    StartTextVisible = !StartTextVisible;
        //}

        //public void ToggleEndText()
        //{
        //    EndTextVisible = !EndTextVisible;
        //}

        //public void ToggleBackAndRestartBtns()
        //{
        //    BackAndRestartBtnsVisible = !BackAndRestartBtnsVisible;
        //}

        //public void ToggleStatBtn()
        //{
        //    StatsBtnVisible = !StatsBtnVisible;
        //}

        //public void ToggleAddActBtn()
        //{
        //    ToggleActivityDropDown();
        //    AddActBtnVisible = !AddActBtnVisible;
        //    ActTextBoxVisible = !ActTextBoxVisible; 
        //    ActTextBoxText = null; // Clears TextBox
        //    CancelActBtnVisible = !CancelActBtnVisible;
        //    AcceptActBtnVisible = !AcceptActBtnVisible;
        //}

        //public void ToggleAddCatBtn()
        //{
        //    AddCatBtnVisible = !AddCatBtnVisible;
        //    //TextBoxVisible = !TextBoxVisible;
        //    //TextBoxText = null; // Clears TextBox
        //    //CancelBtnVisible = !CancelBtnVisible;
        //    //AcceptBtnVisible = !AcceptBtnVisible;
        //}

        //public void ToggleCatDropDown()
        //{
        //    CatDropDownVisible = !CatDropDownVisible;
        //}

        //public void ToggleDeleteActBtn()
        //{
        //    DeleteActBtnVisible = !DeleteActBtnVisible;
        //}

        //public void ToggleDeleteCatBtn()
        //{
        //    //DeleteActBtnDisabled = !DeleteActBtnDisabled;
        //}
    }
}
