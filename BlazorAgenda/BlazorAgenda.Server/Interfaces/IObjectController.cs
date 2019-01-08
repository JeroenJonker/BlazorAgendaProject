using BlazorAgenda.Shared;
using BlazorAgenda.Shared.Interfaces.BaseObjects;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAgenda.Server
{
    public interface IObjectController<T> where T : IBaseObject
    {
        IActionResult Add([FromBody] T Object);
        IActionResult Edit([FromBody] T Object);
        IActionResult Delete([FromBody] T Object);
    }
}
