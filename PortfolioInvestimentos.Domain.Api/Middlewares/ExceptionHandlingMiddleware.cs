using PortfolioInvestimentos.Domain.Api.Models;
using System.Net;
using System.Text.Json;

namespace PortfolioInvestimentos.Domain.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResult = new ControllerResult();
            errorResult.AddError("Erro interno ao processar a solicitação");

            var jsonResponse = JsonSerializer.Serialize(errorResult);

            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
