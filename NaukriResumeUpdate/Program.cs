﻿using System;
using System.Configuration;

namespace NaukriResumeUpdate
{
    public class Program
    {

        private static void Main(string[] args)
        {
            BusinessLogic.NaukriJobScrapper naukriJobScrapper = new BusinessLogic.NaukriJobScrapper();
            try
            {
                string totalpagetoscrap = ConfigurationManager.AppSettings["totalpagetoscrap"];

                for (int i = 1; i < Convert.ToInt32(totalpagetoscrap); i++)
                {
                    naukriJobScrapper.ScrapData(i);
                }
                naukriJobScrapper.CloseBrowser();

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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            Console.WriteLine("Process Completed");
            Console.ReadLine();
        }
    }
}