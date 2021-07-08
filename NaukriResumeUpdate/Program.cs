using System;

using Topshelf;

namespace NaukriResumeUpdate
{
    public class Program
    {

        
        static void Main(string[] args)
        {

            for (int i = 0; i < 2; i++)
            {
                BusinessLogic.NaukriJobScrapper.ScrapData(i);
            }


            BusinessLogic.Naukri naukri = new BusinessLogic.Naukri();
            naukri.UpdateProfile();
            naukri.UploadResume(naukri.originalResumePath);
            naukri.ApplyForJobs();
            naukri.TearDown();


            //var exitCode = HostFactory.Run(x =>
            //  {
            //      x.Service<TaskManager>(s =>
            //      {

            //          s.ConstructUsing(taskmanager => new TaskManager());

            //          s.WhenStarted(taskmanager => taskmanager.Start());
            //          s.WhenStopped(taskmanager => taskmanager.Stop());
            //      });


            //      x.RunAsLocalSystem();
            //      x.SetServiceName("NaukriAutomation");
            //      x.SetDisplayName("Naukri Automation");
            //      x.SetDescription("Daily update Naukri Profile");
            //  });


            //int exitcodevalue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());


            //Environment.ExitCode = exitcodevalue;




        }
    }
}
