namespace GymApp.Views;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
    }

    private async void OnSaveClicked(object? sender, EventArgs e)
    {
        await DisplayAlertAsync(
            "Perfil",
            "Los cambios se han guardado.",
            "Aceptar");
    }
}
