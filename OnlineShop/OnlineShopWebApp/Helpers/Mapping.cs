using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Administrator.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Helpers
{
    public static class Mapping
    {
        public static List<ProductViewModel> ToProductViewModels(this List<Product> products)
        {
            var productViewModels = new List<ProductViewModel>();
            foreach (var product in products)
            {
                productViewModels.Add(ToProductViewModel(product));
            }
            return productViewModels;
        }

        public static ProductViewModel ToProductViewModel(this Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Author = product.Author,
                Cost = product.Cost,
                Description = product.Description,
                ImagesPaths = product.Images.Select(x => x.Url).ToArray()
            };
        }

        public static Product ToProduct(this AddProductViewModel addProductViewModel, List<string> imagesPaths)
        {
            return new Product
            {
                Name = addProductViewModel.Name,
                Author = addProductViewModel.Author,
                Cost = addProductViewModel.Cost,
                Description = addProductViewModel.Description,
                Images = ToImages(imagesPaths)
            };
        }

        public static List<Image> ToImages(this List<string> paths) 
        {
            return paths.Select(x => new Image { Url = x }).ToList();
        }

        public static List<string> ToPaths(this List<Image> paths)
        {
            return paths.Select(x => x.Url).ToList();
        }


        public static EditProductViewModel ToEditProductViewModel(this Product product)
        {
            return new EditProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Author = product.Author,
                Cost = product.Cost,
                Description = product.Description,
                ImagesPaths = product.Images.ToPaths()
            };
        }

        public static Product ToProduct(this EditProductViewModel editProduct)
        {
            return new Product
            {
                Id = editProduct.Id,
                Name = editProduct.Name,
                Author = editProduct.Author,
                Cost = editProduct.Cost,
                Description = editProduct.Description,
                Images = editProduct.ImagesPaths.ToImages()
            };
        }
    }
}
