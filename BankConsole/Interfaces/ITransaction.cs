using BankConsole.Models;
using System;

namespace BankConsole.Interfaces
{
    internal interface ITransaction
    {
        bool Transfer(BankAccount fromAccount, BankAccount toAccount, decimal amount);
    }
}
