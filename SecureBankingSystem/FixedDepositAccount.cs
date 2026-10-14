using System;


namespace SecureBankingSystem
{
    public sealed class FixedDepositAccount : BankAccount
    {
        public decimal AnnualInterestRate { get; }
        public int TermMonths {  get; }
        public FixedDepositAccount(string ownerName, decimal openingBalance, decimal annualInterestRate, int termMonths)
            :base (ownerName, openingBalance)
        {
            // شروط الحماية (Guards)
            if (annualInterestRate <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(annualInterestRate), "Interest rate must be positive.");
            }

            if (termMonths <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(termMonths), "Term months must be positive.");
            }

            AnnualInterestRate = annualInterestRate;
            TermMonths = termMonths;
        }
        public override void RunMonthEnd()
        {
            decimal interest = CalculateMonthlyInterest(AnnualInterestRate);
            Deposit(interest, $"Fixed-term interest at {AnnualInterestRate:F2}%");
        }
        public override string GetSummary()
        => base.GetSummary() + $" | FIXED {TermMonths}mo @ {AnnualInterestRate:F2}%";


    }
}
