using EasyMicroservices.ServiceContracts;
using EasyMicroservices.ServiceContracts.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace ParehNegar.WebApi.Middlewares
{
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
            try
            {
                //IAuthorization authorization = baseUnitOfWork.GetAuthorization();
                //if (authorization != null)
                //{
                //    MessageContract messageContract = await authorization.CheckIsAuthorized(httpContext);
                //    if (!messageContract)
                //    {
                //        httpContext.Response.ContentType = "application/json";
                //        httpContext.Response.StatusCode = 200;
                //        messageContract.Error.ServiceDetails.MethodName = httpContext.Request.Path.ToString();
                //        string text = JsonSerializer.Serialize(messageContract);
                //        await httpContext.Response.WriteAsync(text);
                //        return;
                //    }
                //}

                await _next(httpContext);
                if (httpContext.Response.StatusCode == 401 || httpContext.Response.StatusCode == 403)
                {
                    httpContext.Response.ContentType = "application/json";
                    httpContext.Response.StatusCode = 200;
                    MessageContract messageContract2 = FailedReasonType.SessionAccessDenied;
                    messageContract2.Error.ServiceDetails.MethodName = httpContext.Request.Path.ToString();
                    messageContract2.Error.Details = $"StatusCode: {httpContext.Response.StatusCode}";
                    string text2 = JsonSerializer.Serialize(messageContract2);
                    await httpContext.Response.WriteAsync(text2);
                }
                else if (httpContext.Response.StatusCode == 204)
                {
                    httpContext.Response.ContentType = "application/json";
                    httpContext.Response.StatusCode = 200;
                    MessageContract messageContract3 = FailedReasonType.Nothing;
                    messageContract3.Error.Message = "Do not send null value to the service response! always send me a MessageContract";
                    messageContract3.Error.ServiceDetails.MethodName = httpContext.Request.Path.ToString();
                    string text3 = JsonSerializer.Serialize(messageContract3);
                    await httpContext.Response.WriteAsync(text3);
                }
            }
            catch (Exception exception)
            {
                await ExceptionHandler(httpContext, exception);
            }
        }

        //
        // Parameters:
        //   context:
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

            if (exception.Message.Contains("Authenti", StringComparison.OrdinalIgnoreCase) && messageContract.Error.FailedReasonType != FailedReasonType.AccessDenied)
            {
                messageContract.Error.FailedReasonType = FailedReasonType.SessionAccessDenied;
            }

            messageContract.Error.ServiceDetails.MethodName = context.Request.Path.ToString();
            string text = JsonSerializer.Serialize(messageContract);
            return context.Response.WriteAsync(text);
        }
    }
}
