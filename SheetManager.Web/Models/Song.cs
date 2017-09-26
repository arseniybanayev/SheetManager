namespace SheetManager.Web.Models
{
	public class Song
	{
		public Song() { }

		public Song(string name, int bookId, int pageNumber)
		{
			Name = name;
			BookID = bookId;
			PageNumber = pageNumber;
		}

		public int ID { get; set; }

		public string Name { get; set; }
		public int BookID { get; set; }
		public int PageNumber { get; set; }

		public Book Book { get; set; }
	}
}