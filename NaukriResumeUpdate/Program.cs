using System;

using Topshelf;

namespace NaukriResumeUpdate
{
    public class Program
    {

        
        static void Main(string[] args)
        {

            


            
            



            var exitCode = HostFactory.Run(x =>
              {
                  x.Service<TaskManager>(s =>
                  {

                      s.ConstructUsing(taskmanager => new TaskManager());

                      s.WhenStarted(taskmanager => taskmanager.Start());
                      s.WhenStopped(taskmanager => taskmanager.Stop());
                  });


                  x.RunAsLocalSystem();
                  x.SetServiceName("NaukriAutomation");
                  x.SetDisplayName("Naukri Automation");
                  x.SetDescription("Daily update Naukri Profile");
              });


            int exitcodevalue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());


            Environment.ExitCode = exitcodevalue;




        }
    }
}
