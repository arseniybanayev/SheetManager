using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SheetManager.Web.Models
{
	public class Book
	{
		/// <summary>
		/// Primary key, auto-assigned
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// Name of book
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Path to source PDF on disk
		/// </summary>
		public string Path { get; set; }

		/// <summary>
		/// One of the preset genres of sheet music
		/// </summary>
		public Genre? Genre { get; set; }

		/// <summary>
		/// Indicates that this book is a compilation of
		/// various artist's works (either original or arranged)
		/// </summary>
		public bool IsCompilation { get; set; }

		/// <summary>
		/// Indicates that this book's songs are arrangements
		/// rather than original compositions
		/// </summary>
		public bool IsArrangement { get; set; }

		/// <summary>
		/// Songs in this book
		/// </summary>
		public ICollection<Song> Songs { get; set; }

		private const string Comma = ",";

		internal const string PropertiesToBind = nameof(Name) + Comma +
		                                         nameof(Path) + Comma +
		                                         nameof(Genre) + Comma +
		                                         nameof(IsArrangement) + Comma +
		                                         nameof(IsCompilation);

		internal static readonly Expression<Func<Book, object>>[] PropertiesToUpdate = {
			b => b.Name,
			b => b.Path,
			b => b.Genre,
			b => b.IsArrangement,
			b => b.IsCompilation
		};
	}
}