using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://folkis2017.sharepoint.com/sites/Tim";
            string clientId = ConfigurationManager.AppSettings["ClientId"];
            string Secret = ConfigurationManager.AppSettings["ClientSecret"];

            

            AuthenticationManager authManager = new AuthenticationManager();
            using (ClientContext ctx = authManager.GetAppOnlyAuthenticatedContext(url, clientId, Secret))
            {
                ctx.Load(ctx.Web);
                ctx.ExecuteQuery();
                Console.WriteLine("title before change " + ctx.Web.Title);


                ctx.Web.Title = "App Changed The Title";
                ctx.Web.Update();
                ctx.ExecuteQuery();

                Console.WriteLine("Title after change " + ctx.Web.Title);
                Console.WriteLine(ctx.Web.Url);

            }

            Console.WriteLine("Press enter");
            Console.ReadKey();


        }
    }
}
