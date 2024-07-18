using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel
{
    public class FilmVm
    {
        public Guid FilmId { get; set; }
        public Guid? ImageId { get; set; }
        public string ImageContentType { get; set; }

        public string Name {  get; set; }
        public string Caption { get; set; }
    }
}
