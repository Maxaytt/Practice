using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel
{
    public class AnswerVm
    {
        public Guid Id;
        public string Text { get; set; }
        public bool IsTrue { get; set; }
    }
}
