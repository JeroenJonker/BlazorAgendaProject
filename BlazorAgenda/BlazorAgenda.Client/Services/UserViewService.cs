using BlazorAgenda.Services.Interfaces;
using BlazorAgenda.Shared.Interfaces;
using BlazorAgenda.Shared.Models;
namespace BlazorAgenda.Client.Services
{
    public class UserViewService : BaseObjectViewService<User, IUserService>
    {
        public override User CurrentObject { get; set; }
        public override User DefaultBaseObject { get; set; }
        public override IUserService CurrentService { get; set; }

        public UserViewService(IUser user, IUserService userService)
        {
            DefaultBaseObject = CurrentObject = user as User;
            CurrentService = userService;
        }
    }
}
