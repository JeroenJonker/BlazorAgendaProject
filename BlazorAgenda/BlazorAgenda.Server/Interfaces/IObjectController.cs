using BlazorAgenda.Shared;
using BlazorAgenda.Shared.Interfaces.BaseObjects;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAgenda.Server
{
    public interface IObjectController<T> where T : IBaseObject
    {
        IActionResult Add([FromBody] T Object);
        IActionResult Edit([FromBody] T Object);
        IActionResult Delete([FromBody] T Object);
    }
}
