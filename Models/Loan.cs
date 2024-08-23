using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Troja.Models;
public class Loan
{
    public int LoanId { get; set; } // Clave primaria
    public int UserId { get; set; }
    public int BookId { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime LimitDate { get; set; }
    public DateTime? DevolutionDate { get; set; }

    // Propiedades de navegación
    public User User { get; set; }
    public Book Book { get; set; }

    public int StatusValue { get; set; } // 1: Liberado, 2: Devolución retrasada, 3: Pendiente de devolución

    public Loan()
    { }
}
