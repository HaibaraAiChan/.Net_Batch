namespace MovieshopMVC.Services
{
    public interface ICurrentUser
    {
        int? UserId { get; }
        bool isAdmin { get; }
        bool IsAuthenticated { get; }
        string? Email { get; }
        string? FullName { get; }
        IEnumerable<string> Roles { get; }
       
       

    }
}
