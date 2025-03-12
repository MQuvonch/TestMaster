using System.ComponentModel.DataAnnotations;

namespace TestExecution.Service.DTOs.User;

public class RegistrForCreationDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    public string PasswordHash { get; set; }
    [Required()]
    [EmailAddress]
    public string Email { get; set; }
    public int VerifyCode {  get; set; }    
}
