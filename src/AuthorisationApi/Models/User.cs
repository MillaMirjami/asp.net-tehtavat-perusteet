namespace Authorisation.Models;
    public class UserItem {
        public long Id {get; set;}
        public string UserName {get; set;}
        public string Password {get; set;}
        public UserItem (long id, string userName, string password)
        {
            Id = id;
            UserName = userName;
            Password = password;
        }
    }


