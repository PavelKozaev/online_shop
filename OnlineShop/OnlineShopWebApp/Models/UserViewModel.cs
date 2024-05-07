using OnlineShopWebApp.Areas.Administrator.Models;
using System.ComponentModel.DataAnnotations;


namespace OnlineShopWebApp.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль обязателен к заполнению")]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public Role Role { get; set; }

        public UserViewModel() => Id = Guid.NewGuid();

        public UserViewModel(string email, string password, string firstName, string lastName, string phone)
        {
            Id = Guid.NewGuid();
            Role = new Role("Customer");
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
        }
    }
}