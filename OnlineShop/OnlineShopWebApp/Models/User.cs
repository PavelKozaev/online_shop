using OnlineShopWebApp.Models;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public Role Role { get; set; }

    public User() => Id = Guid.NewGuid();

    public User(string email, string password, string firstName, string lastName, string phone)
    {
        Id = Guid.NewGuid();
        Role = new Role("User");
        Email = email;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
    }
}