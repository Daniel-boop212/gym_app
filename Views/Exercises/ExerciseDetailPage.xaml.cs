using GymApp.Models;

namespace GymApp.Views;

public partial class ExerciseDetailPage : ContentPage
{
    public ExerciseDetailPage(Exercise exercise)
    {
        InitializeComponent();

        BindingContext = exercise;
    }
}