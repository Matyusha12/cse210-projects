
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public void RecordEvent(string goalName)
    {
        var goal = _goals.FirstOrDefault(g => g.ShortName == goalName);
        if (goal != null)
        {
            goal.RecordEvent();
            Console.WriteLine($"Event recorded for '{goalName}'. Points: {goal.Points}");
        }
        else
        {
            Console.WriteLine("Goal not found.");
        }
    }

    public void DisplayGoals()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals to display.");
            return;
        }

        foreach (var goal in _goals)
        {
            Console.WriteLine(goal);
        }
    }

    public void SaveGoals(string filename)
    {
        using (StreamWriter file = new StreamWriter(filename))
        {
            foreach (var goal in _goals)
            {
                file.WriteLine(goal.GetStringRepresentation());
            }
        }
    }

    public void LoadGoals(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine("Save file not found.");
            return;
        }

        _goals.Clear();
        string[] lines = File.ReadAllLines(filename);
        foreach (var line in lines)
        {
            var data = line.Split(',');
            Goal goal = null;
            switch (data[0])
            {
                case "SimpleGoal":
                    goal = new SimpleGoal(data[1], data[2], Convert.ToInt32(data[3]));
                    break;
                case "EternalGoal":
                    goal = new EternalGoal(data[1], data[2], Convert.ToInt32(data[3]));
                    break;
                case "ChecklistGoal":
                    goal = new ChecklistGoal(data[1], data[2], Convert.ToInt32(data[3]), Convert.ToInt32(data[4]), Convert.ToInt32(data[5]));
                    break;
            }
            if (goal != null)
            {
                _goals.Add(goal);
            }
        }
    }
}


public abstract class Goal
{
    public string ShortName { get; protected set; }
    public string Description { get; protected set; }
    public int Points { get; protected set; }

    protected Goal(string shortName, string description, int points)
    {
        ShortName = shortName;
        Description = description;
        Points = points;
    }

    public abstract void RecordEvent();
    public abstract string GetStringRepresentation();

    public override string ToString()
    {
        return $"{ShortName} - {Description}: {Points} points";
    }
}


public class SimpleGoal : Goal
{
    public SimpleGoal(string shortName, string description, int points)
        : base(shortName, description, points) { }

    public override void RecordEvent()
    {
        Points = 1000;} 

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal,{ShortName},{Description},{Points}";
    }
}


public class EternalGoal : Goal
{
    public EternalGoal(string shortName, string description, int points)
        : base(shortName, description, points) { }

    public override void RecordEvent()
    {
        Points += 100; 
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal,{ShortName},{Description},{Points}";
    }
}


public class ChecklistGoal : Goal
{
    private int _target;
    private int _bonus;
    private int _currentCount;

    public ChecklistGoal(string shortName, string description, int points, int target, int bonus)
        : base(shortName, description, points)
    {
        _target = target;
        _bonus = bonus;
        _currentCount = 0;
    }

    public override void RecordEvent()
    {
        if (_currentCount < _target)
        {
            _currentCount++;
            Points += 50; 
            if (_currentCount == _target)
            {
                Points += _bonus; 
            }
        }
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal,{ShortName},{Description},{Points},{_target},{_bonus},{_currentCount}";
    }
}


class Program
{
    static GoalManager goalManager = new GoalManager();
    static string filePath = "goals.txt";

    static void Main()
    {
        LoadGoals();  

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nMenu options:");
            Console.WriteLine("1. Create a Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option: ");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    ListGoals();
                    break;
                case "3":
                    SaveGoals();
                    break;
                case "4":
                    LoadGoals();
                    break;
                case "5":
                    RecordEvent();
                    break;
                case "6":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    static void CreateGoal()
    {
        Console.WriteLine("\nWhat type of goal would you like to create?");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Choose a goal type: ");
        
        string goalType = Console.ReadLine();
        Console.Write("What is the name of the goal? ");
        string name = Console.ReadLine();
        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine();
        Console.Write("What is the amount of points associated with it? ");
        int points = Convert.ToInt32(Console.ReadLine());

        switch (goalType)
        {
            case "1":
                goalManager.AddGoal(new SimpleGoal(name, description, points));
                break;
            case "2":
                goalManager.AddGoal(new EternalGoal(name, description, points));
                break;
            case "3":
                Console.Write("What is the target count for this goal? ");
                int target = Convert.ToInt32(Console.ReadLine());
                Console.Write("What is the bonus points for completing this goal? ");
                int bonus = Convert.ToInt32(Console.ReadLine());
                goalManager.AddGoal(new ChecklistGoal(name, description, points, target, bonus));
                break;
            default:
                Console.WriteLine("Invalid goal type.");
                break;
        }
    }

    static void ListGoals()
    {
        Console.WriteLine();
        goalManager.DisplayGoals();
    }

    static void SaveGoals()
    {
        goalManager.SaveGoals(filePath);
        Console.WriteLine("Goals saved successfully.");
    }

    static void LoadGoals()
    {
        goalManager.LoadGoals(filePath);
        Console.WriteLine("Goals loaded successfully.");
    }

    static void RecordEvent()
    {
        Console.Write("Enter the name of the goal to record: ");
        string name = Console.ReadLine();
        goalManager.RecordEvent(name);
    }
}

