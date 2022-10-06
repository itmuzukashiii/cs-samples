using System;
using System.Threading.Tasks;

namespace LockTest;

class AccountTest
{
    static async Task Main(string[] args)
    {
        var account = new Account(1000);
        var tasks = new Task[100];
        for (int k = 0; k < tasks.Length; k++) {
            var msg = $"task{k}";
            tasks[k] = Task.Run(() => updateAccount(account, msg));
        }
        await Task.WhenAll(tasks);
        Console.WriteLine($"Account's balance is '{account.GetBalance()}'.");
    }

    private static void updateAccount(Account account, string message = "")
    {
        decimal[] amounts = { 0, 2, -3, 6, -2, -1, 8, -5, 11, -6 };
        foreach (var amount in amounts) {
            if (amount >= 0)
                account.Credit(amount);
            else
                account.Debit(Math.Abs(amount));
            Console.WriteLine($"DEBUG: {message}: balance = {account.GetBalance()} ({amount})");
        }
    }
}

class Account
{
    private readonly object balanceLock = new object();
    private decimal balance;

    //--- 口座解説と初回入金
    public Account(decimal initialBalance)
    {
        this.balance = initialBalance;
    }

    //--- 残高照会
    public decimal GetBalance()
    {
        lock (balanceLock) {
            return this.balance;
        }
    }

    //--- 出金
    public decimal Debit(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), @"The debit amount cannot be negative.");
        
        decimal appliedAmount = 0;
        lock (balanceLock)
        {
            if (this.balance >= amount) {
                this.balance -= amount;
                appliedAmount = amount;
            }
        }
        return appliedAmount;
    }

    //--- 入金
    public void Credit(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), @"The credit amount cannot be negative.");

        lock (balanceLock) {
            this.balance += amount;
        }        
    }
}

/*
実行結果:
DEBUG: task6: balance = 1000 (0)
DEBUG: task6: balance = 1002 (2)
DEBUG: task0: balance = 1000 (0)
DEBUG: task4: balance = 1000 (0)
DEBUG: task3: balance = 1000 (0)

…

DEBUG: task93: balance = 2006 (11)
DEBUG: task99: balance = 2001 (-5)
DEBUG: task93: balance = 1995 (-6)
DEBUG: task99: balance = 2006 (11)
DEBUG: task99: balance = 2000 (-6)
Account's balance is '2000'.


参考: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock
*/