using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Troja.Models;
public class Book
{
    public int BookId { get; set; } // Clave primaria
    public string Title { get; set; }
    public int AuthorId { get; set; } // Clave foránea
    public string LiteraryGenre { get; set; }
    public DateOnly PublicationDate { get; set; }
    public string ISBN { get; set; }
    public string StatusValue { get; set; }
    
    // Propiedades de navegación
    public Author? Author { get; set; }
    public required ICollection<Loan>? Loans { get; set; }

}
