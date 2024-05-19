using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class UserDeliveryInfoViewModel
    {
        [Required(ErrorMessage = "Укажите ваше имя")]        
        
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите E-mail")]
        [EmailAddress(ErrorMessage = "Некорректный E-mail адрес")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "Укажите Телефон")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Номер телефона должен содержать 11 цифр")]
        [Phone(ErrorMessage = "Некорректный номер телефона")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Укажите Адрес")]
        public string Address { get; set; }
    }
}
