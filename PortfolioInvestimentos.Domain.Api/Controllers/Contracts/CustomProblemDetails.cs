using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PortfolioInvestimentos.Domain.Api.Controllers.Contracts
{
    public static class CustomProblemDetails
    {
        public static CustomActionResult MakeValidationResponse(ActionContext context)
        {
            List<string> modelErrors = new();

            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status400BadRequest,
            };
            foreach (var keyModelStatePair in context.ModelState)
            {
                var errors = keyModelStatePair.Value.Errors;
                if (errors != null && errors.Count > 0)
                {
                    if (errors.Count == 1)
                    {
                        var errorMessage = errors[0].ErrorMessage;
                        modelErrors.Add(errorMessage);
                    }
                    else
                    {
                        var errorMessages = new string[errors.Count];
                        for (var i = 0; i < errors.Count; i++)
                        {
                            errorMessages[i] = errors[i].ErrorMessage;
                            modelErrors.Add(errorMessages[i]);
                        }
                    }
                }
            }

            return new CustomActionResult(HttpStatusCode.BadRequest, modelErrors);
        }

    }
}
