namespace Domain.Dtos.UserDto
{
    public class CreateProductDto
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public float Price { get; set; }

        public bool IsPublished { get; set; }

        public string Description { get; set; }
    }
}
