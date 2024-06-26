
class UserService
{
    UserRepo ur;

    public UserService(UserRepo ur)
    {
        this.ur = ur;
    }

    //Register - this is not prompting the user for input, but rather taking in a User object.

    public User? RegisterUser(User u)
    {
        //This is where we would do some validation.
        //For example, we could check if the email is already in use.
        //We could also check if the password is strong enough
        //We could also check if the username is already in use.

        if (u.Role != "user")
        {
            System.Console.WriteLine("You can only register as a user!");

            return null;
        }


#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        List<User> allUsers = ur.GetAllUsers();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

        foreach (User user in allUsers)
        {

            if (user.Username == u.Username) //if the username is already in use
            {
                System.Console.WriteLine("Username already in use");
                return null; //reject the user
                //we dont need a break because as soon as we return, the method is over.
            }

            
        }

        return ur.AddUser(u); //if you didn't care about the validation, you could just do this. It would be more of trivial service.
        //.....but we do care soooo we will do some validation - only let the role "user" register in the if statement and foreach above.


}

//Login below will not use the User object, but rather pass in the information needed to login.

public User? LoginUser(string username, string password)

{


    List<User> allUsers = ur.GetAllUsers();

    foreach (User user in allUsers)
    {
        if (user.Username == username && user.Password == password) //BOTH username and password must match to login. the && is the AND operator and checks to make sure both sides are true.
        {
            return user;
        }

    }

    System.Console.WriteLine("Invalid Username or Password");
    return null; //if the foreach loop completes and no user is found, then the user does not exist in the system.



}

    internal User? GetUser(string username)
    {
        List<User> allUsers = ur.GetAllUsers();

        foreach (User user in allUsers)
        {
            if (user.Username == username)
            {
                return user;
            }
        }

        return null;
    }
}