using TestExecution.Domain.Commons;

namespace TestExecution.Domain.Entities;
public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }    
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public int Role { get; set; }  
    public IEnumerable<Test> Tests { get; set; }
    public ICollection<UserAttempt> Attempts { get; set; }
}

public enum Roles
{
    mentor = 1,
    Admin = 2
}