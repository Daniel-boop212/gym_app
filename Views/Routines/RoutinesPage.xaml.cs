using GymApp.Models;
using GymApp.Services;
using GymApp.ViewModels;

namespace GymApp.Views;

public partial class RoutinesPage : ContentPage
{
    private readonly RoutinesViewModel _viewModel;

    public RoutinesPage(RoutinesViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;

        RoutinesList.ItemsSource = _viewModel.Routines;
    }

    private async void OnCreateRoutineClicked(object sender, EventArgs e)
    {
        string? name = await DisplayPromptAsync(
            "Nueva rutina",
            "Escribe el nombre de la rutina:");

        if (!string.IsNullOrWhiteSpace(name))
        {
            _viewModel.AddRoutine(name);
        }
    }

    private async void OnRoutineTapped(object sender, TappedEventArgs e)
    {
        if (sender is Border border &&
            border.BindingContext is Routine routine)
        {
            await Navigation.PushAsync(
                new RoutineDetailPage(routine));
        }
    }
}