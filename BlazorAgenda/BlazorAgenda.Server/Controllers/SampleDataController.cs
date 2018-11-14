using BlazorAgenda.Shared;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Requests;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorAgenda.Server.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Blazor agenda";

        [HttpGet("[action]")]
        public List<CalendarEvent> Test()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();
            List<CalendarEvent> results = new List<CalendarEvent>();
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    DateTime start = eventItem.Start.DateTime ?? DateTime.Parse(eventItem.Start.Date);
                    DateTime end = eventItem.End.DateTime ?? DateTime.Parse(eventItem.End.Date);
                    results.Add(new CalendarEvent { Start = start, End = end, Summary = eventItem.Summary });
                }
            }
            //AuthCallbackController a = new AuthCallbackController();
            return results;
        }

        [HttpGet("[action]")]
        public async Task<string> AddTestAsync()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { CalendarService.Scope.Calendar },
                    "user",
                    CancellationToken.None,
                    new FileDataStore("Calendar.Sample.Store")).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            BatchRequest addrequest = new BatchRequest(service);
            addrequest.Queue<CalendarList>(service.CalendarList.List(),
                 (content, error, i, message) =>
                 {
                     // Put your callback code here.
                 });
            addrequest.Queue<Event>(service.Events.Insert(
             new Event
             {
                 Summary = "Eerste test",
                 Start = new EventDateTime() { DateTime = new DateTime(2018, 11, 14, 12, 0, 0) },
                 End = new EventDateTime() { DateTime = new DateTime(2018, 11, 14, 15, 0, 0) }
             }, "primary"),
             (content, error, i, message) =>
             {
                 // Put your callback code here.
             });
            // You can add more Queue calls here.

            // Execute the batch request, which includes the 2 requests above.
            await addrequest.ExecuteAsync();
            return "ha";
        }
    }
}
