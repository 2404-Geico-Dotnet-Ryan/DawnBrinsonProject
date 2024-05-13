class UserStorage
{

    public Dictionary<int, User> users;
    public int idCounter = 1;

    //Making this constructor give us some pre-loaded Movies to work with.
    public UserStorage()
    {
        User user1 = new(idCounter, "CodeIsMyJam", "pass1", "user"); idCounter++;
        User user2 = new(idCounter, "CodeIsMyNemesis", "pass2", "user"); idCounter++;
        User user3 = new(idCounter, "CodeIsMyName", "pass3", "admin"); idCounter++;

        users = []; //Sets the Dictionary to an empty collection.
        users.Add(user1.Id, user1);
        users.Add(user2.Id, user2);
        users.Add(user3.Id, user3);
    }

}