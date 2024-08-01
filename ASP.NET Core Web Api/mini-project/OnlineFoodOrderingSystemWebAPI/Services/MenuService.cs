using OnlineFoodOrderingSystemWebAPI.Interfaces;
using OnlineFoodOrderingSystemWebAPI.Models;

namespace OnlineFoodOrderingSystemWebAPI.Services
{
    public class MenuService : IMenuService
    {
        private static List<Menu> menus = [];

        public MenuService()
        {
            
        }

        public Menu AddMenu(Menu menu)
        {
            menus.Add(menu);
            return menu;
        }

        public List<Menu> GetAllMenu()
        {
            return menus;
        }

        public Menu? GetMenuById(int id)
        {
            var menuById = menus.FirstOrDefault(m => m.Id == id);

            if (menuById == null)
            {
                return null;
            }

            return menuById;
        }

        public Menu? UpdateMenu(int id, Menu inputMenu)
        {
            var menuToBeUpdated = GetMenuById(id);

            if (menuToBeUpdated == null)
            {
                return null;
            }

            menuToBeUpdated.Id = inputMenu.Id;
            menuToBeUpdated.Name = inputMenu.Name;
            menuToBeUpdated.Price = inputMenu.Price;
            menuToBeUpdated.Category = inputMenu.Category;
            menuToBeUpdated.Rating = inputMenu.Rating;
            menuToBeUpdated.CreatedDate = inputMenu.CreatedDate;
            menuToBeUpdated.IsAvailable = inputMenu.IsAvailable;

            return menuToBeUpdated;
        }

        public bool DeleteMenu(int id)
        {
            var menuToBeDeleted = GetMenuById(id);

            if (menuToBeDeleted == null)
            {
                return false;
            }

            menus.Remove(menuToBeDeleted);
            return true;
        }

        public void AddRating(int rating)
        {

        }
    }
}