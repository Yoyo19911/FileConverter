namespace FileConverter.Domain.Models;

/// <remarks>name "File" is already taken by "System.IO" namespace</remarks>
public record struct FileModel(string Name, byte[] Content);