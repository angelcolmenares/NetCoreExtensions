using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;

namespace NetCoreExtensions.MinimalEndpoints;

public record AssemblyInfo(
    string FriendlyName,
    DateTime LastWriteTime,
    FileVersionInfo? FileVersionInfo,
    IHostEnvironment? HostEnvironment)
{
    public override string ToString()
    => @$"Name: {FriendlyName}, 
Version: {FileVersionInfo?.ProductVersion}, 
Last Updated: {LastWriteTime}, 
Server UTC Datetime: {DateTime.UtcNow}, 
EnvironmentName: {HostEnvironment?.EnvironmentName}";
}

