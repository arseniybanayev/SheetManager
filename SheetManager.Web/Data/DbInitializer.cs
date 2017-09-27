using SheetManager.Web.Models;
using System.Linq;

namespace SheetManager.Web.Data
{
	public static class DbInitializer
	{
		public static void Initialize(SheetsContext context)
		{
			context.Database.EnsureCreated();

			if (context.Books.Any())
				return; // DB has already been seeded with test data

			var books = new[] {
				new Book {
					Name = "After Midnight Jazz",
					Path = @"C:\Users\arsen\Sheet Music\Jazz & Blues\(VA) After Midnight Jazz.pdf",
					Genre = Genre.JazzBlues,
					IsArrangement = true,
					IsCompilation = true
				}
			};

			foreach (var book in books)
				context.Add(book);
			context.SaveChanges();

			var songs = new[] {
				new Song("Be Mine Tonight", 1, 6),
				new Song("Black Coffee", 1, 10),
				new Song("Georgia On My Mind", 1, 15),
				new Song("Goodnight Sweetheart", 1, 16),
				new Song("How Insensitive", 1, 21),
				new Song("I'm Old Fashioned", 1, 24),
				new Song("I'm Beginning To See The Light", 1, 28),
				new Song("In The Still Of The Night", 1, 31),
				new Song("In The Wee Small Hours Of The Morning", 1, 36),
				new Song("Killing Me Softly With His Song", 1, 38),
				new Song("Lullaby Of Birdland", 1, 44),
				new Song("Love Me Tonight", 1, 48),
				new Song("Nice 'n' Easy", 1, 53),
				new Song("Night And Day", 1, 56),
				new Song("Old Devil Moon", 1, 60),
				new Song("On This Night Of A Thousand Stars", 1, 63),
				new Song("Paris By Night", 1, 66),
				new Song("Quiet Nights Of Quiet Stars", 1, 68),
				new Song("Round Midnight", 1, 72),
				new Song("Sexual Healing", 1, 78),
				new Song("So Tired", 1, 85),
				new Song("Stella By Starlight", 1, 88),
				new Song("The Point Is Jumpin'", 1, 92),
				new Song("The Look Of Love", 1, 96),
				new Song("The Nearness of You", 1, 99),
				new Song("The Night We Called It A Day", 1, 104),
				new Song("These Foolish Things", 1, 108),
				new Song("The Way You Look Tonight", 1, 114),
				new Song("Tonight", 1, 119),
				new Song("What A Little Moonlight Can Do", 1, 122),
				new Song("Wonderful Tonight", 1, 126)
			};

			foreach (var song in songs)
				context.Add(song);
			context.SaveChanges();
		}
	}
}