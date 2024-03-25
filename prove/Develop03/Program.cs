using System;

class Reference
{
    public string Book { get; set; }
    public int Chapter { get; set; }
    public int StartVerse { get; set; }
    public int? EndVerse { get; set; }

    public Reference(string book, int chapter, int startVerse)
    {
        Book = book;
        Chapter = chapter;
        StartVerse = startVerse;
    }

    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        Book = book;
        Chapter = chapter;
        StartVerse = startVerse;
        EndVerse = endVerse;
    }

    public override string ToString()
    {
        return EndVerse == null ? $"{Book} {Chapter}:{StartVerse}" : $"{Book} {Chapter}:{StartVerse}-{EndVerse}";
    }
}

class Scripture
{
    public Reference Reference { get; set; }
    public string Text { get; set; }

    public Scripture(Reference reference, string text)
    {
        Reference = reference;
        Text = text;
    }
}

class Word
{
    public string Text { get; set; }
    public bool IsVisible { get; set; }

    public Word(string text)
    {
        Text = text;
        IsVisible = true;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Reference reference = new Reference("John", 3, 16);
        Scripture scripture = new Scripture(reference, "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life.");
        List<Word> words = new List<Word>();

        foreach (string wordText in scripture.Text.Split(' '))
        {
            words.Add(new Word(wordText));
        }

        Random random = new Random();
        bool allWordsHidden = false;

        while (!allWordsHidden)
        {
            Console.Clear();
            Console.WriteLine(scripture.Reference);

            foreach (Word word in words)
            {
                Console.Write(word.IsVisible ? word.Text + " " : new string('_', word.Text.Length) + " ");
            }
            Console.WriteLine("\nPress Enter to hide a word, or type 'quit' to exit.");

            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
            {
                break;
            }

            int indexToHide = random.Next(words.Count);
            words[indexToHide].IsVisible = false;

            allWordsHidden = true;
            foreach (Word word in words)
            {
                if (word.IsVisible)
                {
                    allWordsHidden = false;
                    break;
                }
            }
        }
    }
}