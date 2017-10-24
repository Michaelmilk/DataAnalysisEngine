using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TechTest
{
    public class CancelTask
    {
        public static void CancelTaskByTime()
        {
            var cts = new CancellationTokenSource();
            var task = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2000);
                return "hahah";
            }, cts.Token);
            if (Task.WaitAny(new[] { task }, TimeSpan.FromMilliseconds(5000)) < 0)
            { // timeout
                Console.WriteLine("timeout");
                cts.Cancel();
            }
            else if (task.Exception != null)
            {
                // exception
                cts.Cancel();
                throw task.Exception;
            }
            else
            {
                Console.WriteLine(task.Result);
            }
            Console.WriteLine("end");

        }
    }
}
