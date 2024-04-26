using KomodoConsole.Repository;
using System;

namespace KomodoConsole.Console;

public class ProgramUI
{
    private DeveloperRepo _developerRepo = new DeveloperRepo();
    private DevTeamRepo _devTeamRepo = new DevTeamRepo();

    public void Run()
    {
        SeedData();
        MainMenu();
    }

    private void MainMenu()
    {
        bool isRunning = true;
        while (isRunning)
        {
            System.Console.Clear();
            System.Console.WriteLine("------< Komodo Insurance Management System >------\n\n" +
                              "         1. View all developers\n" +
                              "         2. View all developer teams\n" +
                              "         3. Add a developer\n" +
                              "         4. Remove a developer\n" +
                              "         5. Add a team\n" +
                              "         6. Add developer to team\n" +
                              "         7. Remove team\n" +
                              "         8. Developers with Pluralsight Access\n" +
                              "         9. Exit");

            string input = System.Console.ReadLine();
            switch (input)
            {
                case "1":
                    DisplayAllDevelopers();
                    break;
                case "2":
                    DisplayAllDevTeams();
                    break;
                case "3":
                    AddDeveloper();
                    break;
                case "4":
                    RemoveDeveloper();
                    break;
                case "5":
                    AddTeam();
                    break;
                case "6":
                    AddDeveloperToTeam();
                    break;
                case "7":
                    RemoveTeam();
                    break;
                case "8":
                    ListDevelopersNeedingPluralsight();
                    break;
                case "9":
                    isRunning = false;
                    break;
                default:
                    System.Console.WriteLine("Please enter a valid number.");
                    break;
            }
            System.Console.WriteLine("Press any key to return to the main menu...");
            System.Console.ReadKey();
        }
    }

    private void DisplayAllDevelopers()
    {
        System.Console.Clear();
        List<Developer> developers = _developerRepo.GetAllDevelopers();
        if (developers.Count > 0)
        {
            System.Console.WriteLine("List of All Developers:");
            foreach (Developer developer in developers)
            {
                System.Console.WriteLine($"ID: {developer.IdNumber}, Name: {developer.Name} PluralSight Access: {(developer.HasPluralsightAccess ? "yes" : "No")}");
            }
        }
        else
        {
            System.Console.WriteLine("There are no developers currently registered.");
        }
    }

    private void DisplayAllDevTeams()
    {
        System.Console.Clear();
        List<DevTeam> devTeams = _devTeamRepo.GetAllTeams();
        if (devTeams.Count > 0)
        {
            System.Console.WriteLine("List of all Developer Teams:");
            foreach (DevTeam team in devTeams)
            {
                System.Console.WriteLine($"Team ID: {team.TeamId}, Team Name: {team.TeamName}");
                System.Console.WriteLine("Members:");
                foreach (Developer member in team.Developers)
                {
                    System.Console.WriteLine($"  - ID: {member.IdNumber}, Name: {member.Name}");
                }
                System.Console.WriteLine();
            }
        }
        else
        {
            System.Console.WriteLine("There are no developer teams currently formed.");
        }
    }

    private void AddDeveloper()
    {
        System.Console.Clear();
        System.Console.Write("Enter the name of the developer: ");
        string name = System.Console.ReadLine();
        System.Console.Write("Enter the ID number for the developer: ");
        int id = int.Parse(System.Console.ReadLine());
        System.Console.Write("Does the developer have Pluralsight access? (yes/no): ");
        bool hasAccess = System.Console.ReadLine().ToLower() == "yes";

        Developer newDeveloper = new Developer(name, id, hasAccess);
        _developerRepo.AddDeveloper(newDeveloper);
        System.Console.WriteLine("Developer added successfully!");
    }

    private void RemoveDeveloper()
    {
        System.Console.Clear();
        System.Console.Write("Enter the ID number of the developer to remove: ");
        int id = int.Parse(System.Console.ReadLine());
        if (_developerRepo.RemoveDeveloper(id))
        {
            System.Console.WriteLine("Developer removed successfully.");
        }
        else
        {
            System.Console.WriteLine("Developer not found.");
        }
    }

    private void AddTeam()
    {
        System.Console.Clear();
        System.Console.Write("Enter the team name: ");
        string teamName = System.Console.ReadLine();
        System.Console.Write("Enter the team ID: ");
        int teamId = int.Parse(System.Console.ReadLine());

        DevTeam newTeam = new DevTeam(teamName, teamId);
        _devTeamRepo.AddTeam(newTeam);
        System.Console.WriteLine("Team added successfully!");
    }

    private void AddDeveloperToTeam()
    {
        System.Console.Clear();
        System.Console.Write("Enter the team ID: ");
        int teamId = int.Parse(System.Console.ReadLine());
        System.Console.Write("Enter the developer's ID number: ");
        int developerId = int.Parse(System.Console.ReadLine());

        Developer developer = _developerRepo.GetDeveloperById(developerId);
        if (developer != null)
        {
            _devTeamRepo.AddDeveloperToTeam(teamId, developer);
            System.Console.WriteLine("Developer added to the team successfully!");
        }
        else
        {
            System.Console.WriteLine("Developer not found.");
        }
    }

    private void RemoveTeam()
    {
        System.Console.Clear();
        System.Console.Write("Enter the team ID to remove: ");
        int teamId = int.Parse(System.Console.ReadLine());
        if (_devTeamRepo.RemoveTeam(teamId))
        {
            System.Console.WriteLine("Team removed successfully.");
        }
        else
        {
            System.Console.WriteLine("Team not found.");
        }
    }

    private void ListDevelopersNeedingPluralsight()
    {
        System.Console.Clear();
        List<Developer> developers = _developerRepo.GetDevelopersNeedingPluralsight();
        if (developers.Count > 0)
        {
            foreach (Developer developer in developers)
            {
                System.Console.WriteLine($"ID: {developer.IdNumber}, Name: {developer.Name}");
            }
        }
        else
        {
            System.Console.WriteLine("No developers need Pluralsight access.");
        }
    }

    private void SeedData()
    {
        // Add some initial data to work with
        _developerRepo.AddDeveloper(new Developer("Donnie Brosco", 101, false));
        _developerRepo.AddDeveloper(new Developer("John Tesh", 102, true));
        _developerRepo.AddDeveloper(new Developer("Richard Nixon", 103, true));
        _developerRepo.AddDeveloper(new Developer("Mario VanPeebles", 104, false));
        _developerRepo.AddDeveloper(new Developer("Neil Diamond", 105, true));
        _devTeamRepo.AddTeam(new DevTeam("Red Team", 201));
        _devTeamRepo.AddTeam(new DevTeam("Blue Team", 202));
        _devTeamRepo.AddTeam(new DevTeam("Green Team", 203));
    }
}