namespace TooliRent.DTO_s.CategoryDTOs
{
    public class CategorySelectDTO // admin snd user
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ToolCount { get; set; }
        public bool HasAvailableTools { get; set; }
    }
}
