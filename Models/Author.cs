using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace Troja.Models;
public class Author
{
    public int AuthorId { get; set; }

    [Required(ErrorMessage = "Author Name is required.")]
    public string AuthorName { get; set; }
    [Required(ErrorMessage = "Author Last Name is required.")]
    public string AuthorLastName { get; set; }
    // Propiedad de navegación para los libros
    public ICollection<Book>? Books { get; set; }

    // Constructor for Author 
    public Author()
    {

        // Books = new List<Book>();

    }
}