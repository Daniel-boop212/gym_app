using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GymApp.Models;
using GymApp.Services;

namespace GymApp.ViewModels;

public class WorkoutViewModel : INotifyPropertyChanged
{
    private readonly WorkoutService _workoutService;

    private WorkoutSession? _session;

    private int _currentExerciseIndex;

    private DateTime _startTime;

    private TimeSpan _elapsedTime;

    private bool _isWorkoutActive;

    public event PropertyChangedEventHandler? PropertyChanged;

    public WorkoutSession? Session
    {
        get
        {
            return _session;
        }
        private set
        {
            _session = value;
            OnPropertyChanged();
        }
    }

    public WorkoutExercise? CurrentExercise
    {
        get
        {
            if (Session == null)
                return null;

            if (_currentExerciseIndex < 0 ||
                _currentExerciseIndex >= Session.Exercises.Count)
            {
                return null;
            }

            return Session.Exercises[_currentExerciseIndex];
        }
    }

    public string CurrentExerciseName
    {
        get
        {
            return CurrentExercise?.ExerciseName ?? "";
        }
    }

    public int CurrentExerciseNumber
    {
        get
        {
            return _currentExerciseIndex + 1;
        }
    }

    public int TotalExercises
    {
        get
        {
            return Session?.Exercises.Count ?? 0;
        }
    }

    public ObservableCollection<WorkoutSet> CurrentSets
    {
        get
        {
            if (CurrentExercise == null)
                return new ObservableCollection<WorkoutSet>();

            return CurrentExercise.Sets;
        }
    }

    public TimeSpan ElapsedTime
    {
        get
        {
            return _elapsedTime;
        }
        private set
        {
            _elapsedTime = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ElapsedTimeText));
        }
    }

    public string ElapsedTimeText
    {
        get
        {
            return ElapsedTime.ToString(@"mm\:ss");
        }
    }

    public bool IsWorkoutActive
    {
        get
        {
            return _isWorkoutActive;
        }
        private set
        {
            _isWorkoutActive = value;
            OnPropertyChanged();
        }
    }

    public WorkoutViewModel(WorkoutService workoutService)
    {
        _workoutService = workoutService;
    }

    public void StartWorkout(Routine routine)
    {
        Session = _workoutService.StartWorkout(routine);

        _currentExerciseIndex = 0;

        _startTime = DateTime.Now;

        ElapsedTime = TimeSpan.Zero;

        IsWorkoutActive = true;

        NotifyExerciseChanged();
    }

    public void UpdateTimer()
    {
        if (!IsWorkoutActive)
            return;

        ElapsedTime = DateTime.Now - _startTime;
    }

    public void AddSet(
        WorkoutExercise workoutExercise,
        int reps,
        double weight)
    {
        _workoutService.AddSet(
            workoutExercise,
            reps,
            weight);

        OnPropertyChanged(nameof(CurrentSets));
    }

    public void CompleteExercise()
    {
        if (CurrentExercise == null)
            return;

        _workoutService.CompleteExercise(
            CurrentExercise);
    }

    public void SkipExercise()
    {
        if (CurrentExercise == null)
            return;

        _workoutService.SkipExercise(
            CurrentExercise);
    }

    public void NextExercise()
    {
        if (Session == null)
            return;

        if (_currentExerciseIndex >=
            Session.Exercises.Count - 1)
        {
            return;
        }

        _currentExerciseIndex++;

        NotifyExerciseChanged();
    }

    private void NotifyExerciseChanged()
    {
        OnPropertyChanged(nameof(CurrentExercise));
        OnPropertyChanged(nameof(CurrentExerciseName));
        OnPropertyChanged(nameof(CurrentExerciseNumber));
        OnPropertyChanged(nameof(TotalExercises));
        OnPropertyChanged(nameof(CurrentSets));
    }

    public void FinishWorkout()
    {
        if (!IsWorkoutActive)
            return;

        _workoutService.FinishWorkout();

        IsWorkoutActive = false;

        Session = null;
    }

    private void OnPropertyChanged(
        [CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));
    }
}