//using Microsoft.AspNetCore.Components;
//using System.Threading

//namespace HabitsApp.Client.Components
//{
//    public partial class DisplayTimer
//    {
//        private DateTime currentTime = DateTime.Now;
//        private Timer? timer;
//        private bool isTimerRunning = false;
//        private bool hasTimerStarted = false;

//        [Parameter] public Action OnTimerStarted { get; set; }
//        [Parameter] public Action OnTimerStopped { get; set; }

//        protected override void OnInitialized()
//        {
//            base.OnInitialized();
//            timer = new Timer(OnTimerElapsed, null, Timeout.Infinite, 1000);
//        }

//        private void OnTimerElapsed(object state)
//        {
//            if (isTimerRunning)
//            {
//                currentTime = DateTime.Now;
//                InvokeAsync(() => StateHasChanged());
//            }
//        }

//        public void OnTimerStarted()
//        {
//            isTimerRunning = true;
//            if (!hasTimerStarted)
//            {
//                timer.Change(0, 1000);
//                hasTimerStarted = true;
//            }
//            else
//            {
//                timer.Change(0, 1000);
//            }
//        }

//        public void OnTimerStopped()
//        {
//            timer.Change(Timeout.Infinite, Timeout.Infinite);
//            isTimerRunning = false;
//        }

//    }
//}
