class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }    


// below we have a constructor that initializes the username and password to empty strings
    public User()
    {
        Username = "";
        Password = "";
        Role = "";
    }

//below we have a constructor that initializes the id, username, password and role, with the values passed as arguments. 
//This is important because it allows us to create a user object with the values we want
public User(int id, string username, string password, string role) 
{
    Id = id;
    Username = username;
    Password = password;
    Role = role;


}

//below we have a method that returns a string representation of the user object

public override string ToString()
{
    return "{id:" + Id
    + ",username:'" + Username
    + "',password:" + Password
    + ",role:" + Role + "}";
}



} 