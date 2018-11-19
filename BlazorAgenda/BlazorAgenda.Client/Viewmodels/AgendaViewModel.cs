using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorAgenda.Client.Viewmodels
{
    public class AgendaViewModel : BlazorComponent
    {
        [Inject]
        protected HttpClient HttpClient { get; set; }

        public AgendaViewModel()
        {
        }
    }
}
