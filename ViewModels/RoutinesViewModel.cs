using System.Collections.ObjectModel;
using GymApp.Models;
using GymApp.Services;

namespace GymApp.ViewModels;

public class RoutinesViewModel
{
    private readonly RoutineService _routineService;

    public ObservableCollection<Routine> Routines { get; set; }

    public RoutinesViewModel(RoutineService routineService)
    {
        _routineService = routineService;

        Routines = new ObservableCollection<Routine>(
            _routineService.GetRoutines());
    }

    public void AddRoutine(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return;

        _routineService.AddRoutine(name);

        Routines.Clear();

        foreach (Routine routine in _routineService.GetRoutines())
        {
            Routines.Add(routine);
        }
    }
}