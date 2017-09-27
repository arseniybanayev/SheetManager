using System.ComponentModel.DataAnnotations;

namespace SheetManager.Web.Models
{
	public enum Genre
	{
		[Display(Name = "Classical")]
		Classical				= 0,

		[Display(Name = "Solo Piano")]
		SoloPiano				= 1,

		[Display(Name = "Jazz & Blues")]
		JazzBlues				= 2,

		[Display(Name = "Christmas")]
		Christmas				= 3
	}
}