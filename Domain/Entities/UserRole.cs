namespace Domain.Entities;

public class UserRole
{
    public int IdUserFk {get;set;}
    public User User {get;set;}
    public int IdRolFk {get;set;}
    public Role Role {get;set;}
}