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

    public Reservation()
    {
        LodgeName = "";
        NumberOfGuests = 0;
        NumberOfNights = 0;
        ReservationID = 0;
        CheckInDate = DateTime.Now;
        CheckOutDate = DateTime.Now;
    }

    public Reservation(int id, string lodgeName, int numberOfGuests, int numberOfNights, int reservationID, DateTime checkInDate, DateTime checkOutDate)
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


