using System;

namespace BankConsole.Interfaces
{
    internal interface IAccountOperations
    {
        void Deposit(decimal amount);
        bool Withdraw(decimal amount);
    }
}
