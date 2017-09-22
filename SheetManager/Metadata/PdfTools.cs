using System.Collections.Generic;
using System.IO;
using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;
using Tesseract;

namespace SheetManager.Metadata
{
	public static class PdfTools
	{
		/// <summary>
		/// Yields OCR'd text from the supplied PDF, page by page, from 'startPage' to 'endPage'
		/// </summary>
		public static IEnumerable<string> GetText(string pdfPath, int startPage = 1, int? endPage = null) {
			const int dpi = 400;
			var lastInstalledVersion = GhostscriptVersionInfo.GetLastInstalledVersion(
				GhostscriptLicense.GPL | GhostscriptLicense.AFPL,
				GhostscriptLicense.GPL);

			var rasterizer = new GhostscriptRasterizer();
			rasterizer.Open(pdfPath, lastInstalledVersion, false);

			using (var tessEngine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default)) {
				//tessEngine.SetVariable("tessedit_write_images", true);
				for (var page = startPage; page <= (endPage ?? rasterizer.PageCount); page++) {
					var img = rasterizer.GetPage(dpi, dpi, page);
					var ms = new MemoryStream();
					img.Save(ms, System.Drawing.Imaging.ImageFormat.Tiff);

					using (var pix = Pix.LoadTiffFromMemory(ms.ToArray()))
					using (var tessPage = tessEngine.Process(pix)) {
						yield return tessPage.GetText();
					}
				}

				rasterizer.Close();
			}
		}
	}
}