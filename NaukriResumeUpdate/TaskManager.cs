using System.Timers;

namespace NaukriResumeUpdate
{
    public class TaskManager
    {
        private readonly Timer _timer;

        public TaskManager()
        {
            _timer = new Timer(1000 * 60 * 60 * 10) { AutoReset = true };

            _timer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            BusinessLogic.NaukriJobScrapper naukriJobScrapper = new();
            for (int i = 0; i < 2; i++)
            {
                naukriJobScrapper.ScrapData(i);
            }
            naukriJobScrapper.CloseBrowser();

            BusinessLogic.Naukri naukri = new BusinessLogic.Naukri();
            naukri.UpdateProfile();
            naukri.UploadResume(naukri.originalResumePath);
            naukri.ApplyForJobs();
            naukri.TearDown();
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}