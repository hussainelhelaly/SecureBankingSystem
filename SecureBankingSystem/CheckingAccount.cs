using System;


namespace SecureBankingSystem
{
    public class CheckingAccount : BankAccount
    {
        public decimal OverdraftLimit { get; private set; }

        public CheckingAccount(string ownerName, decimal openingBalance = 500.00m, decimal overdraftLimit = 500.00m)
            : base(ownerName, openingBalance)
        {
            if (overdraftLimit < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(overdraftLimit), "Overdraft limit cannot be negative.");
            }

            OverdraftLimit = overdraftLimit;
        }

        protected override decimal MinimumAllowedBalance => -OverdraftLimit;

    }
}
