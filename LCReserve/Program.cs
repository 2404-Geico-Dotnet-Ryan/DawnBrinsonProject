using System.Collections;
using System.Data.Common;
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

    static ReservationService? rs;
    static UserService? us;
    static User? currentUser = null; //set to 



    static void Main(string[] args)
    {

        string path = @"C:\Users\A141409\LCReserve-db.txt"; // this is the path to the file where the data will be stored - using the @ sign reads the string as is
        string connectionString = File.ReadAllText(path); //tested and this is good to go - reads the file and stores the data in the variable connectionString

        UserRepo ur = new(connectionString); // this line of code creates a new instance of the UserRepo class and passes the connectionString variable as an argument
        us = new(ur); // this line of code creates a new instance of the UserService class and passes the ur variable as an argument

        ReservationRepo rr = new(connectionString); //done
        rs = new(rr); //done

        System.Console.WriteLine();
        System.Console.WriteLine("====================================================");
        System.Console.WriteLine("Welcome to the Little Creek Preserve Reservations!");
        System.Console.WriteLine("====================================================");
        System.Console.WriteLine();

        LoginMenu(); // this line of code calls the LoginMenu method

        UserMenu(); // this line of code calls the UserMenu method that list the options for the user after they have signed in or registered

    }
    private static void LoginMenu()
    {

        bool keepGoing = true;
        while (keepGoing)
        {
            System.Console.WriteLine("Please select an option:");
            System.Console.WriteLine("===============================");
            System.Console.WriteLine("[1] Register"); //tested worked & logged to db 
            System.Console.WriteLine("[2] Login"); //tested - worked
            System.Console.WriteLine("[3] Exit"); //tested - worked
            System.Console.WriteLine("===============================");

            int input = int.Parse(Console.ReadLine() ?? "0");
            //Same Validation method copied over
            input = ValidateCmd(input, 3);

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
                    Exit();
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
        System.Console.WriteLine("Please create a username (pick something cool or I will judge you):");
        string username = System.Console.ReadLine() ?? "";
        System.Console.WriteLine("Please enter a password:");
        string password = System.Console.ReadLine() ?? "";
        User? newUser = new(0, username, password, "user");
        newUser = us.RegisterUser(newUser); //should return the new user
        if (newUser != null)
        {
            System.Console.WriteLine("You have successfully registered!");
            //UserMenu();
        }
        else
        {
            System.Console.WriteLine("Registration failed. Please try again.");
        }

    }

    private static void Login()
    {
        while (currentUser == null) //this will keep the program running until the user logs in (or exits
        {
            System.Console.WriteLine("Please enter your username:");
            string username = System.Console.ReadLine() ?? "";
            System.Console.WriteLine("Please enter your password:");
            string password = System.Console.ReadLine() ?? "";
            currentUser = us.LoginUser(username, password);

            if (currentUser != null)
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
    }

    private static void UserMenu() // this method will list the options for the user after they have signed in or registered
    {
        bool keepGoing = true; // this will keep the program running until the user decides to exit
        while (keepGoing)
        {

            System.Console.WriteLine("==========================");
            System.Console.WriteLine("Please select an option:");
            System.Console.WriteLine("==========================");
            System.Console.WriteLine("[1] Reserve Lodge ");
            System.Console.WriteLine("[2] View Reservations");
            System.Console.WriteLine("[3] Cancel Reservation");
            System.Console.WriteLine("[4] Exit");

            int input = int.Parse(System.Console.ReadLine() ?? "0"); // this will take the user's input and convert it to an integer
            input = ValidateCmd(input, 4);
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



    private static void ReserveLodge() //Works! Kinda buggy but works - need to fix the date format
    {

        //here we will make a new reservation
        System.Console.WriteLine("Please enter the name of the lodge you would like to reserve:");
        string lodgeName = System.Console.ReadLine() ?? "";
        System.Console.WriteLine("Please enter the number of guests:");
        int numberOfGuests = int.Parse(System.Console.ReadLine() ?? "0");
        System.Console.WriteLine("Please enter the number of nights you would like to stay:");
        int numberOfNights = int.Parse(System.Console.ReadLine() ?? "0");
        System.Console.WriteLine("Please enter the check-in date (MMDDYYYY):");
        long checkInDate = long.Parse(System.Console.ReadLine() ?? "");
        //we will calculate the checkout date by adding the number of nights to the check in date - when I changed my DateTime to long I wasn't getting the cool calculations
        long checkOutDate = checkInDate + numberOfNights;

        Reservation? newReservation = new(0, lodgeName, numberOfGuests, numberOfNights, checkInDate, checkOutDate, true, currentUser.Id);
        newReservation = rs.ReserveLodge(newReservation);
        if (newReservation != null)
        {
            System.Console.WriteLine("Your reservation was successful! Here are the details: " + newReservation);
        }
        else
        {
            System.Console.WriteLine("Reservation failed. Please try again.");
        }

    }

    private static void ViewReservations() //Works - tested!
    {
        //now we are going to use services to view the reservations using the current user

        List<Reservation> reservations = rs.ViewReservations(currentUser);
        if (reservations.Count == 0)
        {
            System.Console.WriteLine("You have no reservations.");
        }
        else

        {
            System.Console.WriteLine("Here are your reservations: ");
            foreach (var r in reservations)
            {

                System.Console.WriteLine(r);
            }
        }
        /*  foreach (var r in reservations)
         {
             System.Console.WriteLine(r);
         }

  */
    }


    private static void CancelReservation() //this works and returne
    {
        //this method will find the user's reservation by the reservation ID and cancel it
        System.Console.WriteLine("Please enter the reservation ID you would like to cancel:");
        int reservationID = int.Parse(System.Console.ReadLine() ?? "0");
        Reservation? r = rs.GetReservation(reservationID); //this is retrieving the reservation by the reservation ID
        if (r != null)
        {
            Reservation? cancelledReservation = rs.CancelReservation(r);
            if (cancelledReservation != null)
            {
                System.Console.WriteLine("The following reservation:  " + cancelledReservation + " has successfully been cancelled.");
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

    private static void Exit() //this works BUT it is bringing the main menu up after the user exits - fix if have time
    {
        //this will allow the user to exit the program
        System.Console.WriteLine("Thanks for visiting Little Creek Reserve! Goodbye!");
        LoginMenu(); //after exit just taking them back to the login menu
    }

    //some cool validation method below - copied it 
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