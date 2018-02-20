using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppClientIdSecret_AppOnlyPermission
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://folkis2017.sharepoint.com/sites/Tim";
            //get settings from your app.config file
            string clientId = ConfigurationManager.AppSettings["ClientId"];
            string clientSecret = ConfigurationManager.AppSettings["ClientSecret"];


            AuthenticationManager authManager = new AuthenticationManager();
            using (ClientContext ctx = authManager.GetAppOnlyAuthenticatedContext(url, clientId, clientSecret))
            {
                ctx.Load(ctx.Web);
                ctx.ExecuteQuery();

                Console.WriteLine(ctx.Web.Title);

                if (!ctx.Web.ListExists("FromApp"))
                {
                    List list = ctx.Web.CreateList(ListTemplateType.GenericList, "FromApp", false);
                }
                List lists = ctx.Web.GetListByTitle("FromApp");

                ListItem item = lists.AddItem(new ListItemCreationInformation());
                item["Title"] = "Made this App";
                item.Update();
                ctx.ExecuteQuery();

                User user = ctx.Web.EnsureUser("Tim@folkis2017.onmicrosoft.com");
                ctx.Load(user, u => u.Id);
                ctx.ExecuteQuery();

                ListItem item2 = lists.AddItem(new ListItemCreationInformation());
                item2["Title"] = "Made this App but with user";
                item2["Author"] = user.Id;
                item2["Editor"] = user.Id;
                item2.Update();
                ctx.ExecuteQuery();

            }

            Console.WriteLine("Press enter to continue");
            Console.ReadKey();
            
        }
    }
}
