using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Image
    {
        Guid Id { get; set; }
        public byte[] Content { get; set; }
   
        //Caption which is displayed when image cannot be loaded
        public string Caption { get; set; } 

        public ICollection<Film> Films { get; set; }
     
    }
}
