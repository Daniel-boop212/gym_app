using System.ComponentModel;
using System.Runtime.CompilerServices;
using GymApp.Services;

namespace GymApp.ViewModels;

public sealed class HomeViewModel : INotifyPropertyChanged
{
    private readonly WorkoutService _workoutService;

    private string _completedWorkoutsText = "0";
    private string _trainingTimeText = "0 min";
    private string _latestActivityTitle = "Todavía no hay actividad";
    private string _latestActivityDescription = "Tus entrenamientos registrados aparecerán aquí.";

    public event PropertyChangedEventHandler? PropertyChanged;

    public string CompletedWorkoutsText
    {
        get => _completedWorkoutsText;
        private set => SetProperty(ref _completedWorkoutsText, value);
    }

    public string TrainingTimeText
    {
        get => _trainingTimeText;
        private set => SetProperty(ref _trainingTimeText, value);
    }

    public string LatestActivityTitle
    {
        get => _latestActivityTitle;
        private set => SetProperty(ref _latestActivityTitle, value);
    }

    public string LatestActivityDescription
    {
        get => _latestActivityDescription;
        private set => SetProperty(ref _latestActivityDescription, value);
    }

    public HomeViewModel(WorkoutService workoutService)
    {
        _workoutService = workoutService;
        Refresh();
    }

    public void Refresh()
    {
        WorkoutSessionSummary[] sessions = _workoutService.GetCompletedWorkouts()
            .Where(session => session.EndTime.HasValue)
            .OrderByDescending(session => session.StartTime)
            .Select(session => new WorkoutSessionSummary(session))
            .ToArray();

        CompletedWorkoutsText = sessions.Length.ToString();
        TrainingTimeText = WorkoutSessionSummary.FormatDuration(
            TimeSpan.FromTicks(sessions.Sum(session => session.Duration.Ticks)));

        WorkoutSessionSummary? latest = sessions.FirstOrDefault();
        if (latest == null)
        {
            LatestActivityTitle = "Todavía no hay actividad";
            LatestActivityDescription = "Tus entrenamientos registrados aparecerán aquí.";
            return;
        }

        LatestActivityTitle = latest.RoutineName;
        LatestActivityDescription = $"Última sesión · {latest.DateText} · {latest.DurationText}";
    }

    private void SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return;
        }

        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
