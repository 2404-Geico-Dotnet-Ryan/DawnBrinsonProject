using System.Collections;
using System.Runtime.ExceptionServices;

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


    private static object r;

    static void Main(string[] args)
    {
        LoginMenu(); // this line of code calls the LoginMenu method
        UserMenu(); // this line of code calls the UserMenu method that list the options for the user after they have signed in or registered 

    }


    private static void LoginMenu()
    {
        System.Console.WriteLine("Welcome to the Little Creek Preserve Reservations!");
        bool keepGoing = true;
        while (keepGoing)
        {
            System.Console.WriteLine("Please select an option:");
            System.Console.WriteLine("===============================");
            System.Console.WriteLine("1. Register");
            System.Console.WriteLine("2. Login");
            System.Console.WriteLine("3. Exit");
            System.Console.WriteLine("===============================");

            int input = int.Parse(Console.ReadLine() ?? "0");
            //Same Validation method copied over
            input = ValidateCmd(input, 4);

            //Extracted to method - uses switch case to determine what to do next.
            keepGoing = LoginMenuDecideNextOption(input);
        }

    }

    private static bool LoginMenuDecideNextOption(int input) // this method will determine the next option
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

    private static void Register() //If the user doens't have an existing account to login they can register for a new account
    {
        System.Console.WriteLine("Please enter a username:");
        string username = System.Console.ReadLine() ?? "";
        System.Console.WriteLine("Please enter your password:");
        string password = System.Console.ReadLine() ?? "";
        User u = new() { Username = username, Password = password, Role = "user" };
        User? registeredUser = us.RegisterUser(u);
        if (registeredUser != null)
        {
            System.Console.WriteLine("You have successfully registered!");
            //currentUser = registeredUser;
            UserMenu();
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
        User? loggedInUser = us.LoginUser(username, password); //
        if (loggedInUser != null)
        {
            System.Console.WriteLine("You have successfully logged in!");

            UserMenu();
        }
        else
        {
            System.Console.WriteLine("===============================");
            System.Console.WriteLine("Login failed. Please try again.");
            System.Console.WriteLine("===============================");
        }
    }



    private static void UserMenu() // this method will list the options for the user after they have signed in or registered
    {
        bool keepGoing = true; // this will keep the program running until the user decides to exit
        while (keepGoing)
        {
            System.Console.WriteLine("Please select an option:");
            System.Console.WriteLine("1. Reserve Lodge ");
            System.Console.WriteLine("2. View Reservations");
            System.Console.WriteLine("3. Cancel Reservation");
            System.Console.WriteLine("4. Exit");

            int input = int.Parse(System.Console.ReadLine() ?? "0"); // this will take the user's input and convert it to an integer
            input = ValidateCmd(input, 5);
            keepGoing = UserMenuNextOption(input);
        }
    }

    //next we will use a swith statement to determine the next option 

    private static bool UserMenuNextOption(int input) // this method will determine the next option
    {
        switch (input)
        {
            case 1:
                {
                    ReserveLodge();
                    break;
                }
            case 2:
                {
                    ViewReservations();
                    break;
                }
            case 3:
                {
                    CancelReservation();
                    break;
                }
            case 4:
                {
                    Exit();
                    break;
                }
            case 0:
            default:
                {
                    return false; //If option 0 ir anything else - set keepGoing to false
                }

        }
        return true;
    }



    private static void ReserveLodge()
    {

        //this method will allow the user to reserve a lodge
        //use service method now
        //first we will ask the user to enter the name of the lodge they want to reserve
        System.Console.WriteLine("Please enter the name of the lodge you would like to reserve:");
        string lodgeName = System.Console.ReadLine() ?? "";
        //next we will ask the user to enter the number of guests that will be staying in the lodge
        System.Console.WriteLine("Please enter the number of guests that will be staying in the lodge:");
        int numberOfGuests = int.Parse(System.Console.ReadLine() ?? "0");
        //next we will ask the user to enter the Date they will be checking in
        System.Console.WriteLine("Please enter the Date you will be checking in (YYYY-MM-DD): ");
        DateTime checkInDate = DateTime.Parse(System.Console.ReadLine());
        //next we will ask the user to enter the number of nights they will be staying in the lodge
        System.Console.WriteLine("Please enter the number of nights you will be staying in the lodge:");
        int numberOfNights = int.Parse(System.Console.ReadLine() ?? "0");
        //then we will Check if the reservation is available
        Reservation r = new() { LodgeName = lodgeName, NumberOfGuests = numberOfGuests, CheckInDate = checkInDate, NumberOfNights = numberOfNights, Available = true };
        Reservation? reservedLodge = rs.ReserveLodge(r);

        if (reservedLodge != null)
        {
            System.Console.WriteLine("You have successfully reserved a lodge!");
            //we will return the reservation details here 
            System.Console.WriteLine("Here are your reservation details: " + reservedLodge);
        }
        else
        {
            System.Console.WriteLine("Reservation failed. Please try again.");
        }


    }


    private static void ViewReservations()
    {
        //now we will let the user view a reservation they made using their username
        System.Console.WriteLine("Please enter your username to view your reservation:");
        string username = System.Console.ReadLine() ?? "";
        User? user = us.GetUser(username);
        if (user != null)
        {
            System.Console.WriteLine("Here are your reservations:");
            List<Reservation> reservations = rs.ViewReservations();
            foreach (var r in reservations)
            {
                System.Console.WriteLine(r);
            }
        }
        else
        {
            System.Console.WriteLine("Reservation not found. Please try again.");
        }


    }

    private static void CancelReservation()
    {
        //this method will allow the user to cancel their reservation
        //we will cancel the reservation using the reservation ID
        System.Console.WriteLine("Please enter the reservation ID you would like to cancel:");
        int reservationID = int.Parse(System.Console.ReadLine() ?? "0");
        Reservation? r = rs.GetReservation(reservationID);
        if (r != null)
        {
            Reservation? cancelledReservation = rs.CancelReservation(r);
            if (cancelledReservation != null)
            {
                System.Console.WriteLine("Your " + cancelledReservation + " has successfully cancelled your reservation.");
            }
            else
            {
                System.Console.WriteLine("Cancellation failed. Please try again.");
            }
        }
        else
        {
            System.Console.WriteLine("Reservation not found. Please try again.");
        }


    }

    private static void Exit()
    {
        //this will allow the user to exit the program
        System.Console.WriteLine("Thanks for visiting Little Creek Reserve! Goodbye!");
    }

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