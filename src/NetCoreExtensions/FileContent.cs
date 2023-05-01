using Microsoft.AspNetCore.StaticFiles;

namespace NetCoreExtensions;

public record FileContent(Stream Stream, string FileName, long Length)
{
    public string GetContentType()
        => new FileExtensionContentTypeProvider().TryGetContentType(FileName, out var contentType)
        ?
        contentType
        :
        "application/octet-stream"
        ;
}
