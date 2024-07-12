
namespace Domain.Models
{
    public class Film
    {
        public Guid Id { get; set; }
        public byte[] Content { get; set; } = null!;
        public string Name { get; set; } = null!;
        public ICollection<Question> Questions { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public Image Image { get; set; } = null!;
        public Guid ImageId { get; set; }
    }
}
