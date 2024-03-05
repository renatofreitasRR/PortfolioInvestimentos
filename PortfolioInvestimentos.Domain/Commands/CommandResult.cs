using PortfolioInvestimentos.Domain.Commands.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioInvestimentos.Domain.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult() 
        {
            StatusCode = HttpStatusCode.OK;
        }

        public CommandResult(HttpStatusCode statusCode, object? data, string error)
        {
            StatusCode = statusCode;
            Data = data;
            this.Errors.Add(error);
        }

        public CommandResult(HttpStatusCode statusCode, object? data, List<string> errors)
        {
            StatusCode = statusCode;
            Data = data;
            Errors = errors;
        }

        public CommandResult(HttpStatusCode statusCode, object? data)
        {
            StatusCode = statusCode;
            Data = data;
        }

        public object? Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public HttpStatusCode StatusCode { get; set; }
    }
}
