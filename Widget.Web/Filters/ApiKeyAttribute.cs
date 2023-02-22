﻿using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Widget.Web.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class ApiKeyAttribute : Attribute, IAuthorizationFilter
{
    private const string ApiKeyHeaderName = "X-API-Key";
    private const string ApiKeyName = "ApiKey";

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var submittedApiKey = GetSubmittedApiKey(context.HttpContext);
        var apiKey = GetApiKey(context.HttpContext);

        if (!IsApiKeyValid(apiKey, submittedApiKey))
            context.Result = new UnauthorizedResult();
    }

    private static string GetSubmittedApiKey(HttpContext context)
    {
        return context.Request.Headers[ApiKeyHeaderName];
    }

    private static string? GetApiKey(HttpContext context)
    {
        var configuration = context.RequestServices.GetRequiredService<IConfiguration>();
        return configuration.GetValue<string>(ApiKeyName);
    }

    private static bool IsApiKeyValid(string? apiKey, string submittedApiKey)
    {
        if (string.IsNullOrEmpty(submittedApiKey)) return false;
        var apiKeySpan = MemoryMarshal.Cast<char, byte>(apiKey.AsSpan());
        var submittedApiKeySpan = MemoryMarshal.Cast<char, byte>(submittedApiKey.AsSpan());
        return CryptographicOperations.FixedTimeEquals(apiKeySpan, submittedApiKeySpan);
    }
}