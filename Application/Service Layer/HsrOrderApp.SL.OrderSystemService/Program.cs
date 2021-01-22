#region

using System;
using System.ServiceModel;

#endregion

namespace HsrOrderApp.SL.OrderSystemService
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (ServiceHost sh = new ServiceHost(typeof (OrderSystemService)))
            {
                sh.Open();
                Console.WriteLine("Order-System-Service bereit!");

                Console.ReadLine();
            }
        }
    }
}