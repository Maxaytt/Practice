using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Film
    {
        public Guid Id { get; set; }
        
        public byte[] Content { get; set; }
        
        // For example video/mp4 or video/avi
        public string ContentType { get; set; }
        
        public string Name { get; set; }
        
        public ICollection<Question> Questions { get; set; }

        public Image Image { get; set; }

        public Guid ImageId { get; set; }
    }
}
