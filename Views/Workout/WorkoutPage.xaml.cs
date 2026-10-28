using GymApp.Models;
using GymApp.Services;
using GymApp.ViewModels;

namespace GymApp.Views;

public partial class WorkoutPage : ContentPage
{
    private readonly WorkoutViewModel _viewModel;

    private readonly IDispatcherTimer _timer;

    public WorkoutPage(
        Routine routine,
        WorkoutService workoutService)
    {
        InitializeComponent();

        _viewModel = new WorkoutViewModel(
            workoutService);

        _viewModel.StartWorkout(routine);

        _timer = Dispatcher.CreateTimer();

        _timer.Interval = TimeSpan.FromSeconds(1);

        _timer.Tick += OnTimerTick;

        _timer.Start();

        UpdateScreen();
    }

    private void OnTimerTick(
        object? sender,
        EventArgs e)
    {
        _viewModel.UpdateTimer();

        TimerLabel.Text =
            _viewModel.ElapsedTimeText;
    }

    private void UpdateScreen()
    {
        RoutineNameLabel.Text =
            _viewModel.Session?.RoutineName ?? "Entrenamiento";

        ExerciseNameLabel.Text =
            _viewModel.CurrentExerciseName;

        ExerciseProgressLabel.Text =
            $"Ejercicio {_viewModel.CurrentExerciseNumber} de {_viewModel.TotalExercises}";

        SetsList.ItemsSource =
            _viewModel.CurrentSets;

        TimerLabel.Text =
            _viewModel.ElapsedTimeText;
    }

    private async void OnAddSetClicked(
        object? sender,
        EventArgs e)
    {
        if (!int.TryParse(
                RepsEntry.Text,
                out int reps) ||
            !double.TryParse(
                WeightEntry.Text,
                out double weight))
        {
            await DisplayAlertAsync(
                "Datos inválidos",
                "Introduce un peso y un número de repeticiones válidos.",
                "Aceptar");

            return;
        }

        WorkoutExercise? exercise =
            _viewModel.CurrentExercise;

        if (exercise == null)
            return;

        _viewModel.AddSet(
            exercise,
            reps,
            weight);

        WeightEntry.Text = "";
        RepsEntry.Text = "";

        SetsList.ItemsSource = null;
        SetsList.ItemsSource =
            _viewModel.CurrentSets;
    }

    private void OnCompleteExerciseClicked(
        object? sender,
        EventArgs e)
    {
        _viewModel.CompleteExercise();
    }

    private void OnSkipExerciseClicked(
        object? sender,
        EventArgs e)
    {
        _viewModel.SkipExercise();
    }

    private void OnNextExerciseClicked(
        object? sender,
        EventArgs e)
    {
        _viewModel.NextExercise();

        UpdateScreen();
    }

    private async void OnFinishWorkoutClicked(
        object? sender,
        EventArgs e)
    {
        bool confirm = await DisplayAlertAsync(
            "Finalizar entrenamiento",
            "¿Quieres finalizar este entrenamiento?",
            "Finalizar",
            "Continuar");

        if (!confirm)
            return;

        _timer.Stop();

        _viewModel.FinishWorkout();

        await Navigation.PopAsync();
    }
}
