using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Название продукта обязательно к заполнению")]
        [MinLength(2, ErrorMessage = "Название продукта слишком короткое")]
        [MaxLength(100, ErrorMessage = "Название продукта слишком длинное")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Имя автора обязательно к заполнению")]
        [MinLength(2, ErrorMessage = "Имя автора слишком короткое")]
        [MaxLength(100, ErrorMessage = "Имя автора слишком длинное")]
        [Display(Name = "Author")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Не указана стоимость продукта")]
        [Range(0.01, 1000000, ErrorMessage = "Стоимость должна быть в диапазоне от 0.01 до 1000000")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Cost")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Описание продукта необходимо")]
        [StringLength(1000, ErrorMessage = "Описание продукта слишком длинное")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Необходимо указать URL изображения")]
        [Display(Name = "ImageUrl")]
        public string ImageUrl { get; set; }

        public Product() => Id = Guid.NewGuid();

        public Product(string name, string author, decimal cost, string description, string imageUrl)
        {
            Id = Guid.NewGuid();
            Name = name;
            Author = author;
            Cost = cost;
            Description = description;
            ImageUrl = imageUrl;
        }        
    }
}
