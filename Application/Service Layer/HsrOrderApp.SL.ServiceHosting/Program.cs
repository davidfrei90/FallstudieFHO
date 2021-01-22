#region

using System;
using System.ServiceModel;

#endregion

namespace HsrOrderApp.SL.ServiceHosting
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (ServiceHost selfHost = new ServiceHost(typeof (AdminService.AdminService)))
            {
                selfHost.Open();
                selfHost.Faulted += new EventHandler(selfHost_Faulted);
                Console.WriteLine("Service ist bereit.");
                Console.ReadLine();
            }
        }

        private static void selfHost_Faulted(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}