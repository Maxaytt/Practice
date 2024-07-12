using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Film
    {
        public Guid Id { get; set; }
        public byte[] Content { get; set; } = null!;
        public string Name { get; set; } = null!;
        public ICollection<Question> Questions { get; set; }
        public Image Image { get; set; }
        public Guid ImageId { get; set; }
    }
}
