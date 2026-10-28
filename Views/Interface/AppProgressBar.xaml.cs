namespace GymApp.Views.Interface;

public partial class AppProgressBar : ContentView
{
    public static readonly BindableProperty ProgressProperty = BindableProperty.Create(
        nameof(Progress), typeof(double), typeof(AppProgressBar), 0d,
        coerceValue: (bindable, value) => Math.Clamp((double)value, 0d, 1d));

    public double Progress
    {
        get => (double)GetValue(ProgressProperty);
        set => SetValue(ProgressProperty, value);
    }

    public AppProgressBar()
    {
        InitializeComponent();
    }
}
