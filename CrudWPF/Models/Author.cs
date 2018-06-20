using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudWPF.Models
{
    public class Author
    {

        public string AuthorId { get; set; }

        public string FirstName { get; set; }

        public string SurName { get; set; }

        public string TaxFileNumber { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}