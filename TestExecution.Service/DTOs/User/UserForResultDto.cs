namespace TestExecution.Service.DTOs.User;

public class UserForResultDto
{
    public Guid Id {  get; set; } 
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
}
