using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioInvestimentos.Application.Validators
{
    public class BadRequestResult
    {
        public List<string> Errors { get; set; } = new List<string>();

        public object? Data { get; set; }

    }
}
