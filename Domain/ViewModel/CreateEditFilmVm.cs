using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Web;
namespace Domain.ViewModel
{
    public class CreateEditFilmVm
    {
        public Guid Id { get; set; } = new Guid();
        public IFormFile VideoFile { get; set; } = null!;
        public IFormFile ImageFile { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
