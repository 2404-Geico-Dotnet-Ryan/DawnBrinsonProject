using System.Security.Cryptography.X509Certificates;

class ReservationStorage
{
    /*
    This Entire Setup iS TEMPORARY!
    We don't know how to work with Databases yet 
    - by extension - communicate with them

    SO we are going to build some devices for Storing Reservations.
    BUT everything is sadly lost everytime the application shuts down.
    */

public Dictionary<int, Reservation> reservations = new Dictionary<int, Reservation>();

public int idCounter = 1; 

//making this constructor gives some pre-loaded reservations to work with.
public ReservationStorage()
{
    Reservation r1 = new(idCounter, "The Big Lodge", 2, 3, 1, DateTime.Now, DateTime.Now); idCounter++;
    Reservation r2 = new(idCounter, "The Little Lodge", 4, 2, 2, DateTime.Now, DateTime.Now); idCounter++;
    Reservation r3 = new(idCounter, "Pond Cabin", 6, 1, 3, DateTime.Now, DateTime.Now); idCounter++;

    reservations = []; //Sets the Dictionary to an empty collection.

    reservations.Add(r1.Id, r1);
    reservations.Add(r2.Id, r2);
    reservations.Add(r3.Id, r3);
}

}