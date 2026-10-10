
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LTWLesson08.Models;

public class OrderBookController : Controller
{
    private readonly BookStoreContext _context;

    public OrderBookController(BookStoreContext context)
    {
        _context = context;
    }

    // GET: ORDERBOOKS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.OrderBooks.ToListAsync());
    }

    // GET: ORDERBOOKS/Details/5
    public async Task<IActionResult> Details(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var orderbook = await _context.OrderBooks
            .FirstOrDefaultAsync(m => m.OrderId == id);
        if (orderbook == null)
        {
            return NotFound();
        }

        return View(orderbook);
    }

    // GET: ORDERBOOKS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ORDERBOOKS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("OrderId,OrderDate,AccountId,ReceiveAddress,ReceivePhone,OrderReceive,Note,Status,Account,OrderDetails")] OrderBook orderbook)
    {
        if (ModelState.IsValid)
        {
            _context.Add(orderbook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(orderbook);
    }

    // GET: ORDERBOOKS/Edit/5
    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var orderbook = await _context.OrderBooks.FindAsync(id);
        if (orderbook == null)
        {
            return NotFound();
        }
        return View(orderbook);
    }

    // POST: ORDERBOOKS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string? id, [Bind("OrderId,OrderDate,AccountId,ReceiveAddress,ReceivePhone,OrderReceive,Note,Status,Account,OrderDetails")] OrderBook orderbook)
    {
        if (id != orderbook.OrderId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(orderbook);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderBookExists(orderbook.OrderId))
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
        return View(orderbook);
    }

    // GET: ORDERBOOKS/Delete/5
    public async Task<IActionResult> Delete(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var orderbook = await _context.OrderBooks
            .FirstOrDefaultAsync(m => m.OrderId == id);
        if (orderbook == null)
        {
            return NotFound();
        }

        return View(orderbook);
    }

    // POST: ORDERBOOKS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string? id)
    {
        var orderbook = await _context.OrderBooks.FindAsync(id);
        if (orderbook != null)
        {
            _context.OrderBooks.Remove(orderbook);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool OrderBookExists(string? id)
    {
        return _context.OrderBooks.Any(e => e.OrderId == id);
    }
}
