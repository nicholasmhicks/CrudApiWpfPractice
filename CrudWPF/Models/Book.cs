using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudWPF.Models
{
    public class Book
    {
        public string ISBN { get; set; }

        public string Title { get; set; }

        public int YearPublished { get; set; }

        public string StudentId { get; set; }
        public string AuthorId { get; set; }

        public virtual Author Author { get; set; }

        public virtual Student Student { get; set; }
    }
}
