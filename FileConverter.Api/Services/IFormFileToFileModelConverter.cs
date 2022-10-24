using FileConverter.Domain.Models;

namespace FileConverter.Api.Services;

public interface IFormFileToFileModelConverter
{
    Task<FileModel> Convert(IFormFile file);
}