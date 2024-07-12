using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Web;
namespace Domain.ViewModel
{
    public class FilmUploadVM
    {
        public IFormFile VideoFile { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Name { get; set; }
    }
}
