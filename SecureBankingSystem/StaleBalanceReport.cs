using System;

namespace SecureBankingSystem
{
    public class StaleBalanceReport
    {
        public int AccountNumber { get;}
        public string OwnerName {  get;}
        public bool IsLowBalanceSnapshot { get;}
        public decimal BalanceSnapshot { get; }

        public StaleBalanceReport(BankAccount account)
        {
            AccountNumber = account.AccountNumber;
            OwnerName = account.OwnerName;
            BalanceSnapshot = account.Balance;

            IsLowBalanceSnapshot = (account.Balance -account.HeldAmount) < BankAccount.MinimumOpeningBalance;
        }

        public string GetLine()
        {
            return $"[stored report] #{AccountNumber} | {OwnerName} | balance: {BalanceSnapshot:F2} | low? {IsLowBalanceSnapshot}";
        }

    }
}
