using Microsoft.AspNetCore.Mvc;
using System.Net;
using Zalo.Clean.Api.Models;
using Zalo.Clean.Application.Exceptions;

namespace Zalo.Clean.Api.Middlewares
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
                await next(httpContext);
            }catch(Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }

        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomValidationProblemDetails problem = new();

            switch (ex)
            {
                case BadRequestException badHttpRequestException:
                    statusCode= HttpStatusCode.BadRequest;
                    problem = new CustomValidationProblemDetails
                    {
                        Title= badHttpRequestException.Message,
                        Status = (int)statusCode,
                        Detail = badHttpRequestException.InnerException?.Message,
                        Type = nameof(badHttpRequestException),
                        Errors = badHttpRequestException.Errors,
                    };
                    break;

                case NotFoundException notfound:
                    statusCode= HttpStatusCode.NotFound;
                    problem = new CustomValidationProblemDetails
                    {
                        Title = notfound.Message,
                        Status = (int)statusCode,
                        Type = nameof(NotFoundException),
                        Detail = notfound.InnerException?.Message,
                    };


                    break;
                default:
                    break;

            }

            httpContext.Response.StatusCode = (int)statusCode;

            await httpContext.Response.WriteAsJsonAsync(problem);

        }
    }
}
