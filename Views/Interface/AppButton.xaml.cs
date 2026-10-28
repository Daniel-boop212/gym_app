namespace GymApp.Views.Interface;

public partial class AppButton : ContentView
{
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(AppButton),
        string.Empty);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public event EventHandler? Clicked;

    public AppButton()
    {
        InitializeComponent();
    }

    private void OnButtonClicked(object? sender, EventArgs e)
    {
        Clicked?.Invoke(this, e);
    }
}
