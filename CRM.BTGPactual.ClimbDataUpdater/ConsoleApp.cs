using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataUpdater
{
    public class ConsoleApp
    {
        private Updater Updater = new Updater();
        public void Run()
        {
            Console.Clear();
            Console.WriteLine("Connecting...");
            Console.WriteLine("Connected to: " + Updater.Service.ConnectedOrgPublishedEndpoints[Microsoft.Xrm.Sdk.Discovery.EndpointType.WebApplication]);
            Console.Clear();

            Console.WriteLine("Connected to: " + Updater.Service.ConnectedOrgPublishedEndpoints[Microsoft.Xrm.Sdk.Discovery.EndpointType.WebApplication] + "\n");
            Console.WriteLine(@"Type the number of which operation you want to perform and hit enter:
1  - Update
2  - Assign
3  - Share
4  - Unshare
5  - Delete
6  - Deactivate
7  - Create
8  - Add List Member
9  - Remove List Member
10 - Associate
");

            var option = Console.ReadLine().Trim().ToLower();
            Console.WriteLine();
            switch (option)
            {
                case "1":
                    Updater.Update();
                    break;
                case "2":
                    Updater.Assign();
                    break;
                case "3":
                    Updater.Share();
                    break;
                case "4":
                    Updater.Unshare();
                    break;
                case "5":
                    Updater.Delete();
                    break;
                case "6":
                    Updater.Deactivate();
                    break;
                case "7":
                    Updater.Create();
                    break;
                case "8":
                    Updater.AddListMember();
                    break;
                case "9":
                    Updater.RemoveListMember();
                    break;
                case "10":
                    Updater.Associate();
                    break;
                default:
                    break;
            }

            Console.Write("\nPerform another operation? (Y/N) ");
            if (Console.ReadLine().Trim().ToLower() == "y")
            {
                Run();
            }
        }
    }
}
