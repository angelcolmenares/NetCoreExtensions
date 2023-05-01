using System.Text.RegularExpressions;

namespace NetCoreExtensions.MinimalEndpoints;
public static class DefaultRouteBuilder
{
    static readonly Regex r = new(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])",
             RegexOptions.IgnorePatternWhitespace);


    public static string GetActionName<TEndpoint>() => GetActionName(typeof(TEndpoint));
    public static string GetTemplate<TEndpoint>(
        string prefix,
        string? templateParams = null,
        string endpointsNamespace = "Endpoints")
        => GetTemplate(typeof(TEndpoint), prefix, templateParams, endpointsNamespace);

    public static string GetResourceName<TEndpoint>(string endpointsNamespace = "Endpoints")
        => GetResourceName(typeof(TEndpoint), endpointsNamespace);

    public static string GetActionName(Type endpointType) 
        => r.Replace(endpointType.Name.Replace("Endpoint", ""), "-").ToLower();

    public static string GetTag<TEndpoint>(string endpointsNamespace = "Endpoints")
        => GetTag(typeof(TEndpoint), endpointsNamespace);   


    public static string GetTemplate(
        Type endpointType,
        string prefix,
        string? templateParams=null,
        string endpointsNamespace = "Endpoints")
    {
        var _prefix = "/" + prefix.Trim('/');
        var _resourceName = GetResourceName(endpointType, endpointsNamespace);
        if (!string.IsNullOrEmpty(_resourceName)) 
        {
            _resourceName = "/" + _resourceName;
        }
        var _actionName = "/" + GetActionName(endpointType);

        var _templateParams = !string.IsNullOrEmpty(templateParams) ? "/" + templateParams.Trim('/') : "";
        return _prefix + _resourceName + _actionName + _templateParams;
    }

    public static string GetResourceName(Type type, string endpointsNamespace = "Endpoints")
    {
        var parts = type?.Namespace?.Split(".")?.ToList() ?? new List<string>();

        var index = parts.LastIndexOf(endpointsNamespace);

        return ((index < 0 || parts.Count() < index + 2) ? "" : 
            r.Replace(parts[index + 1],"-").ToLower());
    }

    public static string GetTag(Type type, string endpointsNamespace = "Endpoints")
    {
        var parts = type?.Namespace?.Split(".")?.ToList() ?? new List<string>();

        var index = parts.LastIndexOf(endpointsNamespace);

        return (index < 0 || parts.Count() < index + 2) ? "" : parts[index + 1];
    }

}
