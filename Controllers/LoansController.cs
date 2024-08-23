using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Troja.Models;
using System.Linq;
using System.Threading.Tasks;
using Troja.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

public class LoansController : Controller
{
    private readonly AppDbContext _context;

    public LoansController(AppDbContext context)
    {
        _context = context;
    }


    public async Task<IActionResult> Index()
    {
        var loans = _context.Loans
            .Include(l => l.User)
            .Include(l => l.Book);
        return View(await loans.ToListAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var loan = await _context.Loans
            .Include(l => l.User)
            .Include(l => l.Book)
            .FirstOrDefaultAsync(m => m.LoanId == id);

        if (loan == null)
        {
            return NotFound();
        }

        return View(loan);
    }


    public IActionResult Create()
    {

        ViewData["Books"] = new SelectList(
            _context.Books.Where(b => b.StatusValue == "1")
                          .Select(b => new
                          {
                              BookId = b.BookId,
                              Title = b.Title
                          }).ToList(),
            "BookId",
            "Title");


        ViewData["Users"] = new SelectList(
            _context.Users.Select(u => new
            {
                UserId = u.UserId,
                FullName = $"{u.Name} {u.LastName}"
            }).ToList(),
            "UserId",
            "FullName");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("UserId,BookId,LoanDate,LimitDate,DevolutionDate,StatusValue")] Loan loan)
    {
        if (ModelState.IsValid)
        {
            var book = await _context.Books.FindAsync(loan.BookId);
            if (book != null)
            {
                book.StatusValue = "0";
                _context.Update(book);
            }

            _context.Add(loan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["Books"] = new SelectList(
            _context.Books.Where(b => b.StatusValue == "1")
                          .Select(b => new
                          {
                              BookId = b.BookId,
                              Title = b.Title
                          }).ToList(),
            "BookId",
            "Title");

        ViewData["Users"] = new SelectList(
            _context.Users.Select(u => new
            {
                UserId = u.UserId,
                FullName = $"{u.Name} {u.LastName}"
            }).ToList(),
            "UserId",
            "FullName");

        return View(loan);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var loan = await _context.Loans.FindAsync(id);
        if (loan == null)
        {
            return NotFound();
        }

        ViewBag.Users = new SelectList(await _context.Users.ToListAsync(), "UserId", "FullName");
        ViewBag.Books = new SelectList(await _context.Books.Where(b => b.StatusValue == "1").ToListAsync(), "BookId", "Title");

        return View(loan);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("LoanId,UserId,BookId,LoanDate,LimitDate,DevolutionDate,StatusValue")] Loan loan)
    {
        if (id != loan.LoanId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var existingLoan = await _context.Loans.FindAsync(id);
                if (existingLoan == null)
                {
                    return NotFound();
                }

                existingLoan.UserId = loan.UserId;
                existingLoan.BookId = loan.BookId;
                existingLoan.LoanDate = loan.LoanDate;
                existingLoan.LimitDate = loan.LimitDate;
                existingLoan.DevolutionDate = loan.DevolutionDate;
                existingLoan.StatusValue = loan.StatusValue;

                _context.Update(existingLoan);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanExists(loan.LoanId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Users = new SelectList(await _context.Users.ToListAsync(), "UserId", "FullName", loan.UserId);
        ViewBag.Books = new SelectList(await _context.Books.ToListAsync(), "BookId", "Title", loan.BookId);

        return View(loan);
    }


    public async Task<IActionResult> Delete(int id)
    {
        var loan = await _context.Loans
            .Include(l => l.User)
            .Include(l => l.Book)
            .FirstOrDefaultAsync(m => m.LoanId == id);

        if (loan == null)
        {
            return NotFound();
        }

        return View(loan);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var loan = await _context.Loans.FindAsync(id);
        _context.Loans.Remove(loan);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool LoanExists(int id)
    {
        return _context.Loans.Any(e => e.LoanId == id);
    }
    public IActionResult Release(int id)
    {
        var loan = _context.Loans
            .Include(l => l.Book)
            .FirstOrDefault(l => l.LoanId == id);

        if (loan == null)
        {
            return NotFound();
        }

        var model = new ReleaseViewModel
        {
            LoanId = loan.LoanId,
            BookTitle = loan.Book.Title,
            StatusValue = loan.StatusValue
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Release(ReleaseViewModel model)
    {
        if (ModelState.IsValid)
        {
            var loan = await _context.Loans.FindAsync(model.LoanId);
            if (loan == null)
            {
                return NotFound();
            }

            loan.StatusValue = model.StatusValue;

            if (model.StatusValue == 1)
            {
                var book = await _context.Books.FindAsync(loan.BookId);
                if (book != null)
                {
                    book.StatusValue = "1"; 
                    _context.Update(book);
                }
            }

            _context.Update(loan);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }


}
