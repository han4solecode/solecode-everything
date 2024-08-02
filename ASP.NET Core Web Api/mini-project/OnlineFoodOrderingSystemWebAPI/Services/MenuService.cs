using System.Collections;
using OnlineFoodOrderingSystemWebAPI.Interfaces;
using OnlineFoodOrderingSystemWebAPI.Models;

namespace OnlineFoodOrderingSystemWebAPI.Services
{
    public class MenuService : IMenuService
    {
        private static List<Menu> menus = [];

        private static Dictionary<int, List<double>> storedRating = [];

        public MenuService()
        {

        }

        public Menu AddMenu(Menu menu)
        {
            menus.Add(menu);
            var v = new List<double>
            {
                menu.Rating
            };
            storedRating.Add(menu.Id, v);

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

        public bool AddRating(int id, double rating)
        {
            var menuToAddRating = GetMenuById(id);

            if (menuToAddRating == null)
            {
                return false;
            }

            if (storedRating.TryGetValue(id, out List<double>? value))
            {
                value.Add(rating);
                menuToAddRating.Rating = Math.Round(value.Average(), 2);
            }

            return true;
        }
    }
}