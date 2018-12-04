using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAgenda.Services.Interfaces
{
    public interface IStateService
    {
        User LoginUser { get; set; }
    }
}
