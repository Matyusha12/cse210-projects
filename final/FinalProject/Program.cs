using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Budget budget = new Budget();
        
        Console.WriteLine("Personal Finance Manager!");

   
        bool addMoreIncome = true;
        while (addMoreIncome)
        {
            Console.Write("Enter the source of income: ");
            string source = Console.ReadLine();

            Console.Write("Enter the amount of income: ");
            double amount = double.Parse(Console.ReadLine());

            Transaction income = new Income(source, amount);
            budget.AddTransaction(income);

            Console.Write("Do you want to add more income? (yes/no): ");
            string response = Console.ReadLine().ToLower();
            addMoreIncome = (response == "yes");
        }

       
        bool addMoreExpenses = true;
        while (addMoreExpenses)
        {
            Console.Write("Enter the category of expense: ");
            string category = Console.ReadLine();

            Console.Write("Enter the amount of expense: ");
            double amount = double.Parse(Console.ReadLine());

            Transaction expense = new Expense(category, amount);
            budget.AddTransaction(expense);

            Console.Write("Do you want to add more expenses? (yes/no): ");
            string response = Console.ReadLine().ToLower();
            addMoreExpenses = (response == "yes");
        }

        Console.WriteLine($"Total Income: {budget.GetTotalIncome()}");
        Console.WriteLine($"Total Expenses: {budget.GetTotalExpenses()}");
        Console.WriteLine($"Net Savings: {budget.GetNetSavings()}");
    }
}

abstract class Transaction
{
    public string Description { get; set; }
    public double Amount { get; set; }

    protected Transaction(string description, double amount)
    {
        Description = description;
        Amount = amount;
    }
}

class Income : Transaction
{
    public Income(string source, double amount) : base(source, amount) { }
}

class Expense : Transaction
{
    public Expense(string category, double amount) : base(category, amount) { }
}

class Budget
{
    private List<Transaction> transactions = new List<Transaction>();

    public void AddTransaction(Transaction transaction)
    {
        transactions.Add(transaction);
    }

    public double GetTotalIncome()
    {
        double total = 0;
        foreach (Transaction transaction in transactions)
        {
            if (transaction is Income)
            {
                total += transaction.Amount;
            }
        }
        return total;
    }

    public double GetTotalExpenses()
    {
        double total = 0;
        foreach (Transaction transaction in transactions)
        {
            if (transaction is Expense)
            {
                total += transaction.Amount;
            }
        }
        return total;
    }

    public double GetNetSavings()
    {
        return GetTotalIncome() - GetTotalExpenses();
    }
}
