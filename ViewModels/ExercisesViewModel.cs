using System.Collections.ObjectModel;
using GymApp.Models;
using GymApp.Services;

namespace GymApp.ViewModels;

public class ExercisesViewModel
{
    private readonly ExerciseService _exerciseService;

    public ObservableCollection<Exercise> Exercises { get; set; }

    public ExercisesViewModel()
    {
        _exerciseService = new ExerciseService();
        Exercises = new ObservableCollection<Exercise>();
    }

    public async Task LoadExercises()
    {
        List<Exercise> exercises = await _exerciseService.GetExercises();

        Exercises.Clear();

        foreach (Exercise exercise in exercises)
        {
            Exercises.Add(exercise);
        }
    }
}