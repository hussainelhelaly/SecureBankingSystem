using System;

namespace SecureBankingSystem
{
    public class SavingsAccount : BankAccount
    {
        public decimal AnnualInterestRate { get; private set; }
        public SavingsAccount (string ownerName, decimal openingBalance, decimal annualInterestRate) : base(ownerName, openingBalance)
        {
            if (annualInterestRate < 0m)
            {
                throw new ArgumentOutOfRangeException(nameof(annualInterestRate), $"An interest rate may not be negative. Refused {annualInterestRate:F2}%.");
            }
            AnnualInterestRate = annualInterestRate;
            Console.WriteLine($"[SavingsAccount] #{AccountNumber} opened at {AnnualInterestRate:F2}% per year");
        }
        public decimal ShowInterestPreview()
        {
            // استدعاء الـ protected method من الأب بكل سهولة!
            decimal interest = CalculateMonthlyInterest(AnnualInterestRate);
            Console.WriteLine($"  Interest preview for #{AccountNumber}: {interest:F2} {Currency} this month");
            return interest;
        }

        public override void ApplyMonthlyFee(decimal fee = 5.00M)
        {
            Console.WriteLine($"Monthly fee waived on savings account {AccountNumber} — savings accounts pay no maintenance fee.");
        }

        public override void RunMonthEnd()
        {
            Console.WriteLine($"  [SavingsAccount] month-end for #{AccountNumber} ({OwnerName})");

            decimal interest = CalculateMonthlyInterest(AnnualInterestRate);

            if (interest > 0m)
            {
                Deposit(interest, $"Monthly interest at {AnnualInterestRate:F2}%");

            }
            else
            {
                Console.WriteLine($"  No interest due on #{AccountNumber} this month.");
            }
        }
        public override string GetSummary()
        {
            return base.GetSummary() + $" | SAVINGS @ {AnnualInterestRate:F2}%";
        }
            
        

    }
}
