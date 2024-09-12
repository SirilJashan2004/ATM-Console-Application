using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Atm_Machine_OPPS
{
   public class Account_Detail
    {
        private string _AccountNo;
        private int _Pin;
        private double _Balance;
        string _CurrentAccountPath;
        private List<string>_Transaction;

        public Account_Detail(string Path,string accountno, int pin, double balance,List<string>Transaction_History)
        {
            _CurrentAccountPath = Path;
            _AccountNo = accountno;
            _Pin = pin;
            _Balance = balance;
            _Transaction = Transaction_History;
        }

        public void LoadData()
        {
            StreamWriter write = new StreamWriter(_CurrentAccountPath);
            string WriteContent=_AccountNo+","+_Pin+","+_Balance+"|";
            write.WriteLine(WriteContent);
            foreach (var item in _Transaction)
            {
                write.WriteLine(item+";");
            }
            write.Close();
        }
        public bool ValidatePin(int pin)
        {
            return _Pin == pin;
        }

        public void Deposit(double amount)
        {
            _Balance += amount;
            string prompt = amount + " deposited successfully.";
            _Transaction.Add(prompt);
            Console.WriteLine(prompt);
        }

        public void Withdraw(double amount)
        {
            if (amount > _Balance)
            {
                Console.WriteLine("Insufficient balance.");
                return;
            }

            _Balance -= amount;
            string prompt = amount + " withdrawn successfully.";
            _Transaction.Add(prompt);
            Console.WriteLine(prompt);
        }

        public void PinChange(int newPin)
        {
            if (_Pin == newPin)
            {
                Console.WriteLine("You Entered Older Pin");
                return;
            }
            _Pin = newPin;
            string prompt = "Pin changed successfully.";
            Console.WriteLine(prompt);
        }

        public void MiniStatement()
        {
            if (_Transaction.Count == 0)
            {
                Console.WriteLine("There are no transactions yet.");
                return;
            }

            Console.WriteLine("Mini Statement:");
            foreach (var item in _Transaction)
            {
                Console.WriteLine(item);
            }
        }

        public void CheckBalance()
        {
            Console.WriteLine("Balance Amount: " + _Balance);
        }

        public string GetAccountNo()
        {
            return _AccountNo;
        }
    }
}
