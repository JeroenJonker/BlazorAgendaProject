﻿using BlazorAgenda.Shared;
using BlazorAgenda.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAgenda.Services
{
    public interface IDefaultObjectService
    {
        Task ExecuteAsync();
        event Action OnChange;
        void NotifyStateChanged();
        void CurrentObjectToNull();
        ObjectState GetObjectState();
        string GetObjectName();
        //T CurrentObject { get; set; }
    }
}
