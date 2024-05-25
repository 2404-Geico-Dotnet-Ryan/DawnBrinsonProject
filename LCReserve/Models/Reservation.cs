using System.Numerics;

class Reservation
{
   // private static readonly int lastId = 0; //the Id property is auto-incremented by 1 each time a new reservation is created
    public int Id { get; set; } //this is the primary key
    public string LodgeName { get; set; }
    public int NumberOfGuests { get; set; }
    public int NumberOfNights { get; set; }
    public long CheckInDate { get; set; } //the format is MM/DD/YYYY
    public long CheckOutDate { get; set; }
    public bool Available { get; set; }
    public int UserId { get; set; }

    //constructors - I didn't add very many below because I wanted to keep it simple
    public Reservation()
    {
        
        LodgeName = "";
        NumberOfGuests = 0;
        NumberOfNights = 0;

    }

    public Reservation(int id, string lodgeName, int numberOfGuests, int numberOfNights, long checkInDate, long checkOutDate, bool available, int userId)
    {
        Id = id;
        LodgeName = lodgeName;
        NumberOfGuests = numberOfGuests;
        NumberOfNights = numberOfNights;
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
        Available = available;
        UserId = userId;
    }


    public override string ToString()
    {


        return "\nLodgeName: " + LodgeName + "\nNumber of Guests: " + NumberOfGuests + "\nNumber of Nights: " + NumberOfNights + "\nCheck In Date: " + CheckInDate + "\nCheck Out Date: " + CheckOutDate + "\nReservation ID: " + Id;

    }
}


