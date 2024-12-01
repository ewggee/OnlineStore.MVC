namespace OnlineStore.Contracts.Categories
{
    public sealed class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int? ParentCategoryId { get; set; }
    }
}
