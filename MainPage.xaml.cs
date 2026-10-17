using GymApp.ViewModels;

namespace GymApp;

public partial class MainPage : ContentPage
{
    private readonly ExercisesViewModel _viewModel;

    public MainPage()
    {
        InitializeComponent();

        _viewModel = new ExercisesViewModel();

        ExercisesList.ItemsSource = _viewModel.Exercises;

        LoadExercises();
    }

    private async void LoadExercises()
    {
        await _viewModel.LoadExercises();
    }
}