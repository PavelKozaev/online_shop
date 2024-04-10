using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Role
    {
        [Required(ErrorMessage = "Название роли обязательно к заполнению")]
        public string Name { get; set; }
        public Role() { }

        public Role(string name)
        {
            Name = name;
        }
    }
    
}
