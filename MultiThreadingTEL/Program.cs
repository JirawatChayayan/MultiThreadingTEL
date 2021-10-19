using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace MultiThreadingTEL
{
    internal class Program
    {
        private static System.Timers.Timer aTimer;
        static int countpolling = 0;
        static void Main(string[] args)
        {
            //polling timer setting
            SetTimer(2000);
            Console.Read();
        }
        private static void SetTimer(int interval)
        {
            aTimer = new System.Timers.Timer(interval);
            aTimer.Elapsed += polling;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        private static void polling(object sender, ElapsedEventArgs e)
        {
            aTimer.Stop();
            /*
                          Your polling logic here
             
             */
            //For test
            countpolling += 1;
            Console.WriteLine($"\r\n\r\n################################# Start Polling {countpolling} ##################################\r\n\r\n");
            int countProcess = 0;
            var rand = new Random(32);
            for (int i = 0; i<50; i++)
            {
                double a = rand.NextDouble();
                if(a >= 0.5)
                {
                    countProcess += 1;
                    paramTestThread param = new paramTestThread();
                    param.processNum = countProcess;
                    param.delay = (new Random()).Next(1000, 2000);
                    param.index = i;
                    param.pollingCount = countpolling;

                    /*
                         Send to next process Here
                     
                     */

                    //Usethread uncomment here
                    //Thread t = new Thread((new processing()).doProcess);
                    //t.Start(param);

                    //don't use thread un comment here 
                    (new processing()).doProcess(param);
                }

            }
            Console.WriteLine($"\r\n\r\n################################## Finish Polling {countpolling} ##############################\r\n\r\n");
            aTimer.Start();

        }
       
    }
    class processing
    {
        public void doProcess(object x)
        {
            /* 
                     Your process Here
             */
            var y = (paramTestThread)x;
            Console.WriteLine($"Polling count {y.pollingCount} Start Thread Process {y.processNum} from Index {y.index}");
            Thread.Sleep(y.delay);
            Console.WriteLine($"Polling count {y.pollingCount} Finish Thread  Process {y.processNum} at {y.delay} from Index {y.index}");
        }
    }
    class paramTestThread
    {
        public int processNum { get; set; }
        public int delay { get; set; }
        public int index { get; set; }
        public int pollingCount { get; set; }
    }
}
