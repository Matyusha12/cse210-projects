using System;

// Entry class to represent a journal entry
class Entry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }

    //Entry class
    public Entry(string prompt, string response, string date)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
    }

    // ToString method
    public override string ToString()
    {
        return $"{Date} - {Prompt}: {Response}";
    }
}

class Program
{
    // Create a list to store the journal entries
    static List<Entry> journal = new List<Entry>();

    static void Main(string[] args)
    {
        bool running = true;

        // The program menu
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

            // Switch statement to handle menu options
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

    // Write a new journal entry
    static void WriteNewEntry()
    {
        // An array of prompts
        string[] prompts = { "Favorite memory today", "Lesson learned today", "Gratitude moment", "Personal reflection", "Goal for tomorrow" };
        Random random = new Random();
        int index = random.Next(prompts.Length);

        // Show a random prompt
        Console.WriteLine(prompts[index]);
        string response = Console.ReadLine();
        string date = DateTime.Now.ToShortDateString();

        // Create a new Entry object and add it to the journal
        Entry entry = new Entry(prompts[index], response, date);
        journal.Add(entry);
    }

    //Display all journal entries
    static void DisplayJournal()
    {
        foreach (Entry entry in journal)
        {
            Console.WriteLine(entry);
        }
    }

    // Save the journal to a file
    static void SaveJournal()
    {
        Console.Write("Enter a filename to save: ");
        string filename = Console.ReadLine();

        // Use StreamWriter to write entries to a file
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Entry entry in journal)
            {
                writer.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
            }
        }
    }

    // Method to load the journal from a file
    static void LoadJournal()
    {
        Console.Write("Enter a filename to load: ");
        string filename = Console.ReadLine();

        // Clear the current journal before loading new entries
        journal.Clear();

        // Read the file and create Entry objects for each line
        string[] lines = File.ReadAllLines(filename);
        foreach (string line in lines)
        {
            string[] parts = line.Split('|');
            Entry entry = new Entry(parts[1], parts[2], parts[0]);
            journal.Add(entry);
        }
    }
}