using PortfolioInvestimentos.Domain.Entities;
using PortfolioInvestimentos.Domain.Enums;
using PortfolioInvestimentos.Domain.Repositories;
using PortfolioInvestimentos.Domain.Services;
using Quartz;
using System;
using System.Text;

namespace PortfolioInvestimentos.Application.Services.Email
{
    public class DailyDueDateProductsEmailJob : IJob
    {
        private readonly IEmailSenderService _emailSenderService;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        public DailyDueDateProductsEmailJob(
            IEmailSenderService emailSenderService,
            IUserRepository userRepository,
            IProductRepository productRepository
            )
        {
            _emailSenderService = emailSenderService;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        private string GenerateProductsMessage(IEnumerable<Product> products)
        {
            StringBuilder htmlBuilder = new StringBuilder();


            htmlBuilder.Append("Atenção! Estes produtos estão prestes a vencer");

            htmlBuilder.Append("<html><body>");
            htmlBuilder.Append("<table border='1'>");
            htmlBuilder.Append("<tr>");
            htmlBuilder.Append("<th>Produto</th>");
            htmlBuilder.Append("<th>Data de Vencimento</th>");
            htmlBuilder.Append("</tr>");

            foreach (var product in products)
            {
                htmlBuilder.Append("<tr>");
                htmlBuilder.AppendFormat("<td>{0}</td>", product.Name);
                htmlBuilder.AppendFormat("<td>{0}</td>", product.DueDate.ToString("dd/MM/yyyy"));
                htmlBuilder.Append("</tr>");
            }

            htmlBuilder.Append("</table>");
            htmlBuilder.Append("</body></html>");

            return htmlBuilder.ToString();
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var managerUsers = await _userRepository
                .GetAllWithParamsAsync(x => x.Role == UserRole.Manager);

            var products = await _productRepository
            .GetNearDueDateProductsAsync();


            if(products.Any())
            {
                var productsMessage = this.GenerateProductsMessage(products);

                foreach (var user in managerUsers)
                {
                    await _emailSenderService.SendEmailAsync(user.Email, "Atualização de Email diária", productsMessage);
                }
            }
        }
    }
}
