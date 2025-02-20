using BankConsole.Interfaces;
using System;

namespace BankConsole.Models
{
    internal class BankAccount : IAccountOperations
    {
        public string AccountNumber { get; }
        public decimal Balance { get; private set; }

        public BankAccount(string accountNumber, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
        }

        public void Deposit(decimal amount) => Balance += amount;

        public bool Withdraw(decimal amount)
        {
            if (amount <= Balance)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }
    }
}
