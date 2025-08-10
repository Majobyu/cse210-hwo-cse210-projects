// File: Program.cs
// This file contains the main program logic and a description of the enhancements.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Description of enhancements to exceed requirements:
// 1. Leveling System: The program now includes a leveling system.
//    As the user earns points, their level increases. The total score determines the level.
// 2. Level Up Celebration: Every time the user levels up, a celebration message is
//    displayed to reinforce positive behavior.
// 3. Expanded Player Info: The player's information now shows not only the
//    score but also the current level, making progress tracking more fun.
// 4. Encapsulation and Abstraction: Encapsulation has been improved by ensuring that
//    important member variables are private and accessed through public methods.

class Program
{
    static void Main(string[] args)
    {
        // An instance of the GoalManager class is created to manage the goals.
        GoalManager goalManager = new GoalManager();
        goalManager.Start();
    }
}

// File: GoalManager.cs
// This class handles the application's logic, including the menu,
// goal creation, event recording, and managing data saving and loading.

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;
    private int _level = 1;
    private int _pointsForNextLevel = 1000;

    // Starts the main program loop by displaying the menu.
    public void Start()
    {
        // The goals are initialized with some example values.
        _goals.Add(new SimpleGoal("Run a Marathon", "Run a full 42 km marathon.", 1000));
        _goals.Add(new EternalGoal("Read the Scriptures", "Read the scriptures every day.", 100));
        _goals.Add(new ChecklistGoal("Attend the Temple", "Attend the temple 10 times.", 50, 10, 500));

        bool running = true;
        while (running)
        {
            DisplayPlayerInfo();
            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. Quit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    ListGoalDetails();
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
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    // Displays the player's score and level.
    public void DisplayPlayerInfo()
    {
        CheckForLevelUp(); // Checks if the player should level up.
        Console.WriteLine($"Your current score is {_score} points.");
        Console.WriteLine($"Your current level is: {_level}");
        Console.WriteLine($"Points for the next level: {_pointsForNextLevel - _score}");
    }

    // Displays all goals with their full details.
    public void ListGoalDetails()
    {
        Console.WriteLine("The goals are:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    // Allows the user to create a new goal.
    public void CreateGoal()
    {
        Console.WriteLine("The types of goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        Console.Write("What type of goal would you like to create? ");
        string goalType = Console.ReadLine();

        Console.Write("What is the short name of your goal? ");
        string shortName = Console.ReadLine();
        Console.Write("What is a brief description of your goal? ");
        string description = Console.ReadLine();
        Console.Write("How many points is this goal worth? ");
        int points = int.Parse(Console.ReadLine());

        switch (goalType)
        {
            case "1":
                _goals.Add(new SimpleGoal(shortName, description, points));
                break;
            case "2":
                _goals.Add(new EternalGoal(shortName, description, points));
                break;
            case "3":
                Console.Write("How many times does this goal need to be completed for a bonus? ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("What is the bonus for completing the goal? ");
                int bonus = int.Parse(Console.ReadLine());
                _goals.Add(new ChecklistGoal(shortName, description, points, target, bonus));
                break;
        }
    }

    // Allows the user to record an event for a goal.
    public void RecordEvent()
    {
        Console.WriteLine("The goals are:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetShortName()}");
        }
        Console.Write("Which goal did you complete? ");
        int goalIndex = int.Parse(Console.ReadLine()) - 1;

        if (goalIndex >= 0 && goalIndex < _goals.Count)
        {
            int pointsEarned = _goals[goalIndex].RecordEvent();
            _score += pointsEarned;
            Console.WriteLine($"Congratulations! You have earned {pointsEarned} points.");
        }
        else
        {
            Console.WriteLine("Invalid goal index.");
        }
    }

    // Saves all goals and the score to a file.
    public void SaveGoals()
    {
        Console.Write("What is the filename for saving the goals? ");
        string filename = Console.ReadLine();

        try
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.WriteLine(_score);
                sw.WriteLine(_level);
                sw.WriteLine(_pointsForNextLevel);

                foreach (Goal goal in _goals)
                {
                    sw.WriteLine(goal.GetStringRepresentation());
                }
            }
            Console.WriteLine("Goals saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving the file: {ex.Message}");
        }
    }

    // Loads goals and the score from a file.
    public void LoadGoals()
    {
        Console.Write("What is the filename for loading the goals? ");
        string filename = Console.ReadLine();

        if (File.Exists(filename))
        {
            _goals.Clear();
            string[] lines = File.ReadAllLines(filename);

            _score = int.Parse(lines[0]);
            _level = int.Parse(lines[1]);
            _pointsForNextLevel = int.Parse(lines[2]);

            for (int i = 3; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(':');
                string goalType = parts[0];
                string[] goalData = parts[1].Split(',');

                switch (goalType)
                {
                    case "SimpleGoal":
                        _goals.Add(new SimpleGoal(goalData[0], goalData[1], int.Parse(goalData[2]), bool.Parse(goalData[3])));
                        break;
                    case "EternalGoal":
                        _goals.Add(new EternalGoal(goalData[0], goalData[1], int.Parse(goalData[2])));
                        break;
                    case "ChecklistGoal":
                        _goals.Add(new ChecklistGoal(goalData[0], goalData[1], int.Parse(goalData[2]), int.Parse(goalData[3]), int.Parse(goalData[4]), int.Parse(goalData[5])));
                        break;
                }
            }
            Console.WriteLine("Goals loaded successfully.");
        }
        else
        {
            Console.WriteLine("The file does not exist.");
        }
    }

    // Checks if the player has earned enough points to level up.
    private void CheckForLevelUp()
    {
        // A simple leveling system based on score is used.
        // The logic can be adjusted if a different growth curve is desired.
        while (_score >= _pointsForNextLevel)
        {
            _level++;
            _pointsForNextLevel += 1000 * _level; // Increases the points required for the next level.
            Console.WriteLine($"Congratulations! You have leveled up to level {_level}.");
        }
    }
}

// File: Goal.cs
// This base class defines the common structure and behavior for all types of goals.
// It contains abstract methods that must be implemented by derived classes.

public abstract class Goal
{
    protected string _shortName;
    protected string _description;
    protected int _points;

    public Goal(string name, string description, int points)
    {
        _shortName = name;
        _description = description;
        _points = points;
    }

    public string GetShortName() => _shortName;
    public string GetDescription() => _description;

    public abstract int RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetDetailsString();
    public abstract string GetStringRepresentation();
}

// File: SimpleGoal.cs
// This class represents a goal that is completed only once.

public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points)
        : base(name, description, points)
    {
        _isComplete = false;
    }

    // Constructor for loading data from a file.
    public SimpleGoal(string name, string description, int points, bool isComplete)
        : base(name, description, points)
    {
        _isComplete = isComplete;
    }

    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            return _points;
        }
        return 0; // Gives no points if already completed.
    }

    public override bool IsComplete() => _isComplete;

    public override string GetDetailsString()
    {
        string status = _isComplete ? "[X]" : "[ ]";
        return $"{status} {_shortName} ({_description})";
    }

    public override string GetStringRepresentation() => $"SimpleGoal:{_shortName},{_description},{_points},{_isComplete}";
}

