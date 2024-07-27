using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel
{
    public class QuestionVm
    {
        public Guid FilmId { get; set; }
        public string Text { get; set; }
        public List<AnswerVm> Answers { get; set; } = new List<AnswerVm>();
        public Guid Id { get; set; }
    }
}
