namespace KomodoConsole.Repository;

// POCO Plain Old C# Object
public class Developer
{
    public string Name { get; set; }
    public int IdNumber { get; set; }
    public bool HasPluralsightAccess { get; set; }

    public Developer(string name, int idNumber, bool hasPluralsightAccess)
    {
        Name = name;
        IdNumber = idNumber;
        HasPluralsightAccess = hasPluralsightAccess;
    }
}

public class DevTeam
{
    public string TeamName { get; set; }
    public int TeamId { get; set; }
    public List<Developer> Developers { get; set; }

    public DevTeam(string teamName, int teamId)
    {
        TeamName = teamName;
        TeamId = teamId;
        Developers = new List<Developer>();
    }
}
