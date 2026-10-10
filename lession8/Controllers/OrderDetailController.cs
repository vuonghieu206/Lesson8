
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LTWLesson08.Models;

public class OrderDetailController : Controller
{
    private readonly BookStoreContext _context;

    public OrderDetailController(BookStoreContext context)
    {
        _context = context;
    }

    // GET: ORDERDETAILS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.OrderDetails.ToListAsync());
    }

    // GET: ORDERDETAILS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var orderdetail = await _context.OrderDetails
            .FirstOrDefaultAsync(m => m.OrderDetailId == id);
        if (orderdetail == null)
        {
            return NotFound();
        }

        return View(orderdetail);
    }

    // GET: ORDERDETAILS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ORDERDETAILS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("OrderDetailId,OrderId,BookId,Quantity,Price,TotalMoney,Book,Order")] OrderDetail orderdetail)
    {
        if (ModelState.IsValid)
        {
            _context.Add(orderdetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(orderdetail);
    }

    // GET: ORDERDETAILS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var orderdetail = await _context.OrderDetails.FindAsync(id);
        if (orderdetail == null)
        {
            return NotFound();
        }
        return View(orderdetail);
    }

    // POST: ORDERDETAILS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("OrderDetailId,OrderId,BookId,Quantity,Price,TotalMoney,Book,Order")] OrderDetail orderdetail)
    {
        if (id != orderdetail.OrderDetailId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(orderdetail);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(orderdetail.OrderDetailId))
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
        return View(orderdetail);
    }

    // GET: ORDERDETAILS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var orderdetail = await _context.OrderDetails
            .FirstOrDefaultAsync(m => m.OrderDetailId == id);
        if (orderdetail == null)
        {
            return NotFound();
        }

        return View(orderdetail);
    }

    // POST: ORDERDETAILS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var orderdetail = await _context.OrderDetails.FindAsync(id);
        if (orderdetail != null)
        {
            _context.OrderDetails.Remove(orderdetail);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool OrderDetailExists(int? id)
    {
        return _context.OrderDetails.Any(e => e.OrderDetailId == id);
    }
}
