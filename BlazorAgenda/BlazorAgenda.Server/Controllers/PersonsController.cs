using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlazorAgenda.Shared;
using Google.Apis.Auth.OAuth2;
using Google.Apis.PeopleService.v1;
using Google.Apis.PeopleService.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAgenda.Server.Controllers
{
    [Route("api/[controller]")]
    public class PersonsController : Controller
    {
        static string[] Scopes = { PeopleServiceService.Scope.Contacts };
        static string ApplicationName = "Blazor agenda";
        PeopleServiceService Service { get; set; }

        private UserCredential GetCredential()
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
                    new[] { PeopleServiceService.Scope.Contacts },
                    "me",
                    CancellationToken.None,
                    new FileDataStore("Calendar.Sample.Store")).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }
            return credential;
        }

        [HttpGet("[action]")]
        public List<Contact> GetContacts()
        {
            UserCredential credential = GetCredential();
            var Pservice = new PeopleServiceService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            PeopleResource.ConnectionsResource.ListRequest peopleRequest = Pservice.People.Connections.List("people/me");
            peopleRequest.PersonFields = "names,emailAddresses";
            ListConnectionsResponse connectionsResponse = peopleRequest.Execute();
            List<Person> connections = connectionsResponse.Connections.ToList();
            return ConvertGooglePersonToContact(connections);
        }

        public List<Contact> ConvertGooglePersonToContact(List<Person> connections)
        {
            List<Contact> result = new List<Contact>();
            foreach (Person person in connections)
            {
                result.Add(
                    new Contact
                    {
                        EmailAdress = person.EmailAddresses.First().Value,
                        FirstName = person.Names.First().GivenName,
                        LastName = person.Names.First().FamilyName
                    });
            }
            return result;
        }
    }
}