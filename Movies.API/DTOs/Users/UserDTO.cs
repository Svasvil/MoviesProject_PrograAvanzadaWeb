namespace Movies.API.DTOs.Users
{
    public record  UserDTO
     (
        int id,
        string nombre,
        string apellido,
        string email
       );
}
