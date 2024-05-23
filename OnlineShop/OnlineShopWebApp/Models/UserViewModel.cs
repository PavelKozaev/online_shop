namespace OnlineShopWebApp.Models
{
    public class UserViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; } = "/images/Profiles/68a750f6-5bef-4835-bfcd-f6e71260252d.jpg";
        public IFormFile? UploadedFile { get; set; }
    }
}