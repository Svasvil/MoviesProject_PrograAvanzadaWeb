namespace Movies.API.DTOs.Users
{
    public record CreateUserDTO
       (
        int id,
        string username,
        string email
       );
}
