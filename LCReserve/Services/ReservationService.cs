using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Identity.Client;

class ReservationService
{

    /* 
    Services: 
     - CheckAvailability - IsAvailable
     - Make Reserve
        - ViewReservations
        - CancelReservation
        - Exit

         */

    ReservationRepo rr;

    public ReservationService(ReservationRepo rr)
    {
        this.rr = rr;
    }





    public Reservation? CheckAvailability(Reservation r)
    {
        // this method will check if the reservation is available
        // it will return the reservation details
        if (r.Available == true)
        {
            System.Console.WriteLine("The reservation is available.");
            return r;
        }
        else
        {
            System.Console.WriteLine("The reservation is not available. Please try again.");
            return null;
        }

    }



    public Reservation? ReserveLodge(Reservation r)
    {
        if (!r.Available)
        {

            System.Console.WriteLine("The reservation is not available. Please try again.");
            return null; //resevation does not get made
        }

        r.Available = false; //the reservation is no longer available
        r.CheckOutDate = r.CheckInDate + r.NumberOfNights; //the check out date is calculated
        rr.AddReservation(r); //the reservation is updated in storage

        return rr.AddReservation(r); //the reservation is added to the list of reservations

    }

    //get reservations for a specific user - added this new method to test that we can filter by a logged in user 
    public List<Reservation> ViewReservations(User u)
    {

        List<Reservation> allReservations = rr.GetAllReservations(); //get all reservations
        List<Reservation> filteredReservations = []; //create a new list to hold the reservations that belong to the user
        foreach (Reservation r in allReservations) //iterate through all reservations
        {
            if (r.UserId == u.Id) //if the reservation belongs to the user
            {
                filteredReservations.Add(r); //add the reservation to the list of filtered reservations
            }
        }
        return filteredReservations; //return the list of filtered reservations */
    }

    //cancel a reservation




    public Reservation? CancelReservation(Reservation r)
    {
        if (r.Available)
        {
            System.Console.WriteLine("The reservation could not be found. Please try again.");
            return null;
        }
        else
        {
            r.Available = true;
            rr.CancelReservation(r);
            return r;
        }
    }

    public void Exit()
    {
        System.Console.WriteLine("Goodbye!");
    }

    // Trivial methods that simply pass the call to the repo
    public Reservation? GetReservation(int id)
    {
        return rr.GetReservation(id);
    }

}