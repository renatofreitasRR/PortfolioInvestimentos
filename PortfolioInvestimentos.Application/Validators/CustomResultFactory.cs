using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;
using SharpGrip.FluentValidation.AutoValidation.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioInvestimentos.Application.Validators
{
    public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
    {
        public IActionResult CreateActionResult(ActionExecutingContext context, ValidationProblemDetails? validationProblemDetails)
        {
            var errors = validationProblemDetails?
                .Errors
                .Values
                .SelectMany(x => x)
                .ToList();

            return new BadRequestObjectResult(new BadRequestResult { Data = null, Errors = errors });
        }
    }
}
