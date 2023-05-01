using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Reflection;

namespace NetCoreExtensions.MinimalEndpoints;

public static class AssemblyExtensions
{
    public static AssemblyInfo GetAssemblyInfo(this Assembly assembly, IHostEnvironment? hostEnvironment = null)
    {
        var lastWriteTime = System.IO.File.GetLastWriteTime(assembly.Location);
        var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
        var name = assembly?.GetName()?.Name?.Replace(".dll", "").Replace(".", " ") ?? string.Empty;

        return new AssemblyInfo(name, lastWriteTime, fileVersionInfo, hostEnvironment);

    }
}
