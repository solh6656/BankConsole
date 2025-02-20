using BankConsole.Enums;
using BankConsole.Interfaces;
using System;

namespace BankConsole.Models
{
    internal class Bank : ITransaction
    {
        private List<Customer> customers = new List<Customer>()
        {
            new ("AdminUser", UserRole.Admin, new BankAccount("ACC1", 100000)),
            new ("RegularUser", UserRole.User, new BankAccount("ACC2", 100000)),
            new ("User1", UserRole.User, new BankAccount("ACC3", 100000)),
            new ("User2", UserRole.User, new BankAccount("ACC4", 100000)),
            new ("User3", UserRole.User, new BankAccount("ACC5", 100000))
            
        };
        public void InitializeData()
        {
            Transfer(customers[1].Account, customers[2].Account, 1000);
            Transfer(customers[3].Account, customers[4].Account, 500);
            Transfer(customers[2].Account, customers[1].Account, 700);
            Transfer(customers[4].Account, customers[3].Account, 1200);
            Transfer(customers[1].Account, customers[3].Account, 600);
        }


        public void Run()
        {
            Console.Clear();
            Console.WriteLine("Bank tizimiga xush kelibsiz! Kirish uchun ismingizni kiriting:");
            string name = Console.ReadLine();
            Customer user = customers.Find(c => c.Name == name);
            if (user == null)
            {
                Console.WriteLine("Bunday foydalanuvchi topilmadi.");
                return;
            }
            ShowMenu(user);
        }

        private void ShowMenu(Customer user)
        {
            while (true)
            {
                //InitializeData();
                Console.Clear();
                Console.WriteLine("1. Balansni ko'rish");
                Console.WriteLine("2. Pul qo'yish");
                Console.WriteLine("3. Pul yechish");
                Console.WriteLine("4. Pul o'tkazish");
                if (user.Role == UserRole.Admin)
                {
                    Console.WriteLine("5. Barcha hisoblarni ko'rish");
                    Console.WriteLine("6. Yangi foydalanuvchi qo'shish");
                }
                Console.WriteLine("0. Chiqish");
                Console.Write("Tanlovingiz: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine($"Sizning balansingiz: {user.Account.Balance}");
                        break;
                    case "2":
                        Console.Clear();
                        Console.Write("Qo'yiladigan summani kiriting: ");
                        decimal depositAmount = decimal.Parse(Console.ReadLine());
                        user.Account.Deposit(depositAmount);
                        break;
                    case "3":
                        Console.Clear();
                        Console.Write("Yechib olinadigan summani kiriting: ");
                        decimal withdrawAmount = decimal.Parse(Console.ReadLine());
                        if (!user.Account.Withdraw(withdrawAmount))
                            Console.WriteLine("Yetarli mablag' yo'q!");
                        break;
                    case "4":
                        Console.Clear();
                        Console.Write("Pul o'tkazish uchun qabul qiluvchi hisob raqamini kiriting: ");
                        string targetAccount = Console.ReadLine();
                        Customer receiver = customers.Find(c => c.Account.AccountNumber == targetAccount);
                        if (receiver == null)
                        {
                            Console.WriteLine("Bunday hisob topilmadi.");
                            break;
                        }
                        Console.Write("O'tkazma miqdorini kiriting: ");
                        decimal transferAmount = decimal.Parse(Console.ReadLine());
                        if (!Transfer(user.Account, receiver.Account, transferAmount))
                        {
                            Console.WriteLine("O'tkazma amalga oshmadi.");
                        }
                        break;
                    case "5":
                        if (user.Role == UserRole.Admin)
                        {
                            Console.Clear();
                            Console.WriteLine("Barcha mijozlar:");
                            foreach (var customer in customers)
                            {
                                Console.WriteLine($"{customer.Name} - {customer.Account.AccountNumber} - {customer.Account.Balance}");
                            }
                        }
                        break;
                    case "6":
                        if (user.Role == UserRole.Admin)
                        {
                            Console.Clear();
                            Console.Write("Foydalanuvchi ismini kiriting: ");
                            string newName = Console.ReadLine();
                            Console.Write("Rolni tanlang (1 - User, 2 - Admin): ");
                            UserRole newRole = Console.ReadLine() == "2" ? UserRole.Admin : UserRole.User;
                            string newAccountNumber = "ACC" + (customers.Count + 1);
                            customers.Add(new Customer(newName, newRole, new BankAccount(newAccountNumber, 0)));
                            Console.WriteLine("Foydalanuvchi muvaffaqiyatli qo'shildi!");
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Noto'g'ri tanlov, qaytadan urinib ko'ring!");
                        break;
                }
                Console.WriteLine("Davom etish uchun istalgan tugmani bosing...");
                Console.ReadKey();
            }
        }

        public bool Transfer(BankAccount fromAccount, BankAccount toAccount, decimal amount)
        {
            if (fromAccount.Withdraw(amount))
            {
                toAccount.Deposit(amount);
                return true;
            }
            return false;
        }
    }
}
