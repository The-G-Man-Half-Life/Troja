using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Troja.Models;
public class LoanViewModel
{
    public int LoanId { get; set; }
    public string BookTitle { get; set; }
    public string UserFullName { get; set; }
    public DateTime? DevolutionDate { get; set; }
    public int StatusValue { get; set; }
}
