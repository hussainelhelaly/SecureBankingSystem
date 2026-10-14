using System;


namespace SecureBankingSystem
{
    public class BankAccount //PascalCase
    {
        //  public int AccountNumber; //Field
        private decimal _balance;  // Backing field
        public int AccountNumber {  get; private set; }  //Property (Auto-property)
        public  string OwnerName { get; private set; }
        public static readonly decimal MinimumOpeningBalance = 500.00m;

        public string Currency { get; } = "EGP";


        private decimal _heldAmount;
        public decimal HeldAmount => _heldAmount;  //expression-bodied property (Computed Property)
        public decimal AvailableBalance { get { return Balance - HeldAmount; } }
        public bool HasFundsOnHold { get { return HeldAmount > 0m; } }
        public bool IsLowBalance { get { return AvailableBalance < MinimumOpeningBalance; } }


        public const string BankName = "Cairo Bank";


        public static int TotalAccountOpened;
        private static int _nextAccountNumber;





        public decimal Balance  // Full Property
        { 
            get {  return _balance; }
           private set 
            {
                if(value <MinimumAllowedBalance)
                {
                    throw new ArgumentOutOfRangeException(
                             nameof(value),
                             $"Balance cannot be less than the minimum allowed balance ({MinimumAllowedBalance:F2} {Currency}). Refused {value:F2} {Currency}"
                     );
                }
                _balance = value;
            }
        }
        // Constructor

        public BankAccount(string ownerName, decimal openingBalance = 500.00m)
        {
            if(string.IsNullOrWhiteSpace(ownerName))
            {
                throw new ArgumentException(nameof(ownerName));
            }

            if(openingBalance < MinimumOpeningBalance)
            {
                throw new ArgumentOutOfRangeException(
                    $"{BankName} requires a minimum opening balance of {MinimumOpeningBalance:F2} {Currency} . Refused {openingBalance:F2} {Currency}"
                    );
            }


            AccountNumber = _nextAccountNumber;
            _nextAccountNumber++;
            TotalAccountOpened++;

            OwnerName = ownerName;
            Balance = openingBalance;
        }
        // Another constrain depends on constructor
        public BankAccount(string ownerName) : this(ownerName , 500.00m)
        {
        }


        static BankAccount()
        {
            _nextAccountNumber = 1001;
        }

        //Method Overloading
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(amount),
                    $"A Deposit must be a positive amount. Refused {amount:F2} {Currency}"
                 );
            }
            Balance += amount;
            Console.WriteLine($"Deposited {amount:F2} EGP into account {AccountNumber} New Balance: {Balance:F2} EGP");
        }

        public void Deposit(decimal amount , string note)
        {
            Deposit(amount);
            Console.WriteLine($"Note: {note}");
        }

        public virtual void ApplyMonthlyFee(decimal fee = 5.00m) // optional parameter
        {
            if (fee > Balance) 
            {
                Console.WriteLine($"Monthly fee of {fee:F2} {Currency} waived on account {AccountNumber} - balance {Balance:F2} {Currency} is too low");
                return;
            }
            Balance -= fee;
            Console.WriteLine($"Monthly fee of {fee:F2} EGP applied to account {AccountNumber} New Balance: {Balance:F2} EGP");
        }

        public virtual void RunMonthEnd()
        {
            Console.WriteLine($"  [BankAccount] month-end for #{AccountNumber} ({OwnerName})");
            ApplyMonthlyFee();
        }


        public bool withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine($"Amount must be positive");
                return false;
            }
            if (amount > Balance - MinimumAllowedBalance) 
            {
                Console.WriteLine($"Withdrawal of {amount:F2} EGP refused..");
                return false;
            }
            Balance -= amount;
            Console.WriteLine($"Withdrew {amount:F2} EGP...");
            return true;
        }

        public void  HoldFunds(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount) , $"Hold Amount must be positive");
            }

            if(amount > Balance)
            {
                Console.WriteLine($"Hold of {amount:F2} {Currency} refused on account {AccountNumber} - only {Balance} {Currency} available");
                return;
            }
            _heldAmount += amount;
            Console.WriteLine($"Held {amount:F2} {Currency} on account {AccountNumber}. Available: {AvailableBalance:F2} {Currency}");
        }

        public void ReleaseHold(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), $"Release Amount must be positive");
            }
            if (amount > _heldAmount)
            {
                Console.WriteLine($"Release of {amount:F2} {Currency}. Refused");
            }
            _heldAmount -= amount;
            Console.WriteLine($"Released {amount:F2} {Currency} on account {AccountNumber}. Available: {AvailableBalance:F2} {Currency}");
        }

        public static string GetBankInfo()
        {
            return $"#Bank Name: {BankName} | Minimum Balance: {MinimumOpeningBalance} EGP | account opened: {TotalAccountOpened}";
        }

        public virtual string GetSummary()
        {
            return $"#{AccountNumber} | {OwnerName} | {Balance:F2} EGP";
        }
        // This 
        public void TransferTo(BankAccount destination, decimal amount)
        {
            if (this.withdraw(amount))
            {
                destination.Deposit(amount, $"Transfer from account {this.AccountNumber}");
            }
        }
        public void SetOwnerName(string OwnerName)
        {
            this.OwnerName = OwnerName;
        }
        //==============================
        protected decimal CalculateMonthlyInterest(decimal annualRatePercent)
        {
            if (annualRatePercent < 0m)
            {
                throw new ArgumentOutOfRangeException(nameof(annualRatePercent), "An interest rate may not be negative.");
            }

            return Math.Round(Balance * annualRatePercent / 100m / 12m, 2);
        }

        protected virtual decimal MinimumAllowedBalance => 0.00m;

    }
}
