using ERPPlus.Application.Exceptions;
using FluentValidation;
using System.Net;
using System.Text.Json;


namespace ERPPlus.WebAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public  ExceptionMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try { 
            
            await _requestDelegate(context);
            
            } catch (Exception ex) {

                _logger.LogError(ex, "Exceção capturada pelo middleware");
                await HandleExceptionAsync(context, ex);
                
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception) {

            HttpStatusCode statusCode;
            string message;
            object errors = null;

            switch (exception)
            {
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = "Erro de validação";
                    errors = validationException.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                    break;
                case BusinessRuleException businessRuleException:
                    statusCode = HttpStatusCode.UnprocessableEntity;
                    message = businessRuleException.Message;
                    break;
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    message = notFoundException.Message;
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "Ops! Algo deu errado. Tente novamente em instantes";
                    break;
            }

            var response = new
            {
                statusCode = (int)statusCode,
                message,
                errors
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            return context.Response.WriteAsJsonAsync(json);
        }

    }
}
