namespace GymApp.Views.Interface;

public partial class AppHeader : ContentView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title), typeof(string), typeof(AppHeader), string.Empty);

    public static readonly BindableProperty SubtitleProperty = BindableProperty.Create(
        nameof(Subtitle), typeof(string), typeof(AppHeader), string.Empty,
        propertyChanged: OnSubtitleChanged);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string Subtitle
    {
        get => (string)GetValue(SubtitleProperty);
        set => SetValue(SubtitleProperty, value);
    }

    public AppHeader()
    {
        InitializeComponent();
    }

    private static void OnSubtitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is AppHeader header)
        {
            header.SubtitleLabel.IsVisible = !string.IsNullOrWhiteSpace((string)newValue);
        }
    }
}
