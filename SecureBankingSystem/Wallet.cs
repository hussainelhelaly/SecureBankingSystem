using System;


namespace SecureBankingSystem
{
    public class Wallet
    {
        public string OwnerId;
        public decimal Balance;

        public Wallet(string ownerId, decimal balance)
        {
            OwnerId = ownerId;
            Balance = balance;
        }
    }
}
