using System;
using System.Collections.Generic;
using System.IO;
using Ghostscript.NET.Rasterizer;
using Tesseract;
using ImageFormat = System.Drawing.Imaging.ImageFormat;

namespace SheetManager.Metadata
{
	public static class PdfTools
	{
		/// <summary>
		/// Yields OCR'd text from the supplied PDF, page by page, from 'startPage' to 'endPage'
		/// </summary>
		public static IEnumerable<string> GetText(string pdfPath, int startPage = 1, int? endPage = null) {
			var rasterizer = new GhostscriptRasterizer();
			rasterizer.Open(pdfPath);

			for (var page = startPage; page <= (endPage ?? rasterizer.PageCount); page++) {
				using (var pix = rasterizer.GetPix(page))
				using (var tessPage = TessEngine.Value.Process(pix)) {
					yield return tessPage.GetText();
				}
			}

			rasterizer.Close();
		}

		public static void SetDebugOn() => TessEngine.Value.SetVariable("tessedit_write_images", true);
		public static void SetDebugOff() => TessEngine.Value.SetVariable("tessedit_write_images", false);

		private static readonly Lazy<TesseractEngine> TessEngine = new Lazy<TesseractEngine>(() => new TesseractEngine(@"./tessdata", "eng", EngineMode.Default));

		/// <summary>
		/// Don't forget to DISPOSE this!
		/// </summary>
		internal static Pix GetPix(this GhostscriptRasterizer rasterizer, int pageNumber) {
			const int dpi = 400; // the higher, the more accurate OCR will be
			var image = rasterizer.GetPage(dpi, dpi, pageNumber);
			var memoryStream = new MemoryStream();
			image.Save(memoryStream, ImageFormat.Tiff);
			return Pix.LoadTiffFromMemory(memoryStream.ToArray());
		}
	}
}