using System.ComponentModel.DataAnnotations;

namespace TooliRent.Models
{
    public class User
    {
        public int Id { get; set; }
        //public string Username { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(75)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string PasswordHash { get; set; }
        [Required]
        [MaxLength(20)]
        public string Role { get; set; } // Admin or member

        // navigation property
        public ICollection<Reservation> Reservations { get; set; }  // one user can have many resorvations
        public ICollection<OrderDeatils> OrderDetails { get; set; }  // One user can have many orders
    }
}
