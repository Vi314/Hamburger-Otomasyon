using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CA_Hamburger.Concrete;

namespace CA_Hamburger.Data
{
    internal static class Data
    {
        public static List<Account> accounts = new List<Account>()
        {
            new Account() {ID = 1, Nickname = "Admin", FirstName = "Pro", LastName = "Gamer", Password = "1234abcd", Balance = 99999}
        };


        public static List<Order> orders = new List<Order>();

        public static List<Menu> menus = new List<Menu>()
        {
            new Menu() {ID = 1,Name = "Whooper", Price = 60},
            new Menu() {ID = 2,Name = "BigKing", Price = 65 },
            new Menu() {ID = 3,Name = "SteakHouse", Price = 80},
            new Menu() {ID = 4,Name = "KingChicken", Price = 50}
        };
        
        public static List<Extra> extras = new List<Extra>()
        {
            new Extra() {ID = 1,Name = "Hardal", Price = 2 },
            new Extra() {ID = 2,Name = "BBQ", Price = 2},
            new Extra() {ID = 3,Name = "Buffalo", Price = 2.5m},
            new Extra() {ID = 4,Name = "Mayonez", Price = 1 },
            new Extra() {ID = 5,Name = "Ketçap", Price = 1}
        };

    }
}
