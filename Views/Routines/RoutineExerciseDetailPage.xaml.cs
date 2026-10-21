using GymApp.Models;

namespace GymApp.Views;

public partial class RoutineExerciseDetailPage : ContentPage
{
    private readonly Routine _routine;
    private readonly RoutineExercise _routineExercise;

    public RoutineExerciseDetailPage(
        Routine routine,
        RoutineExercise routineExercise)
    {
        InitializeComponent();

        _routine = routine;
        _routineExercise = routineExercise;

        ExerciseNameLabel.Text = routineExercise.Exercise.Name;
        MuscleGroupLabel.Text = routineExercise.Exercise.MuscleGroup;

        SetsEntry.Text = routineExercise.Sets.ToString();
        RepsEntry.Text = routineExercise.Reps.ToString();
        WeightEntry.Text = routineExercise.Weight.ToString();
        RestEntry.Text = routineExercise.RestSeconds.ToString();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (!int.TryParse(SetsEntry.Text, out int sets) ||
            !int.TryParse(RepsEntry.Text, out int reps) ||
            !double.TryParse(WeightEntry.Text, out double weight) ||
            !int.TryParse(RestEntry.Text, out int rest))
        {
            await DisplayAlert(
                "Datos inválidos",
                "Revisa los valores ingresados.",
                "Aceptar");

            return;
        }

        _routineExercise.Sets = sets;
        _routineExercise.Reps = reps;
        _routineExercise.Weight = weight;
        _routineExercise.RestSeconds = rest;

        await Navigation.PopAsync();
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert(
            "Eliminar ejercicio",
            $"¿Quieres eliminar {_routineExercise.Exercise.Name} de esta rutina?",
            "Eliminar",
            "Cancelar");

        if (!confirm)
            return;

        _routine.Exercises.Remove(_routineExercise);

        await Navigation.PopAsync();
    }
}