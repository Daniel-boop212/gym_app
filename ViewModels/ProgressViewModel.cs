using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GymApp.Services;

namespace GymApp.ViewModels;

public sealed class ProgressViewModel : INotifyPropertyChanged
{
    private readonly WorkoutService _workoutService;
    private string _completedWorkoutsText = "0";
    private string _trainingTimeText = "0 min";
    private string _completedSetsText = "0";

    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<WorkoutSessionSummary> Sessions { get; } = new();

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

    public string CompletedSetsText
    {
        get => _completedSetsText;
        private set => SetProperty(ref _completedSetsText, value);
    }

    public ProgressViewModel(WorkoutService workoutService)
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

        Sessions.Clear();
        foreach (WorkoutSessionSummary session in sessions)
        {
            Sessions.Add(session);
        }

        CompletedWorkoutsText = sessions.Length.ToString();
        CompletedSetsText = sessions.Sum(session => session.CompletedSetCount).ToString();
        TrainingTimeText = WorkoutSessionSummary.FormatDuration(
            TimeSpan.FromTicks(sessions.Sum(session => session.Duration.Ticks)));
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
