using GymApp.Models;
using GymApp.ViewModels;
using GymApp.Views;

namespace GymApp.Views;

public partial class ExercisesPage : ContentPage
{
    private readonly ExercisesViewModel _viewModel;

    public ExercisesPage()
    {
        InitializeComponent();

        _viewModel = new ExercisesViewModel();

        ExercisesList.ItemsSource = _viewModel.Exercises;

        LoadExercises();
    }

    private async void LoadExercises()
    {
        await _viewModel.LoadExercises();

        MuscleGroupPicker.Items.Clear();
        MuscleGroupPicker.Items.Add("Todos");

        foreach (string muscleGroup in _viewModel.GetMuscleGroups())
        {
            MuscleGroupPicker.Items.Add(muscleGroup);
        }

        MuscleGroupPicker.SelectedIndex = 0;
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        _viewModel.SearchExercises(e.NewTextValue);
    }

    private void OnMuscleGroupChanged(object sender, EventArgs e)
    {
        if (MuscleGroupPicker.SelectedItem is string muscleGroup)
        {
            _viewModel.FilterByMuscleGroup(muscleGroup);
        }
    }

    private async void OnExerciseTapped(object sender, TappedEventArgs e)
    {
        if (sender is Border border &&
            border.BindingContext is Exercise exercise)
        {
            await Navigation.PushAsync(
                new ExerciseDetailPage(exercise));
        }
    }
}