using System.Collections.Generic;

namespace KrisApp.Models.Nav
{
    public class MenuItemModel
    {
        public MenuItemModel()
        {
            SubMenu = new List<MenuItemModel>();
        }

        public string Text { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Parameter { get; set; }
        public bool Active { get; set; }

        public List<MenuItemModel> SubMenu { get; set; }
    }
}