#region

using System;
using System.ServiceModel;

#endregion

namespace HsrOrderApp.SL.DistributionSystemService
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (ServiceHost sh = new ServiceHost(typeof (DistributionSystemService)))
            {
                sh.Open();
                Console.WriteLine("Distribution-System-Service bereit!");

                Console.ReadLine();
            }
        }
    }
}