using GymApp.Models;

namespace GymApp.Views;

public partial class RoutineDetailPage : ContentPage
{
    private readonly Routine _routine;

    public RoutineDetailPage(Routine routine)
    {
        InitializeComponent();

        _routine = routine;

        RoutineNameLabel.Text = routine.Name;

        ExercisesList.ItemsSource = _routine.Exercises;
    }

    private async void OnAddExerciseClicked(object sender, EventArgs e)
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
        object sender,
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