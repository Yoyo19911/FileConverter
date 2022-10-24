using FileConverter.Application.Services;
using PuppeteerSharp;

namespace FileConverter.Infrastructure.Services;

public class HtmlToPdfConverter : IHtmlToPdfConverter
{
    public async Task<byte[]> ConvertAsync(byte[] htmlFileBody)
    {
        string htmlFilePath = await WriteHtmlFile(htmlFileBody);
        byte[] pdfFileBody = await GetPdfFileBody(htmlFilePath);
        File.Delete(htmlFilePath);

        return pdfFileBody;
    }

    /// <summary>
    /// Writes a new html file to local storage using its body
    /// </summary>
    /// <returns>Full path to new html file</returns>
    private static async Task<string> WriteHtmlFile(byte[] htmlFileBody)
    {
        var filesFolder = Path.Combine(Directory.GetCurrentDirectory(), "Files");
        var htmlFilePath = Path.Combine(filesFolder, Guid.NewGuid().ToString() + ".html");
        Directory.CreateDirectory(filesFolder);
        await File.WriteAllBytesAsync(htmlFilePath, htmlFileBody);
        return htmlFilePath;
    }

    /// <summary>
    /// Converts html file body to pdf file body
    /// </summary>
    /// <param name="htmlFilePath">Full path to the html file which body will be converted</param>
    /// <returns>Body of the pdf file as an array of bytes</returns>
    private static async Task<byte[]> GetPdfFileBody(string htmlFilePath)
    {
        var browserFetcher = new BrowserFetcher();
        await browserFetcher.DownloadAsync();
        await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
        await using var page = await browser.NewPageAsync();
        await page.GoToAsync($"file://{htmlFilePath.Replace('\\', '/')}");
        return await page.PdfDataAsync();
    }
}