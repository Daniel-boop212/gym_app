using System.Collections.ObjectModel;
using GymApp.Models;
using GymApp.Services;

namespace GymApp.ViewModels;

public class ExercisesViewModel
{
    private readonly ExerciseService _exerciseService;

    private List<Exercise> _allExercises = new();

    private string _currentSearchText = "";
    private string _currentMuscleGroup = "Todos";

    public ObservableCollection<Exercise> Exercises { get; set; }

    public ExercisesViewModel()
    {
        _exerciseService = new ExerciseService();
        Exercises = new ObservableCollection<Exercise>();
    }

    public async Task LoadExercises()
    {
        _allExercises = await _exerciseService.GetExercises();

        _currentSearchText = "";
        _currentMuscleGroup = "Todos";

        Exercises.Clear();

        foreach (Exercise exercise in _allExercises)
        {
            Exercises.Add(exercise);
        }
    }

    public void SearchExercises(string searchText)
    {
        _currentSearchText = searchText;

        ApplyFilters(
            _currentSearchText,
            _currentMuscleGroup);
    }

    public void FilterByMuscleGroup(string muscleGroup)
    {
        _currentMuscleGroup = muscleGroup;

        ApplyFilters(
            _currentSearchText,
            _currentMuscleGroup);
    }

    private void ApplyFilters(
        string? searchText,
        string? muscleGroup)
    {
        IEnumerable<Exercise> results = _allExercises;

        if (!string.IsNullOrWhiteSpace(searchText))
        {
            string search = searchText.ToLower();

            results = results.Where(exercise =>
                exercise.Name.ToLower().Contains(search) ||
                exercise.MuscleGroup.ToLower().Contains(search) ||
                exercise.Equipment.ToLower().Contains(search) ||
                exercise.Difficulty.ToLower().Contains(search) ||
                exercise.Type.ToLower().Contains(search));
        }

        if (!string.IsNullOrWhiteSpace(muscleGroup) &&
            muscleGroup != "Todos")
        {
            results = results.Where(exercise =>
                exercise.MuscleGroup == muscleGroup);
        }

        Exercises.Clear();

        foreach (Exercise exercise in results)
        {
            Exercises.Add(exercise);
        }
    }

    public List<string> GetMuscleGroups()
    {
        return _allExercises
            .Select(exercise => exercise.MuscleGroup)
            .Distinct()
            .OrderBy(group => group)
            .ToList();
    }
}