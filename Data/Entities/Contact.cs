namespace Pzpp.Data.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string FavoriteTymbark { get; set; }

        // new
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
