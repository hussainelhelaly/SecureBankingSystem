using System;

namespace SecureBankingSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount sara = new BankAccount("Sara Ahmed", 1500.00m);

            BankAccount ahmed = new BankAccount("Ahmed Khaled ", 2000.00m);

            //BankAccount layla = new BankAccount()
            //{
            //    OwnerName = "Layla",
            //    //Balance = 1000.00m
            //};
            BankAccount layla = new BankAccount("Layla", 1000.00m);
            //================================================
            sara.Deposit(250.00m);
            sara.Deposit(100.00m, "Salary top-up");

            var withdrawalSucceded = sara.withdraw(300.00m);
            Console.WriteLine($"Withdraw returned : {withdrawalSucceded}");

            var withdrawalRefused = ahmed.withdraw(5000);
            Console.WriteLine($"Withdraw returned : {withdrawalRefused}");

            sara.ApplyMonthlyFee();
            ahmed.ApplyMonthlyFee(15);

            sara.TransferTo(ahmed, 500.00m);

            Console.WriteLine(sara.GetSummary());
            Console.WriteLine(ahmed.GetSummary());

            layla.SetOwnerName("Layla Mahmoud");
            Console.WriteLine(layla.GetSummary());
            Console.WriteLine(BankAccount.GetBankInfo());
            ///===============================================
            //sara.Balance = -999999.99m;
            Console.WriteLine(sara.GetSummary());

            //sara.AccountNumber = 7;
            Console.WriteLine(sara.GetSummary());

            // Validation and setters
            try
            {
                sara.Deposit(-500.00m);

            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"REFUSED -> {ex.Message}");
            }
            Console.WriteLine(sara.GetSummary());

            //=======================================

            Console.WriteLine($"Layla Balance {layla.Balance:F2} | Available {layla.AvailableBalance:F2} | On Hold? {layla.HasFundsOnHold} | Low? {layla.IsLowBalance}");

            StaleBalanceReport report = new(layla);
            Console.WriteLine(report.GetLine());

            layla.HoldFunds(600.00m);
            Console.WriteLine($"Layla Balance {layla.Balance:F2} | Available {layla.AvailableBalance:F2} | On Hold? {layla.HasFundsOnHold} | Low? {layla.IsLowBalance}");

            layla.ReleaseHold(600.00m);
            layla.withdraw(900.00m);

            Console.WriteLine(report.GetLine());
            Console.WriteLine($"[computed live] #{layla.AccountNumber} | {layla.OwnerName} | {layla.Balance:F2} | low? {layla.IsLowBalance}");
        }

       
    }
}
