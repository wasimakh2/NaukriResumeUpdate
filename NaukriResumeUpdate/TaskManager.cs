using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace NaukriResumeUpdate
{
    public class TaskManager
    {

        private readonly Timer _timer;

        public TaskManager()
        {
            _timer = new Timer(1000*60*60) { AutoReset = true };

            _timer.Elapsed += TimerElapsed;  
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Naukri naukri = new Naukri();
            naukri.UpdateProfile();
            naukri.UploadResume(naukri.originalResumePath);

            naukri.tearDown();
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
