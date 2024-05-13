using System.Security.Cryptography.X509Certificates;

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

    ReservationRepo rr = new(); // this line of code creates a new instance of the ReservationRepo class

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

    public Reservation? MakeReservation(Reservation r)
    {
        if (!r.Available)
        {

            System.Console.WriteLine("The reservation is not available. Please try again.");
            return null; //resevation does not get made
        }

            r.Available = false; //the reservation is no longer available

            r.CheckOutDate = r.CheckInDate.AddDays(r.NumberOfNights); //the check out date is set to the check in date plus the number of nights

            rr.UpdateReservation(r); //the reservation is updated in storage

            return r;

        }
        
        public List<Reservation> ViewReservations()
        {
            return rr.GetAllReservations();
    
     
        }

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
                rr.UpdateReservation(r);
                System.Console.WriteLine("The reservation has been cancelled.");
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

