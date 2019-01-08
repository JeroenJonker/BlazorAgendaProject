using BlazorAgenda.Client.Viewmodels.BaseViewModels;
using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Models;
using Microsoft.AspNetCore.Blazor.Components;

namespace BlazorAgenda.Client.Viewmodels
{
    public class UserViewModel : ObjectBase<User,IUserService>
    {
        [Inject] [Parameter] protected override User CurrentObject { get; set; }
        [Inject] public override IUserService CurrentService { get; set; }

        protected override void OnInit()
        {
            base.OnInit();
            if (CurrentObject != default(User))
            {
                CurrentObject.Password = string.Empty;
            }
        }
    }
}
