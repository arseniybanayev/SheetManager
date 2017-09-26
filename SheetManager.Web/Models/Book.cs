using System.Collections.Generic;

namespace SheetManager.Web.Models
{
	public class Book
	{
		public Book() { }

		public Book(string pdfPath, string name)
		{
			Path = pdfPath;
			Name = name;
		}

		public int ID { get; set; }
		public string Path { get; set; }
		public string Name { get; set; }

		public ICollection<Song> Songs { get; set; }
	}
}