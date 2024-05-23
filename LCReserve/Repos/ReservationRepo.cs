using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Data.SqlClient;

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

    private readonly string _connectionString;
    public ReservationRepo(string connString)
    {
        _connectionString = connString;
    }
//below we will implement the CRUD methods for the ReservationRepo class

    public Reservation? AddReservation(Reservation r) //this worked with some bugs - it's duplicating the reservation
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        string sql = "INSERT INTO dbo.[Reservation] OUTPUT inserted.* VALUES (@LodgeName, @NumberofGuests, @NumberOfNights, @CheckInDate, @CheckOutDate, @Available, @UserId)";
        using SqlCommand cmd = new(sql, connection);
        cmd.Parameters.AddWithValue("@id", r.Id);
        cmd.Parameters.AddWithValue("@LodgeName", r.LodgeName);
        cmd.Parameters.AddWithValue("@NumberOfGuests", r.NumberOfGuests);
        cmd.Parameters.AddWithValue("@NumberOfNights", r.NumberOfNights);
        cmd.Parameters.AddWithValue("@CheckInDate", r.CheckInDate);
        cmd.Parameters.AddWithValue("@CheckOutDate", r.CheckOutDate);
        cmd.Parameters.AddWithValue("@Available", r.Available);
        cmd.Parameters.AddWithValue("@UserId", r.UserId);

        using SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            Reservation newReservation = BuildReservation(reader);
            return newReservation;
        }
        else
        {
            return null;
        }
 
    }


    public Reservation? GetReservation(int id)
    {
        try
        {
            //SET UP DB CONNECTION AGAIN 
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            //CREATE THE SQL STRING
            string sql = "SELECT * FROM dbo.[Reservation] WHERE Id = @Id";

            //SET UP SQL COMMAND OBJECT
            using SqlCommand cmd = new(sql, connection);
            cmd.Parameters.AddWithValue("@Id", id);

            //EXECUTE THE QUERY
           using var reader = cmd.ExecuteReader();

            //EXTRACT THE RESULTS
            if (reader.Read())
            {
                //for each iteration -> extract the results to a User object -> add to list.
                Reservation newReservation = BuildReservation(reader);
                return newReservation;
            }
            else
            {
                return null; //didn't find the reservation
            }
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
            System.Console.WriteLine(e.StackTrace);
            return null;
        }
    }

    public List<Reservation> GetAllReservations()
    {
         List<Reservation> reservations = [];

        try
        {
            //Set up DB Connection
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            //Create the SQL String
            string sql = "SELECT * FROM dbo.[Reservation]";

            //Set up SqlCommand Object
            using SqlCommand cmd = new(sql, connection);

            //Execute the Query
            using var reader = cmd.ExecuteReader(); //flexing options here with the use of var.

            //Extract the Results
            while (reader.Read())
            {
                //for each iteration -> extract the results to a User object -> add to list.
                Reservation newReservation = BuildReservation(reader);

                //don't return! Instead Add to List!
                reservations.Add(newReservation);
            }

            return reservations;
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
            System.Console.WriteLine(e.StackTrace);
            return null;
        }

    }

    public Reservation? UpdateReservation(Reservation updatedReservation)
    {
        try
        {
            //SET UP DB CONNECTION AGAIN 
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            //CREATE THE SQL STRING
            string sql = "UPDATE dbo.[Reservation] SET LodgeName = @LodgeName, NumberOfGuests = @NumberOfGuests, NumberOfNights = @NumberOfNights, CheckInDate = @CheckInDate, CheckOutDate = @CheckOutDate, Available = @Available, UserId = @UserId WHERE Id = @Id";

            //SET UP SQL COMMAND OBJECT
            using SqlCommand cmd = new(sql, connection);
            cmd.Parameters.AddWithValue("@Id", updatedReservation.Id);
            cmd.Parameters.AddWithValue("@LodgeName", updatedReservation.LodgeName);
            cmd.Parameters.AddWithValue("@NumberOfGuests", updatedReservation.NumberOfGuests);
            cmd.Parameters.AddWithValue("@NumberOfNights", updatedReservation.NumberOfNights);
            cmd.Parameters.AddWithValue("@CheckInDate", updatedReservation.CheckInDate);
            cmd.Parameters.AddWithValue("@CheckOutDate", updatedReservation.CheckOutDate);
            cmd.Parameters.AddWithValue("@Available", updatedReservation.Available);
            cmd.Parameters.AddWithValue("@UserId", updatedReservation.UserId);

            //EXECUTE THE QUERY
            using SqlDataReader reader = cmd.ExecuteReader(); //lets see if this will work like "using var reader = cmd.ExecuteReader(); does

            //EXTRACT THE RESULTS
            if (reader.Read())
            {
                Reservation newReservation = BuildReservation(reader);
                return newReservation;
            }
            else
            {
                return null;
            }
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
            System.Console.WriteLine(e.StackTrace);
            return null;
        }
    }

    public Reservation? CancelReservation(Reservation r) 
    {
        try
        {
            //SET UP DB CONNECTION AGAIN 
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            //CREATE THE SQL STRING
            string sql = "DELETE FROM dbo.[Reservation] OUTPUT deleted. * WHERE Id = @Id";

            //SET UP SQL COMMAND OBJECT
            using SqlCommand cmd = new(sql, connection);
            cmd.Parameters.AddWithValue("@Id", r.Id);

            //EXECUTE THE QUERY
            using var reader = cmd.ExecuteReader();

            //EXTRACT THE RESULTS
            if (reader.Read())
            {
                Reservation newReservation = BuildReservation(reader);
                return newReservation;
            }
            else
            {
                return null;
            }
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
            System.Console.WriteLine(e.StackTrace);
            return null;
        }
    }

    //Helper Method - the method below will be used to build a reservation object from the data in the database

    private static Reservation BuildReservation(SqlDataReader reader)
    {
       Reservation newReservation = new Reservation();
        newReservation.Id = (int)reader["Id"];
        newReservation.LodgeName = (string)reader["LodgeName"];
        newReservation.NumberOfGuests = (int)reader["NumberOfGuests"];
        newReservation.NumberOfNights = (int)reader["NumberOfNights"];
        newReservation.CheckInDate = (long)reader["CheckInDate"];
        newReservation.CheckOutDate = (long)reader["CheckOutDate"];
        newReservation.Available = (bool)reader["Available"];
        newReservation.UserId = (int)reader["UserId"];

        return newReservation;
       

    }



}