using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Atm_Machine_OPPS
{
    class ATM
    {
        private List<Account_Detail> accounts;
        List<string> AccountPath = new List<string>();
        private List<string[]> ListofAccounts = new List<string[]>();
        string Path = @"D:\Root IT Task\Atm_Machine_OPPS\ATM_File_Project\";

        public ATM()
        {
            accounts = new List<Account_Detail>();
            Account_File_Path();
            load_Accounts();
        }

        public void Start()
        {
            Console.WriteLine("Welcome to the ATM!");

            Console.Write("Enter Account Number: ");
            string accountNo = Console.ReadLine();

            Account_Detail CurrentAccount = null;
            foreach (var acc in accounts)
            {
                if (acc.GetAccountNo() == accountNo)
                {
                    CurrentAccount = acc;
                    break;
                }
            }

            if (CurrentAccount == null)
            {
                Console.WriteLine("Invalid CurrentAccount number.");
                return;
            }

            Console.Write("Enter PIN: ");
            int pin;
            if (!int.TryParse(Console.ReadLine(), out pin) || !CurrentAccount.ValidatePin(pin))
            {
                Console.WriteLine("Invalid PIN.");
                return;
            }

            while (true)
            {
                Console.WriteLine("\nSelect Operation:");
                Console.WriteLine("1. Deposit");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Change PIN");
                Console.WriteLine("4. Mini Statement");
                Console.WriteLine("5. Check Balance");
                Console.WriteLine("6. Exit");

                Console.Write("Enter your choice: ");
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter amount to deposit: ");
                        double depositAmount;
                        if (double.TryParse(Console.ReadLine(), out depositAmount))
                        {
                            CurrentAccount.Deposit(depositAmount);
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount.");
                        }
                        break;

                    case 2:
                        Console.Write("Enter amount to withdraw: ");
                        double withdrawAmount;
                        if (double.TryParse(Console.ReadLine(), out withdrawAmount))
                        {
                            CurrentAccount.Withdraw(withdrawAmount);
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount.");
                        }
                        break;

                    case 3:
                        Console.Write("Enter new PIN: ");
                        int newPin;
                        if (int.TryParse(Console.ReadLine(), out newPin))
                        {
                            CurrentAccount.PinChange(newPin);
                        }
                        else
                        {
                            Console.WriteLine("Invalid PIN format.");
                        }
                        break;

                    case 4:
                        CurrentAccount.MiniStatement();
                        break;

                    case 5:
                        CurrentAccount.CheckBalance();
                        break;

                    case 6:
                        CurrentAccount.LoadData();
                        Console.WriteLine("Thank you for using the ATM.");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        void load_Accounts()
        {
            foreach (var item in AccountPath)
            {
                string CurrentAccPath = Path + item;
                string line = String.Empty;
                string AllContent = File.ReadAllText(CurrentAccPath);
                string[] SeperateContent = AllContent.Split('|');

                string AC_Details = SeperateContent[0];
                string[] GetAccountDetails = AC_Details.Split(',');

                string Transaction_Details = SeperateContent[1];
                string[] Statements = Transaction_Details.Split(';');

                List<string> Trasaction_History = new List<string>(Statements);
                accounts.Add(new Account_Detail(CurrentAccPath, GetAccountDetails[0], int.Parse(GetAccountDetails[1]), double.Parse(GetAccountDetails[2]), Trasaction_History));
            }
        }
        void Account_File_Path()
        {
            AccountPath.Add("Account_1\\908070_Account.txt");
            AccountPath.Add("Account_2\\807060_Account.txt");
            AccountPath.Add("Account_3\\706050_Account.txt");
            AccountPath.Add("Account_4\\605040_Account.txt");
            AccountPath.Add("Account_5\\504030_Account.txt");
        }
    }
}

