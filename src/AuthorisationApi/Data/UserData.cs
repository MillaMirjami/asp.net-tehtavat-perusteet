using Authorisation.Models;

namespace AuthorisationApi.Data
{
    public class UserData
    {
        //Esimerkkitiedot palautetaan, jos lista on tyhjä
        public static List<UserItem> GetInitialUsers()
        {
            return new List<UserItem>
            {
                new UserItem(1, "Muumi", "1234"), //Id, UserName ja Password
                new UserItem(2, "Tähtioppilas", "salasana"),
                new UserItem(3, "Majakka", "valo00")
            };
        }
    }
}