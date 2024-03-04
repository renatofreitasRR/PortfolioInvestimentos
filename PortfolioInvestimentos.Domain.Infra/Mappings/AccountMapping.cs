using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioInvestimentos.Domain.Entities;

namespace PortfolioInvestimentos.Domain.Infra.Mappings
{
    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .ValueGeneratedOnAdd()
                   .IsRequired();

            builder.Property(u => u.UserId)
                  .IsRequired();

            builder.Property(u => u.Value)
                  .IsRequired();

            builder.Property(u => u.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()");
        }
    }
}
