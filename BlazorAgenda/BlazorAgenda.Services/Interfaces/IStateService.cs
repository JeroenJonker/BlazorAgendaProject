using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAgenda.Services.Interfaces
{
    public interface IStateService
    {
        IUserService LoginUser { get; set; }
    }
}
