using System.Collections;

namespace LCReserve;

class Program
{

    /* 
    This program will allow users to reserve their desired lodge in a private preserve community. 
    
    The program will allow users to:
    1. Reserve a Lodge
    2. View Reservations
    3. Cancel Reservation
    4. Exit

     */

    static ReservationService rs = new();
     static UserService us = new();
    static UserStorage? currentUser = null;
    static ReservationStorage? currentReservation = null;
   


    static void Main(string[] args)
    {
        MainMenu(); // this line of code calls the MainMenu method

    }


    private static void MainMenu()
    {
        System.Console.WriteLine("Welcome to the Little Creek Preserve Reservations!");
        bool keepGoing = true;
        while (keepGoing)
        {
            System.Console.WriteLine("Please select an option:");
            System.Console.WriteLine("1. Register");
            System.Console.WriteLine("2. Login");
            System.Console.WriteLine("3. Exit");

            int input = int.Parse(System.Console.ReadLine() ?? "0");
            input = ValidateCmd(input, 3);
            keepGoing = DecideNextOption(input);
        }

    }

    private static bool DecideNextOption(int input) // this method will determine the next option
    {
        switch (input)
        {
            case 1:
                {
                    Register();
                    break;
                }
            case 2:
                {
                    Login();
                    break;
                }

            case 3:
                {
                    System.Console.WriteLine("Goodbye!"); 
                    break;
                }
            case 0:
            default:
                {
                    return false; //
                }

        }
        return true;
    }

    private static void Register()
    {
        System.Console.WriteLine("Please enter your username:");
        string username = System.Console.ReadLine() ?? "";
        System.Console.WriteLine("Please enter your password:");
        string password = System.Console.ReadLine() ?? "";
        User u = new() { Username = username, Password = password, Role = "user" };
        User? registeredUser = us.RegisterUser(u);
        if (registeredUser != null)
        {
            System.Console.WriteLine("You have successfully registered!");
           // currentUser = registeredUser;
          // UserMenu();
        }
        else
        {
            System.Console.WriteLine("Registration failed. Please try again.");
        }

    }

    private static void Login()
    {
        System.Console.WriteLine("Please enter your username:");
        string username = System.Console.ReadLine() ?? "";
        System.Console.WriteLine("Please enter your password:");
        string password = System.Console.ReadLine() ?? "";
        User? loggedInUser = us.LoginUser(username, password);
        if (loggedInUser != null)
        {
            System.Console.WriteLine("You have successfully logged in!");
           
            UserMenu();
        }
        else
        {
            System.Console.WriteLine("Login failed. Please try again.");
        }
    }

    private static void UserMenu()
    {
        bool keepGoing = true;
        while (keepGoing)
        {
            System.Console.WriteLine("Please select an option:");
            System.Console.WriteLine("1. Reserve a Lodge");
            System.Console.WriteLine("2. View Reservations");
            System.Console.WriteLine("3. Cancel Reservation");
            System.Console.WriteLine("4. Exit");

            int input = int.Parse(System.Console.ReadLine() ?? "0");
            input = ValidateCmd(input, 4);
            keepGoing = DecideNextOption(input);
        }
    }

private static void ReserveLodge()
{
    //this method will allow the user to reserve a lodge
}

private static void ViewReservations()
{
//want to let the user view their reservations
}

private static void CancelReservation()
{
 //this method will allow the user to cancel their reservation
}

private static void Exit()
{
//this method will allow the user to exit the program 
}


    //Generic Command Input Validator - assume 1-maxOption are the number of options. and 0 is an option to quit.
    private static int ValidateCmd(int cmd, int maxOption)
    {
        while (cmd < 0 || cmd > maxOption)
        {
            System.Console.WriteLine("Invalid Command - Please Enter a command 1-" + maxOption + "; or 0 to Quit");
            cmd = int.Parse(Console.ReadLine() ?? "0");
        }

        //if input was already valid - it skips the if statement and just returns the value.
        return cmd;
}

}
