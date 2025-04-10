namespace RazorDb.Services
{
    public class Secret
    {
        //Denne klasse, skal ikke deles på git-hub, eftersom dennne vil give adgang til databasen.
        private static string _connectionString = 

        public static string ConnectionString
        {
            get { return _connectionString; }

        }
        private static string quereyString = "SELECT Room_No,Hotel_No,Types,Price from Room";
    }

}

