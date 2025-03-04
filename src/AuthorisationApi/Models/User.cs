namespace Authorisation.Models // With this name it is possible to refer to the class that is declared under this spacename
{
    public class UserItem { // Defines class UserItem that playes as a model(data transfer object) to display user info
        // Public so that other classes or parts of the program can access these parts
        public long Id {get; set;}
        public string UserName {get; set;}
        public string Password {get; set;}
        public UserItem (long id, string userName, string password) // Constructor method which creates a new UserItem object. Here is defined which parameters are needed to create the object.
        {
            // Sets the values for these variables from the parameters values.
            Id = id;
            UserName = userName;
            Password = password;
        }
    }

}


