using EasyMicroservices.ServiceContracts;
using EasyMicroservices.ServiceContracts.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using ParehNegar.Domain.Contracts.Authentications;
using ParehNegar.Logics.Attributes;
using ParehNegar.Logics.Logics;

namespace ParehNegar.WebApi.Middlewares;

// [https://github.com/EasyMicroservices/Cores]
public class AppAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    //
    // Parameters:
    //   next:
    public AppAuthorizationMiddleware(RequestDelegate next)
    {
            _next = next;
    }

    //
    // Parameters:
    //   httpContext:
    //
    //   baseUnitOfWork:
    public async Task Invoke(HttpContext httpContext)
    {
        var originalBodyStream = httpContext.Response.Body;
        using var responseBody = new MemoryStream();
        httpContext.Response.Body = responseBody;

        bool isAccessDenied = false;

        try
        {
            await _next(httpContext);

            var endpoint = httpContext.GetEndpoint();
            var authAttribute = endpoint?.Metadata.GetMetadata<CustomAuthorizeCheckAttribute>();

            if (authAttribute != null)
            {
                var claimTypes = authAttribute.ClaimTypes;
                var user = httpContext.User;

                if (user.Identity is { IsAuthenticated: false } || !claimTypes.All(claimType => user.Claims.Any(claim => claim.Type == claimType)))
                    isAccessDenied = true;
            }

            if (httpContext.Response.StatusCode is 401 or 403 || isAccessDenied)
            {
                MessageContract messageContract = FailedReasonType.SessionAccessDenied;
                messageContract.Error.ServiceDetails.MethodName = httpContext.Request.Path.ToString();
                messageContract.Error.Details = $"Session access denied!";
                await WriteResponse(httpContext, messageContract, 200, originalBodyStream);
            }
            else if (httpContext.Response.StatusCode == 204)
            {
                MessageContract messageContract = FailedReasonType.Nothing;
                messageContract.Error.Message = "Do not send null value to the service response! always send me a MessageContract";
                messageContract.Error.ServiceDetails.MethodName = httpContext.Request.Path.ToString();
                await WriteResponse(httpContext, messageContract, 200, originalBodyStream);
            }
            else
            {
                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }
        catch (Exception exception)
        {
            await ExceptionHandler(httpContext, exception);
        }
        finally
        {
            httpContext.Response.Body = originalBodyStream;
        }
    }

    private async Task WriteResponse(HttpContext httpContext, MessageContract messageContract, int statusCode, Stream originalBodyStream)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        string responseText = JsonSerializer.Serialize(messageContract);

        var responseBytes = Encoding.UTF8.GetBytes(responseText);
        await originalBodyStream.WriteAsync(responseBytes, 0, responseBytes.Length);
        await originalBodyStream.FlushAsync(); 
    }
    
    internal static Task ExceptionHandler(HttpContext context)
    {
        Exception exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        return ExceptionHandler(context, exception);
    }
    
    //
    // Parameters:
    //   context:
    //
    //   exception:
    internal static Task ExceptionHandler(HttpContext context, Exception exception)
    {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 200;
            MessageContract messageContract;
            if (exception is InvalidResultOfMessageContractException ex)
            {
                messageContract = ex.MessageContract;
                messageContract.Error.StackTrace.Add(ex.Message);
                messageContract.Error.StackTrace.AddRange(ex.StackTrace.ToListStackTrace());
            }
            else
            {
                messageContract = exception;
            }

            if (exception.Message.Contains("Authentication", StringComparison.OrdinalIgnoreCase) && messageContract.Error.FailedReasonType != FailedReasonType.AccessDenied)
            {
                messageContract.Error.FailedReasonType = FailedReasonType.SessionAccessDenied;
            }

            messageContract.Error.ServiceDetails.MethodName = context.Request.Path.ToString();
            string text = JsonSerializer.Serialize(messageContract);
            return context.Response.WriteAsync(text);
        }
}