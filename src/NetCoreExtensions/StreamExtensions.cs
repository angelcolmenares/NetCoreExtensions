namespace NetCoreExtensions;

public static class StreamExtensions
{
    public static async Task<byte[]> ConvertToByteArray(this Stream stream)
    {
        var memStream = (stream as MemoryStream);
        if (memStream != null) return memStream.ToArray();

        using (memStream = new MemoryStream())
        {
            await stream.CopyToAsync(memStream);
            return memStream.ToArray();
        }
    }

    public static async Task<string> ConvertToBase64(this Stream stream)
    {
        var array = await ConvertToByteArray(stream);

        return Convert.ToBase64String(array);   
    }

}
