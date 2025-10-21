namespace TooliRent.Models
{
    public class Enums
    {
        public static class UserRoles
        {
            public const string Admin = "Admin";
            public const string Member = "Member";
        }


        public static class OrderStatus
        {
            public const string Pending = "Pending";
            public const string CheckedOut = "CheckedOut";
            public const string Returned = "Returned";
            public const string Cancelled = "Cancelled";
        }


        //public static class ReservationStatus
        //{
        //    public const string Active = "Active";
        //    public const string Cancelled = "Cancelled";
        //}
    }
}

