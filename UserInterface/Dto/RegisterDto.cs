namespace Dto;


public class Registerdto
{
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? Address { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
    public string UserType { get; set; } = null!;

}
