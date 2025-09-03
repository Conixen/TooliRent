namespace TooliRent.Models
{
    public class Enums
    {
        public static class UserRoles
        {
            public const string Admin = "Admin";
            public const string Member = "Member";
        }

        public static class ToolStatus
        {
            public const string Available = "Available";
            public const string Reserved = "Reserved";
            public const string CheckedOut = "Not Avaiable";
        }

        public static class ReservationStatus
        {
            public const string Available = "Available";
            public const string Reserved = "Reserved";
            public const string CheckedOut = "Not Avaiable";
            public const string Cancelled = "Cancelled";

        }
        public static class OrderStatus
        {
            public const string Pending = "Pending";
            public const string Completed = "Completed";
            public const string Cancelled = "Cancelled";
            public const string Late = "Late";
        }


    }
}

