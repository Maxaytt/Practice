using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Image
    {
        public Guid Id { get; set; }

        public byte[] Content { get; set; }

        // For example image/jpg or image/png
        public string ContentType { get; set; }

        //Caption which is displayed when image cannot be loaded
        public string Caption { get; set; } 

        public Film Film { get; set; }

     
    }
}
