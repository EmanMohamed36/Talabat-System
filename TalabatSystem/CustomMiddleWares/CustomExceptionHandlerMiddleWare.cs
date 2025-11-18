using DomainLayer.Exceptions;
using DomainLayer.Exceptions.ForbiddenExceptions;
using DomainLayer.Exceptions.NotFoundExceptions;
using Shared.ErrorModels;
using System.Text.Json;

namespace TalabatSystem.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate Next, ILogger<CustomExceptionHandlerMiddleWare> logger) {
            _next = Next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
                await HandleNotFoundEndPointAsync(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Somthing Went Wrong");
                await HandleExceptionAsync(httpContext, ex);

            }

        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            //Create Response Object
            var response = new ErrorToReturn()
            {
                ErrorMessage = ex.Message
            };


            //Set Status Code For Response 
            //httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                BadRequestException badRequest => GetBadRequestErrors(response,badRequest),
                EmailOrPhoneAlreadyExistException => StatusCodes.Status409Conflict,
                ForbiddenException => StatusCodes.Status403Forbidden,
                
                _ => StatusCodes.Status500InternalServerError
            };

            response.StatusCode = httpContext.Response.StatusCode;
            
            //Set Content Type For Response 
            //httpContext.Response.ContentType = "application/json";

            
            //Return Object As Json
            //var responseToReturn = JsonSerializer.Serialize(response);
            //await httpContext.Response.WriteAsync(responseToReturn);
            await httpContext.Response.WriteAsJsonAsync(response);
        }
        private static int GetBadRequestErrors(ErrorToReturn response, BadRequestException badRequestException)
        {
            response.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }
        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End Point {httpContext.Request.Path} is Not Found"
                };
                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
