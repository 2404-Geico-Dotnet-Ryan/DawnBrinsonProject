using System.Security.Cryptography.X509Certificates;

class ReservationRepo
{
    /* 

CRUD is the acronym for CREATE, READ, UPDATE and DELETE
Data access & management centered around our Reservation Object 
- CreateReservation
- ReadReservation
- UpdateReservation
- DeleteReservation

 */

    ReservationStorage reservationStorage = new(); // this line of code creates a new instance of the ReservationStorage class

    public Reservation? AddReservation(Reservation r)
    {
        // this method will create a new reservation
        // it will return the reservation details
        r.Id = reservationStorage.idCounter;
        reservationStorage.reservations.Add(r.Id, r);
        return r;
    }

    public Reservation? GetReservation(int id)
    {
       if (reservationStorage.reservations.ContainsKey(id))
        {
            Reservation selectedReservation = reservationStorage.reservations[id];
            return selectedReservation;
            // return reservationStorage.reservations[id];
        }
        else
        {
            Console.WriteLine("The reservation could not be found. Please try again.");
            return null;
        }
       
    }

    public List<Reservation> GetAllReservations()
    {
        return reservationStorage.reservations.Values.ToList();
    }

    public Reservation? UpdateReservation(Reservation updatedReservation)
    {
        try
        {
            reservationStorage.reservations[updatedReservation.Id] = updatedReservation;
            return updatedReservation;
        }
        catch (Exception)
        {
            Console.WriteLine("The reservation could not be updated. Please try again.");
            return null;

        }

    }

public Reservation? DeleteReservation(Reservation r)
{
    bool didRemove = reservationStorage.reservations.Remove(r.Id);

    if (didRemove)
    {
        return r;
    }
    else
    {
        Console.WriteLine("The reservation could not be deleted. Please try again.");
        return null;
    }


}
}