using GymApp.Models;
using GymApp.Services;

namespace GymApp.Views;

public partial class RoutineDetailPage : ContentPage
{
    private readonly Routine _routine;
    private readonly WorkoutService _workoutService;

    public RoutineDetailPage(Routine routine, WorkoutService workoutService)
    {
        InitializeComponent();

        _routine = routine;
        _workoutService = workoutService;

        RoutineNameLabel.Text = routine.Name;

        ExercisesList.ItemsSource = _routine.Exercises;
    }

    private async void OnStartWorkoutClicked(object? sender, EventArgs e)
    {
        if (_routine.Exercises.Count == 0)
        {
            await DisplayAlertAsync(
                "Rutina vacía",
                "Agrega al menos un ejercicio antes de comenzar.",
                "Aceptar");
            return;
        }

        await Navigation.PushAsync(new WorkoutPage(_routine, _workoutService));
    }

    private async void OnAddExerciseClicked(object? sender, EventArgs e)
    {
        SelectExercisePage page = new SelectExercisePage(OnExerciseSelected);

        await Navigation.PushAsync(page);
    }

    private void OnExerciseSelected(Exercise exercise)
    {
        RoutineExercise routineExercise = new RoutineExercise
        {
            Exercise = exercise
        };

        _routine.Exercises.Add(routineExercise);
    }

    private async void OnRoutineExerciseSelected(
        object? sender,
        SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is RoutineExercise routineExercise)
        {
            await Navigation.PushAsync(
                new RoutineExerciseDetailPage(
                    _routine,
                    routineExercise));

            ExercisesList.SelectedItem = null;
        }
    }
}
