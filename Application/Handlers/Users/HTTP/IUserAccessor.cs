namespace Application.DTOs.Users.HTTP
{
    public interface IUserAccessor
    {
        string GetUsername();
        Guid GetUserId();
    }
}
