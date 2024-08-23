using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Troja.Models;
public class AuthorsBooksRelation
{
    public int Id { get; set; }

    public int AuthorId { get; set; }

    public int BookId { get; set; }
}
