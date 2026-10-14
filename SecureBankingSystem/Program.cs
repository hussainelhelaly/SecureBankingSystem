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

            //var withdrawalSucceded = sara.withdraw(300.00m);
            //Console.WriteLine($"Withdraw returned : {withdrawalSucceded}");

            //var withdrawalRefused = ahmed.withdraw(5000);
            //Console.WriteLine($"Withdraw returned : {withdrawalRefused}");

            //sara.ApplyMonthlyFee();
            //ahmed.ApplyMonthlyFee(15);

            //sara.TransferTo(ahmed, 500.00m);

            //Console.WriteLine(sara.GetSummary());
            //Console.WriteLine(ahmed.GetSummary());

            //layla.SetOwnerName("Layla Mahmoud");
            //Console.WriteLine(layla.GetSummary());
            //Console.WriteLine(BankAccount.GetBankInfo());
            /////===============================================
            ////sara.Balance = -999999.99m;
            //Console.WriteLine(sara.GetSummary());

            ////sara.AccountNumber = 7;
            //Console.WriteLine(sara.GetSummary());

            //// Validation and setters
            //try
            //{
            //    sara.Deposit(-500.00m);

            //}
            //catch (ArgumentOutOfRangeException ex)
            //{
            //    Console.WriteLine($"REFUSED -> {ex.Message}");
            //}
            //Console.WriteLine(sara.GetSummary());

            //=======================================

            //Console.WriteLine($"Layla Balance {layla.Balance:F2} | Available {layla.AvailableBalance:F2} | On Hold? {layla.HasFundsOnHold} | Low? {layla.IsLowBalance}");

            //StaleBalanceReport report = new(layla);
            //Console.WriteLine(report.GetLine());

            //layla.HoldFunds(600.00m);
            //Console.WriteLine($"Layla Balance {layla.Balance:F2} | Available {layla.AvailableBalance:F2} | On Hold? {layla.HasFundsOnHold} | Low? {layla.IsLowBalance}");

            //layla.ReleaseHold(600.00m);
            //layla.withdraw(900.00m);

            //Console.WriteLine(report.GetLine());
            //Console.WriteLine($"[computed live] #{layla.AccountNumber} | {layla.OwnerName} | {layla.Balance:F2} | low? {layla.IsLowBalance}");
            //===========================================
            SavingsAccount laylaSavings = new SavingsAccount("Layla Mahmoud", 2000.00m, 6.00m);
            Console.WriteLine(laylaSavings.AccountNumber);        // 1004
            Console.WriteLine(laylaSavings.Currency);             // EGP
            Console.WriteLine(laylaSavings.AvailableBalance);     // 2000.00
            Console.WriteLine(BankAccount.BankName);              // Cairo Bank
            laylaSavings.withdraw(500.00m);   // works — 2500.00 -> 2000.00

            laylaSavings.Deposit(500.00m);    // works — 2000.00 -> 2500.00
            //laylaSavings.Deposit(-1.00m); // error work it -

            laylaSavings.ShowInterestPreview();

            SavingsAccount saraSaving = new SavingsAccount(sara.OwnerName, sara.AvailableBalance, 18.00m);
            saraSaving.ShowInterestPreview();
            //virtual & override
            sara.ApplyMonthlyFee();  // ordinary account
            laylaSavings.ApplyMonthlyFee();  // savings account
            //Checking Account
            CheckingAccount nadaAccount = new CheckingAccount("Nada", 1000.00m, 500.00m);
            Console.WriteLine($"Account Created for: {nadaAccount.OwnerName}");
            Console.WriteLine($"Initial balance: {nadaAccount.Balance} EGP");
            Console.WriteLine($"Overdraft Limit: {nadaAccount.OverdraftLimit} EGP\n");
            Console.WriteLine("----------------------------------\n");
            Console.WriteLine("--> operation 1: Withdrawing 1300 EGP...");
            nadaAccount.withdraw(1300.00m);
            Console.WriteLine($"New Balance: {nadaAccount.Balance} EGP");
            Console.WriteLine("--------------------------");
            Console.WriteLine("--> operation 2: Trying to withdraw another 300 EGP...");
            nadaAccount.withdraw(400.00m);
            Console.WriteLine($"New Balance: {nadaAccount.Balance} EGP");
            Console.WriteLine("--------------------------");
            Console.WriteLine("--> Operation 3: Depositing 500 EGP to cover overdraft...");
            nadaAccount.Deposit(500.00m, "Covering overdraft balance");
            Console.WriteLine($"Final Balance: {nadaAccount.Balance} EGP");
            //============================================
            SavingsAccount asSavings = laylaSavings;
            BankAccount asAccount = laylaSavings;   // the SAME object, parent-typed variable

            Console.WriteLine(asSavings.GetSummary());
            Console.WriteLine(asAccount.GetSummary());
            //=============================
            FixedDepositAccount nourFixed = new FixedDepositAccount("Nour Adel", 5000.00m, 9.00m, 12);
            Console.WriteLine(nourFixed.GetSummary());
        }
    }
}
