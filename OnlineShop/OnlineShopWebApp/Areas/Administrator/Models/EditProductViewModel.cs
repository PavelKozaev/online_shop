using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Administrator.Models
{
    public class EditProductViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Название продукта обязательно к заполнению")]
        [MinLength(2, ErrorMessage = "Название продукта слишком короткое")]
        [MaxLength(100, ErrorMessage = "Название продукта слишком длинное")]
        [Display(Name = "Название книги")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Имя автора обязательно к заполнению")]
        [MinLength(2, ErrorMessage = "Имя автора слишком короткое")]
        [MaxLength(100, ErrorMessage = "Имя автора слишком длинное")]
        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Не указана стоимость продукта")]
        [Range(0.01, 1000000, ErrorMessage = "Стоимость должна быть в диапазоне от 0.01 до 1000000")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Цена")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Описание продукта необходимо")]
        [StringLength(1000, ErrorMessage = "Описание продукта слишком длинное")]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        public List<string> ImagesPaths { get; set; }
        public IFormFile[] UploadedFiles { get; set; }
    }
}
