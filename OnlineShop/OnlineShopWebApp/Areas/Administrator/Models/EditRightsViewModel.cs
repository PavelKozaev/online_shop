﻿namespace OnlineShopWebApp.Areas.Administrator.Models
{
    public class EditRightsViewModel
    {
        public string UserName { get; set; }
        public List<RoleViewModel> UserRoles { get; set; }
        public List<RoleViewModel> AllRoles { get; set; }
    }
}
