using System.ComponentModel.DataAnnotations;

namespace TooliRent.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // Admin, Member

        // Navigation properties
        //public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public ICollection<OrderDeatils> OrderDetails { get; set; } = new List<OrderDeatils>();
    }
}
