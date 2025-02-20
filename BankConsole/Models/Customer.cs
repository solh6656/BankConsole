using BankConsole.Enums;
using BankConsole.Interfaces;
using System;
namespace BankConsole.Models
{
    internal class Customer : IUser
    {
        public string Name { get; }
        public UserRole Role { get; }
        public BankAccount Account { get; }

        public Customer(string name, UserRole role, BankAccount account)
        {
            Name = name;
            Role = role;
            Account = account;
        }
    }
}
