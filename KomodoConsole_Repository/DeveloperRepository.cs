namespace KomodoConsole.Repository;

public class DeveloperRepo
{
    private List<Developer> _developers = new List<Developer>();
    // Create
    public void AddDeveloper(Developer developer)
    {
        _developers.Add(developer);
    }
    // Read
    public List<Developer> GetAllDevelopers()
    {
        return _developers;
    }
    // Update
    public Developer GetDeveloperById(int id)
    {
        return _developers.FirstOrDefault(d => d.IdNumber == id);
    }
    // Delete
    public bool RemoveDeveloper(int id)
    {
        Developer developer = GetDeveloperById(id);
        if (developer != null)
        {
            return _developers.Remove(developer);
        }
        return false;
    }

    public List<Developer> GetDevelopersNeedingPluralsight()
    {
        return _developers.Where(d => !d.HasPluralsightAccess).ToList();
    }
}

public class DevTeamRepo
{
    private List<DevTeam> _devTeams = new List<DevTeam>();

    public void AddTeam(DevTeam team)
    {
        _devTeams.Add(team);
    }

    public List<DevTeam> GetAllTeams()
    {
        return _devTeams;
    }

    public DevTeam GetTeamById(int id)
    {
        return _devTeams.FirstOrDefault(t => t.TeamId == id);
    }

    public bool RemoveTeam(int id)
    {
        DevTeam team = GetTeamById(id);
        if (team != null)
        {
            return _devTeams.Remove(team);
        }
        return false;
    }

    public void AddDeveloperToTeam(int teamId, Developer developer)
    {
        DevTeam team = GetTeamById(teamId);
        if (team != null)
        {
            team.Developers.Add(developer);
        }
    }

    public void AddMultipleDevelopersToTeam(int teamId, List<Developer> developers)
    {
        foreach (var developer in developers)
        {
            AddDeveloperToTeam(teamId, developer);
        }
    }
}

