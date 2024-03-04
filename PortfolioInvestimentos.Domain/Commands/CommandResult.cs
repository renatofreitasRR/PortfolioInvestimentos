using PortfolioInvestimentos.Domain.Commands.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioInvestimentos.Domain.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult() { }

        public CommandResult(object data, string error)
        {
            Data = data;
            this.Errors.Add(error);
        }

        public CommandResult(object data, List<string> errors)
        {
            Data = data;
            Errors = errors;
        }

        public CommandResult(object data)
        {
            Data = data;
        }

        public object? Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
