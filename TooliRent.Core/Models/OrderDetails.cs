using System.ComponentModel.DataAnnotations;

namespace TooliRent.Models
{
    public class OrderDeatils
    {
        public int Id { get; set; }
        public DateTime Date2Hire { get; set; }
        public DateTime Date2Return { get; set; }
        
        [MaxLength(50)]
        public string Status { get; set; } 
        
        public decimal TotalPrice { get; set; }
        public decimal LateFee { get; set; }

        public DateTime? CheckedOutAt { get; set; } 
        public DateTime? ReturnedAt { get; set; }   

        // foreign key
        public int UserId { get; set; }
        public int ToolId { get; set; }
        public int ReservationId { get; set; }

        // navigation property
        public User User { get; set; }      // One user can have many orders
        public Tool Tool { get; set; }    // one reservation can have many tools
        public Reservation? Reservation { get; set; }   // one user can have many resorvations

    }
}
