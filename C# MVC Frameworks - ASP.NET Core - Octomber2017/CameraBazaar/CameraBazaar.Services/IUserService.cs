namespace CameraBazaar.Services
{
    using CameraBazaar.Services.Models.Users;

    public interface IUserService
    {
        UserDetailsModel ById(string id);   
    }
}
