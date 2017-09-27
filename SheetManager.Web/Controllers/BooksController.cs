using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SheetManager.Web.Data;
using SheetManager.Web.Models;

namespace SheetManager.Web.Controllers
{
	public class BooksController : Controller
	{
		private readonly SheetsContext _context;

		public BooksController(SheetsContext context) {
			_context = context;
		}

		// GET: Books
		public async Task<IActionResult> Index(string sortOrder) {
			const string nameDesc = "name_desc";
			const string genreDesc = "genre_desc";
			const string genreAsc = "genre";
			ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? nameDesc : string.Empty;
			ViewData["GenreSortParam"] = sortOrder == genreAsc ? genreDesc : genreAsc;
			var books = _context.Books.Select(b => b);
			switch (sortOrder) {
				case nameDesc:
					books = books.OrderByDescending(b => b.Name);
					break;
				case genreAsc:
					books = books.OrderBy(b => b.Genre);
					break;
				case genreDesc:
					books = books.OrderByDescending(b => b.Genre);
					break;
				default:
					books = books.OrderBy(b => b.Name);
					break;
			}

			return View(await books.AsNoTracking().ToListAsync());
		}

		// GET: Books/Details/5
		public async Task<IActionResult> Details(int? id) {
			if (!id.HasValue)
				return NotFound();

			var book = await _context.Books
				.Include(b => b.Songs)
				.AsNoTracking()
				.SingleOrDefaultAsync(m => m.ID == id);

			if (book == null)
				return NotFound();

			return View(book);
		}

		// GET: Books/Create
		public IActionResult Create() {
			return View();
		}

		// POST: Books/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind(Book.PropertiesToBind)] Book book) {
			try {
				if (ModelState.IsValid) {
					_context.Add(book);
					await _context.SaveChangesAsync();
					return RedirectToAction(nameof(Index));
				}
			} catch (DbUpdateException e) {
				ModelState.AddModelError("", $"Unable to save changes ({e.Message}). " +
				                             "Please try again and if the problem persists, " +
				                             "see your system administrator.");
			}

			return View(book);
		}

		// GET: Books/Edit/5
		public async Task<IActionResult> Edit(int? id) {
			if (id == null) {
				return NotFound();
			}

			var book = await _context.Books.SingleOrDefaultAsync(b => b.ID == id);
			if (book == null) {
				return NotFound();
			}
			return View(book);
		}

		// POST: Books/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost, ActionName(nameof(Edit))]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditPost(int? id) {
			if (!id.HasValue)
				return NotFound();

			var bookToUpdate = await _context.Books.SingleOrDefaultAsync(b => b.ID == id);
			if (await TryUpdateModelAsync(bookToUpdate, "", Book.PropertiesToUpdate)) {
				try {
					await _context.SaveChangesAsync();
					return RedirectToAction(nameof(Index));
				} catch (DbUpdateException e) {
					ModelState.AddModelError("", $"Unable to save changes ({e.Message}). " +
					                             "Please try again and if the problem persists, " +
					                             "see your system administrator.");
				}
			}

			return View(bookToUpdate);
		}

		// GET: Books/Delete/5
		public async Task<IActionResult> Delete(int? id) {
			if (id == null) {
				return NotFound();
			}

			var book = await _context.Books
				.SingleOrDefaultAsync(m => m.ID == id);
			if (book == null) {
				return NotFound();
			}

			return View(book);
		}

		// POST: Books/Delete/5
		[HttpPost, ActionName(nameof(Delete))]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id) {
			var book = await _context.Books.SingleOrDefaultAsync(m => m.ID == id);
			_context.Books.Remove(book);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool BookExists(int id) {
			return _context.Books.Any(e => e.ID == id);
		}
	}
}