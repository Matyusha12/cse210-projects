using System;


class Entry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }


    public Entry(string prompt, string response, string date)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
    }

  
    public override string ToString()
    {
        return $"{Date} - {Prompt}: {Response}";
    }
}

class Program
{

    static List<Entry> journal = new List<Entry>();

    static void Main(string[] args)
    {
        bool running = true;

       
        while (running)
        {
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Quit");
            Console.Write(" What would you like to do? ");

            string option = Console.ReadLine();

            
            switch (option)
            {
                case "1":
                    WriteNewEntry();
                    break;
                case "2":
                    DisplayJournal();
                    break;
                case "3":
                    SaveJournal();
                    break;
                case "4":
                    LoadJournal();
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }


    static void WriteNewEntry()
    {
       
        string[] prompts = { "Favorite memory today", "Lesson learned today", "Gratitude moment", "Personal reflection", "Goal for tomorrow" };
        Random random = new Random();
        int index = random.Next(prompts.Length);

       
        Console.WriteLine(prompts[index]);
        string response = Console.ReadLine();
        string date = DateTime.Now.ToShortDateString();

        Entry entry = new Entry(prompts[index], response, date);
        journal.Add(entry);
    }


    static void DisplayJournal()
    {
        foreach (Entry entry in journal)
        {
            Console.WriteLine(entry);
        }
    }

    static void SaveJournal()
    {
        Console.Write("Enter a filename to save: ");
        string filename = Console.ReadLine();

        
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Entry entry in journal)
            {
                writer.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
            }
        }
    }

    static void LoadJournal()
    {
        Console.Write("Enter a filename to load: ");
        string filename = Console.ReadLine();

        
        journal.Clear();

       
        string[] lines = File.ReadAllLines(filename);
        foreach (string line in lines)
        {
            string[] parts = line.Split('|');
            Entry entry = new Entry(parts[1], parts[2], parts[0]);
            journal.Add(entry);
        }
    }
}