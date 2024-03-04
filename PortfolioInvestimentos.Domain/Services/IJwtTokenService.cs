﻿using PortfolioInvestimentos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioInvestimentos.Domain.Services
{
    public interface IJwtTokenService
    {
        string GetToken(User user);
    }
}
