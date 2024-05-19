using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class UserDeliveryInfoViewModel
    {
        [Required(ErrorMessage = "Укажите полностью ваше имя, фамилию и отчество")]        
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите E-mail")]
        [EmailAddress(ErrorMessage = "Некорректный E-mail адрес")]
        [Display(Name = "Email")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "Укажите Телефон")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Номер телефона должен содержать 11 цифр")]
        [Phone(ErrorMessage = "Некорректный номер телефона")]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Укажите Адрес")]
        [RegularExpression(@"^[а-яА-ЯёЁ0-9s.,-/]+$", ErrorMessage = "Адрес может содержать буквы, цифры, пробелы и символы . , - /")]
        [Display(Name = "Адрес")]
        public string Address { get; set; }
    }
}
