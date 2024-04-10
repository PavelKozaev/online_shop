using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Введите email")]
        [EmailAddress(ErrorMessage = "Введите валидный email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен быть не менее 6 символов")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Повторите пароль")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен быть не менее 6 символов")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Введите ваше имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите вашу фамилию")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Введите номер вашего телефона")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Номер телефона должен содержать 11 цифр")]
        [Phone(ErrorMessage = "Некорректный номер телефона")]
        public string Phone { get; set; }
    }
}
