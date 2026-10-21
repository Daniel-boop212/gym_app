using GymApp.Models;

namespace GymApp.Services;

public class RoutineService
{
    private readonly List<Routine> _routines = new();

    public List<Routine> GetRoutines()
    {
        return _routines;
    }

    public void AddRoutine(string name)
    {
        Routine routine = new Routine
        {
            Id = _routines.Count + 1,
            Name = name
        };

        _routines.Add(routine);
    }
}