// File: EternalGoal.cs
// This class represents a goal that can be completed an infinite number of times.

public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    public override int RecordEvent()
    {
        return _points; // Always gives points, never completes.
    }

    public override bool IsComplete() => false;

    public override string GetDetailsString()
    {
        return $"[ ] {_shortName} ({_description})";
    }

    public override string GetStringRepresentation() => $"EternalGoal:{_shortName},{_description},{_points}";
}

// File: ChecklistGoal.cs
// This class represents a goal that must be completed a specific number of times to earn a bonus.

public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int target, int bonus)
        : base(name, description, points)
    {
        _amountCompleted = 0;
        _target = target;
        _bonus = bonus;
    }

    // Constructor for loading data from a file.
    public ChecklistGoal(string name, string description, int points, int target, int bonus, int amountCompleted)
        : base(name, description, points)
    {
        _amountCompleted = amountCompleted;
        _target = target;
        _bonus = bonus;
    }

    public override int RecordEvent()
    {
        _amountCompleted++;
        if (_amountCompleted >= _target)
        {
            return _points + _bonus;
        }
        return _points;
    }

    public override bool IsComplete() => _amountCompleted >= _target;

    public override string GetDetailsString()
    {
        string status = IsComplete() ? "[X]" : "[ ]";
        return $"{status} {_shortName} ({_description}) -- Completed {_amountCompleted}/{_target} times";
    }

    public override string GetStringRepresentation() => $"ChecklistGoal:{_shortName},{_description},{_points},{_target},{_bonus},{_amountCompleted}";
}
