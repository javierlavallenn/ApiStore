namespace Domain.Models.ProductDto
{
    public class UpdateProductRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public bool IsPublished { get; set; }
    }
}
