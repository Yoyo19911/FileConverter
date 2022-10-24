namespace FileConverter.Application.Services;

/// <summary>
/// The class responsible for converting html to pdf
/// </summary>
public interface IHtmlToPdfConverter
{
    /// <summary>
    /// Converts the body of html file to the body of pdf file
    /// </summary>
    /// <returns>Returns pdf file body as an array of bytes</returns>
    Task<byte[]> ConvertAsync(byte[] htmlFileBody);
}