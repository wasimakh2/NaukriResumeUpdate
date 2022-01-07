using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace NaukriResumeUpdate
{
    public class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                
                if(File.Exists("user.json"))
                {
                    // deserialize JSON directly from a file
                    using (StreamReader file = File.OpenText(@"user.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        BusinessLogic.NaukriJobScrapper.userDetails = serializer.Deserialize(file, typeof(UserDetails)) as UserDetails;
                    }
                }
                else
                {
                    Console.WriteLine("Please provide naukri username:");
                    var UserName = Console.ReadLine();
                    Console.WriteLine("Please provide naukri password:");
                    var Password = Console.ReadLine();
                    Console.WriteLine("Please provide mobile number:");
                    var MobileNumber = Console.ReadLine();
                    Console.WriteLine("Please provide jobkeysearch:");
                    var jobkeysearch = Console.ReadLine();
                    Console.WriteLine("Please provide joblocation:");
                    var joblocation = Console.ReadLine();


                    BusinessLogic.NaukriJobScrapper.userDetails.UserName = UserName;
                    BusinessLogic.NaukriJobScrapper.userDetails.Password = Password;
                    BusinessLogic.NaukriJobScrapper.userDetails.MobileNumber = MobileNumber;
                    BusinessLogic.NaukriJobScrapper.userDetails.jobkeysearch = jobkeysearch;
                    BusinessLogic.NaukriJobScrapper.userDetails.joblocation = joblocation;
                    
                    //open file stream
                    using (StreamWriter file = File.CreateText(@"user.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        //serialize object directly into file stream
                        serializer.Serialize(file, BusinessLogic.NaukriJobScrapper.userDetails);
                    }
                }
                


                



                BusinessLogic.NaukriJobScrapper naukriJobScrapper = new();
                string totalpagetoscrap = ConfigurationManager.AppSettings["totalpagetoscrap"];

                for (int i = 1; i < Convert.ToInt32(totalpagetoscrap); i++)
                {
                    naukriJobScrapper.ScrapData(i);
                }
                naukriJobScrapper.CloseBrowser();

                BusinessLogic.Naukri naukri = new();
                naukri.UpdateProfile();
                if(File.Exists(naukri.OriginalResumePath))
                {
                    naukri.UploadResume(naukri.OriginalResumePath);
                }
                
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
                Console.WriteLine(ex);
                Console.ReadLine();
            }
            Console.WriteLine("Process Completed");
            Console.ReadLine();
        }
    }
}