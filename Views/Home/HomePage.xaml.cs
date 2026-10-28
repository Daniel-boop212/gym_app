using GymApp.ViewModels;

namespace GymApp.Views;

public partial class HomePage : ContentPage
{
    private readonly HomeViewModel _viewModel;

    public HomePage(HomeViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.Refresh();
    }

    private async void OnRoutinesClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//RoutinesPage");
    }
}
