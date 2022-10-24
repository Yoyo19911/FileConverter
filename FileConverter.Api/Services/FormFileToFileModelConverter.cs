using FileConverter.Domain.Models;

namespace FileConverter.Api.Services;

public class FormFileToFileModelConverter : IFormFileToFileModelConverter
{
    public async Task<FileModel> Convert(IFormFile file)
        => new FileModel
        {
            Name = file.Name,
            Content = await GetFileBody(file)
        };

    private async Task<byte[]> GetFileBody(IFormFile file)
    {
        var body = new byte[file.Length];
        using var ms = new MemoryStream(body);
        await file.CopyToAsync(ms);

        return body;
    }
}