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
        CalendarService Service { get; set; }

        public SampleDataController()
        {
            UserCredential credential = GetCredential();

            // Create Google Calendar API service.
            Service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        private static UserCredential GetCredential()
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

            return credential;
        }

        [HttpGet("[action]")]
        public List<CalendarEvent> GetEvents()
        {
            // Define parameters of request.
            EventsResource.ListRequest request = Service.Events.List("primary");
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

        [HttpPost("[action]")]
        public IActionResult PostEvent([FromBody] CalendarEvent calendarEvent)
        {
            BatchRequest addrequest = new BatchRequest(Service);
            addrequest.Queue<CalendarList>(Service.CalendarList.List(),
                 (content, error, i, message) =>
                 {
                     // Put your callback code here.
                 });
            addrequest.Queue<Event>(Service.Events.Insert(
             new Event
             {
                 Summary = calendarEvent.Summary,
                 Start = new EventDateTime() { DateTime = calendarEvent.Start },
                 End = new EventDateTime() { DateTime = calendarEvent.End }
             }, "primary"),
             (content, error, i, message) =>
             {
                 // Put your callback code here.
             });
            // You can add more Queue calls here.

            // Execute the batch request, which includes the 2 requests above.
            addrequest.ExecuteAsync();
            return Ok();
        }
    }
}
