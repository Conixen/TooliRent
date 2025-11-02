namespace TooliRent.DTO_s.OrderDetailsDTOs
{
    public class OrderDetailsDTO
    {
        public int Id { get; set; }
        public DateTime Date2Hire { get; set; }
        public DateTime Date2Return { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal? LateFee { get; set; }

        // User info
        public string UserName { get; set; }     

        // Tool info
        public string ToolName { get; set; }      
        public string ToolBrand { get; set; }    

        
        public DateTime? CheckedOutAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
    }
}

