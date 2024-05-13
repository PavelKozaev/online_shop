using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Administrator.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Название роли обязательно к заполнению")]
        public string Name { get; set; }
        public RoleViewModel() { }

        public RoleViewModel(string name)
        {
            Name = name;
        }

        public override bool Equals(object? obj)
        {
            var role = (RoleViewModel)obj;
            return Name == role.Name;
        }
    }
    
}
