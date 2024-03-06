using PortfolioInvestimentos.Domain.Enums;

namespace PortfolioInvestimentos.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public Transaction(int accountId, int productId, int quantity, OperationType operationType, decimal value)
        {
            AccountId = accountId;
            ProductId = productId;
            Quantity = quantity;
            OperationType = operationType;
            Value = value;
            CreatedAt = DateTime.UtcNow;
        }

        protected Transaction() { }

        public int AccountId { get; private set; }
        public Account Account { get; private set; }
        public int ProductId { get; private set; }
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public OperationType OperationType { get; private set; }
        public decimal Value { get; private set; }
    }
}
