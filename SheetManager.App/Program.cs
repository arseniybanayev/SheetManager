﻿using System;
using SheetManager.Metadata;

namespace SheetManager.App
{
	internal class Program
	{
		public static void Main(string[] args) {
			const string pdfPath = @"C:\dev\SheetManager\SheetManager\lanz.pdf";
			var text = PdfTools.GetText(pdfPath);
			var i = 0;
			foreach (var page in text) {
				Console.WriteLine($"Page {i} ----------------------------------------");
				Console.WriteLine(page);
				Console.WriteLine($"------------------------------- ENTER to continue");
				Console.ReadLine();
				i++;
			}
		}
	}
}

/*var testImagePath = "./phototest.tif";
if (args.Length > 0) {
	testImagePath = args[0];
}

try {
	var logger = new FormattedConsoleLogger();
	using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default)) {
		using (var img = Pix.LoadFromFile(testImagePath)) {
			using (logger.Begin("Process image")) {
				var i = 1;
				using (var page = engine.Process(img)) {
					var text = page.GetText();
					logger.Log("Text: {0}", text);
					logger.Log("Mean confidence: {0}", page.GetMeanConfidence());

					using (var iter = page.GetIterator()) {
						iter.Begin();
						do {
							if (i % 2 == 0) {
								using (logger.Begin("Line {0}", i)) {
									do {
										using (logger.Begin("Word Iteration")) {
											if (iter.IsAtBeginningOf(PageIteratorLevel.Block)) {
												logger.Log("New block");
											}
											if (iter.IsAtBeginningOf(PageIteratorLevel.Para)) {
												logger.Log("New paragraph");
											}
											if (iter.IsAtBeginningOf(PageIteratorLevel.TextLine)) {
												logger.Log("New line");
											}
											logger.Log("word: " + iter.GetText(PageIteratorLevel.Word));
										}
									} while (iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));
								}
							}
							i++;
						} while (iter.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
					}
				}
			}
		}
	}
} catch (Exception e) {
	Trace.TraceError(e.ToString());
	Console.WriteLine("Unexpected Error: " + e.Message);
	Console.WriteLine("Details: ");
	Console.WriteLine(e.ToString());
}*/