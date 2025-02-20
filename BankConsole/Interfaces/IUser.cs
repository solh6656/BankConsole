using BankConsole.Enums;
using System;

namespace BankConsole.Interfaces
{
    internal interface IUser
    {
        string Name { get; }
        UserRole Role { get; }
    }
}
