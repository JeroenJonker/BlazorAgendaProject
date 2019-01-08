using BlazorAgenda.Shared;
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
