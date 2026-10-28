using GymApp.Models;
using GymApp.Services;

namespace GymApp.Views;

public partial class SelectExercisePage : ContentPage
{
    private readonly ExerciseService _exerciseService;

    private readonly Action<Exercise> _onExerciseSelected;

    private List<Exercise> _allExercises = new();

    public SelectExercisePage(Action<Exercise> onExerciseSelected)
    {
        InitializeComponent();

        _exerciseService = new ExerciseService();
        _onExerciseSelected = onExerciseSelected;

        LoadExercises();
    }

    private async void LoadExercises()
    {
        _allExercises = await _exerciseService.GetExercises();

        ExercisesList.ItemsSource = _allExercises;
    }

    private void OnSearchTextChanged(object? sender, TextChangedEventArgs e)
    {
        string search = e.NewTextValue?.ToLower() ?? "";

        ExercisesList.ItemsSource = _allExercises
            .Where(exercise =>
                exercise.Name.ToLower().Contains(search) ||
                exercise.MuscleGroup.ToLower().Contains(search))
            .ToList();
    }

    private async void OnExerciseTapped(object? sender, TappedEventArgs e)
    {
        if (sender is Border border &&
            border.BindingContext is Exercise exercise)
        {
            _onExerciseSelected(exercise);

            await Navigation.PopAsync();
        }
    }
}
