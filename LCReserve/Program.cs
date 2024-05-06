namespace LCReserve;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Little Creek Reserve!");
        System.Console.WriteLine("Please enter your user name: ");
        System.Console.ReadLine();
        System.Console.WriteLine("Please enter your password: ");
        System.Console.ReadLine();
        PrintMenu();


    
        // need some way to collect a user name and a password from the user
        // need to check if the user name and password are correct
        // if they are correct, then we can proceed to the next step
        // if they are not correct, then we need to ask the user to try again

        // need to display a menu to the user

       
    }

    public static void PrintMenu()
    { 
        System.Console.WriteLine("1. Reserve a Lodge");
        System.Console.WriteLine("2. View Reservations");
        System.Console.WriteLine("3. Cancel Reservation");
        System.Console.WriteLine("4. Exit");
    }
    
}
