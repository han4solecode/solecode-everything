using OnlineFoodOrderingSystemWebAPI.Models;

namespace OnlineFoodOrderingSystemWebAPI.Interfaces
{
    public interface IMenuService
    {
        Menu AddMenu(Menu menu);

        List<Menu> GetAllMenu();

        Menu GetMenuById(int id);

        Menu UpdateMenu(int id, Menu inputMenu);

        bool DeleteMenu(int id);

        void AddRating(int rating);
    }
}