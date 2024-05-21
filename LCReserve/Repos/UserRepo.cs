class UserRepo
{


    UserStorage userStorage = new();

    //add, get-one, get-all, update, delete


    public User AddUser(User u)
    {
        
        u.Id = userStorage.idCounter++;  //incrementing the value afterwards, to prep it for the next time it's needed.
        userStorage.users.Add(u.Id, u); //Add the user into our collection.
        return u; //return the user
    }

    public User? GetUser(int id)
    {
        // Alternative approach that breaks each step down.
        if (userStorage.users.ContainsKey(id)) //if the user exists
        {
            User selectedUser = userStorage.users[id]; //select the user
            return selectedUser; //return the user
            //return UserStorage.Users[id];
        }
        else
        {
            System.Console.WriteLine("Invalid User ID - Please Try Again");
            return null;
        }
    }

    //THIS IS A NEW METHOD!
    //No Parameters because...get everything is get everything. No options to choose.
    public List<User> GetAllUsers()
    {
        //I am choosing to return a List because that is a much more common collection to
        //work with. It does mean I have to do a little bit of work here - but its not bad.
        return userStorage.users.Values.ToList();
    }


    public User? UpdateUser(User updatedUser)
    {
        //Assuming that the ID is consistent with an ID that exists
        //then we just have to update the value (aka User) for said key (ID) within the Dictionary.
        try
        {
            userStorage.users[updatedUser.Id] = updatedUser;
            //I choose to send the updated User back as a "response-feedback" system.
            //"Here is me telling you that I have updated the storage with this User I send back to you"
            return updatedUser;
        }
        catch (Exception)
        {
            System.Console.WriteLine("Invalid User ID - Please Try Again");
            return null;
        }
    }

    public User? DeleteUser(User m)
    {
        //If we have the ID -> then simply Remove it from storage
        bool didRemove = userStorage.users.Remove(m.Id);

        if (didRemove)
        {
            //now we will return the User that got deleted.
            return m;
        }
        else
        {
            System.Console.WriteLine("Invalid User ID - Please Try Again");
            return null;
        }
    }

}