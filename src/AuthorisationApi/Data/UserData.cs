using Authorisation.Models;

namespace AuthorisationApi.Data
{
    public class UserData // Declares UserData class which offers methods for handling the data (ex. initialization or retrieval)
    {
        //Initialdata(GetInitialUsers) is returned if the list is empty
        //Static method which can be called without creating a UserData object
        public static List<UserItem> GetInitialUsers()
        {
            //Returns a list of UserItem objects which are initialized with hardcoded values
            return new List<UserItem>
            {   // Creates a new UserItem object with hardcoded values
                new UserItem(1, "Muumi", "1234"), //Id, UserName ja Password
                new UserItem(2, "Tähtioppilas", "salasana"),
                new UserItem(3, "Majakka", "valo00")
            };
        }
    }
}