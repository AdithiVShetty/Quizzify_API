using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzify_API.Models
{
    public class SelectedQuestionDTO
    {
        public int id { get; set; }
        public int weightage { get; set; }
        public string category { get; set; }
    }
}
