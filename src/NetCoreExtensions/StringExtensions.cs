namespace NetCoreExtensions;
public static class StringExtensions
{
    public static (Stream Stream, long Length) ConverToStream(this string base64string)
    {
        var bytes = Convert.FromBase64String(base64string);
        return (new MemoryStream(bytes), bytes.LongLength);

    }
}
