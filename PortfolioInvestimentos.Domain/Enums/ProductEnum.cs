using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioInvestimentos.Domain.Enums
{
    public enum ProductType
    {
        Stocks = 1,             // Ações
        FixedIncomeSecurities,  // Títulos de Renda Fixa
        GovernmentBonds,        // Títulos Públicos
        InvestmentFunds,        // Fundos de Investimento
        ExchangeTradedFunds,    // ETFs (Fundos Negociados em Bolsa)
        RealEstate,             // Investimento em Imóveis
        Derivatives,            // Derivativos
        Commodities,            // Commodities
        Cryptocurrencies,       // Criptomoedas
        RetirementSavings       // Previdência Privada
    }
}
