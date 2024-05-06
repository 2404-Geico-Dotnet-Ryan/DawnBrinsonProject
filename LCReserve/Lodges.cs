class Lodges // I don't know if this is a necessary class??? I think it is, but I'm not sure. 
//I'm going to leave it here for now. I can always delete it later if it's not needed.
{ 
    public string Name { get; set; }
    public string Availability { get; set; }
    public string CheckIn { get; set; }
    public string CheckOut { get; set; }
    public string MinimumStay { get; set; }
    public string MaximumStay { get; set; }

    public Lodges()
    {
        Name = "Unknown";
        Availability = "Unknown";
        CheckIn = "Unknown";
        CheckOut = "Unknown";
        MinimumStay = "Unknown";
        MaximumStay = "Unknown";
    }

    public Lodges(string name, string availability, string checkIn, string checkOut, string minimumStay, string maximumStay)
    {
        Name = name;
        Availability = availability;
        CheckIn = checkIn;
        CheckOut = checkOut;
        MinimumStay = minimumStay;
        MaximumStay = maximumStay;
    }

    public override string ToString()
    {
        return $"Name: {Name}, Availability: {Availability}, CheckIn: {CheckIn}, CheckOut: {CheckOut}, MinimumStay: {MinimumStay}, MaximumStay: {MaximumStay}";
    }
    
   
}