using Microsoft.EntityFrameworkCore;
using SheetManager.Web.Models;

namespace SheetManager.Web.Data
{
	public class SheetsContext : DbContext
	{
		public SheetsContext(DbContextOptions<SheetsContext> options) : base(options) {

		}

		public DbSet<Book> Books { get; set; }
		public DbSet<Song> Songs { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Book>().ToTable("Book");
			modelBuilder.Entity<Song>().ToTable("Song");
		}
	}
}