using Microsoft.AspNetCore.Mvc;
using PortfolioInvestimentos.Domain.Commands.Contracts;
using PortfolioInvestimentos.Domain.Api.Models;
using System.Net;

namespace PortfolioInvestimentos.Domain.Api.Controllers.Contracts
{
    public class CustomActionResult : IActionResult
    {
        private readonly HttpStatusCode _statusCode;
        private ControllerResult _returnData = new ControllerResult();
        public HttpStatusCode StatusCode => _statusCode;
        public List<string> Errors => _returnData.Errors;

        public CustomActionResult(HttpStatusCode statusCode, object? data = null)
        {
            _returnData.Data = data;
            _statusCode = statusCode;
        }

        public CustomActionResult(HttpStatusCode statusCode, object? data, List<string> list)
        {
            _returnData.Data = data;
            _statusCode = statusCode;
            _returnData.Errors = list;
        }

        public CustomActionResult(HttpStatusCode statusCode, List<string> list, bool isData = false)
        {
            if (isData == true)
                _returnData.Data = list;
            else
                _returnData.Errors = list;

            _statusCode = statusCode;
        }

        public CustomActionResult(HttpStatusCode statusCode, string content, bool isData = false)
        {
            if (isData == true)
                _returnData.Data = content;
            else
                _returnData.AddError(content);

            _statusCode = statusCode;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_returnData)
            {
                StatusCode = (int)_statusCode
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
