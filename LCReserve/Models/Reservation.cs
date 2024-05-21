class Reservation
{
public int Id { get; set; }
    public string LodgeName { get; set; }
    public int NumberOfGuests { get; set; }
    public int NumberOfNights { get; set; }
    public int ReservationID { get; set; }
    public bool Available { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public string Date { get; internal set; }
    public int? UserId { get; internal set; }

    public Reservation()
    {
        LodgeName = "";
        NumberOfGuests = 0;
        NumberOfNights = 0;
        ReservationID = 0;
        CheckInDate = DateTime.Now;
        CheckOutDate = DateTime.Now;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Reservation(int id, string lodgeName, int numberOfGuests, int numberOfNights, int reservationID, DateTime checkInDate, DateTime checkOutDate)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        Id = id;
        LodgeName = lodgeName;
        NumberOfGuests = numberOfGuests;
        NumberOfNights = numberOfNights;
        ReservationID = reservationID;
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
    }

    public override string ToString()
    {
        return "{id:" + Id
        + ",lodgeName:'" + LodgeName
        + "',numberOfGuests:" + NumberOfGuests
        + ",numberOfNights:" + NumberOfNights
        + ",reservationID:" + ReservationID
        + ",checkInDate:" + CheckInDate
        + ",checkOutDate:" + CheckOutDate + NumberOfNights +"}";
    }
}


