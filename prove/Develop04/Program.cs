using System;

abstract class Activity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }

    public Activity(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void StartActivity()
    {
        Console.WriteLine($"Activity: {Name}\nDescription: {Description}");
        Console.Write("Enter the duration in seconds: ");
        Duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Get ready...");
        Thread.Sleep(3000);
    }

    public abstract void RunActivity();

    public void EndActivity()
    {
        Console.WriteLine("Well done!");
        Thread.Sleep(2000);
        Console.WriteLine($"You have completed the {Name} activity for {Duration} seconds.");
        Thread.Sleep(3000);
    }
}

class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly.")
    {
    }

    public override void RunActivity()
    {
        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        while (DateTime.Now < endTime)
        {
            Console.WriteLine("Breathe in...");
            Thread.Sleep(3000);
            Console.WriteLine("Breathe out...");
            Thread.Sleep(3000);
        }
    }
}

class ReflectionActivity : Activity
{
    public ReflectionActivity() : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience.")
    {
    }

    public override void RunActivity()
    {
        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        Random random = new Random();
        List<string> prompts = new List<string>()
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };
        string selectedPrompt = prompts[random.Next(prompts.Count)];
        Console.WriteLine(selectedPrompt);

        while (DateTime.Now < endTime)
        {
            Thread.Sleep(3000); 
            Console.Write(".");
        }
    }
}

class ListingActivity : Activity
{
    public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    public override void RunActivity()
    {
        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        Random random = new Random();
        List<string> prompts = new List<string>()
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };
        string selectedPrompt = prompts[random.Next(prompts.Count)];
        Console.WriteLine(selectedPrompt);

        int count = 0;
        while (DateTime.Now < endTime)
        {
            Console.Write("List item: ");
            string item = Console.ReadLine();
            count++;
        }
        Console.WriteLine($"You listed {count} items.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Start breathing activity");
            Console.WriteLine("2. Start reflection activity");
            Console.WriteLine("3. Start listing activity");
            Console.WriteLine("4. Quit");
            Console.Write(" Select a choice from the menu: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Activity breathingActivity = new BreathingActivity();
                    breathingActivity.StartActivity();
                    breathingActivity.RunActivity();
                    breathingActivity.EndActivity();
                    break;
                case "2":
                    Activity reflectionActivity = new ReflectionActivity();
                    reflectionActivity.StartActivity();
                    reflectionActivity.RunActivity();
                    reflectionActivity.EndActivity();
                    break;
                case "3":
                    Activity listingActivity = new ListingActivity();
                    listingActivity.StartActivity();
                    listingActivity.RunActivity();
                    listingActivity.EndActivity();
                    break;
                case "4":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}