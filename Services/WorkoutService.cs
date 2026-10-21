using GymApp.Models;

namespace GymApp.Services;

public class WorkoutService
{
    private readonly List<WorkoutSession> _sessions = new();

    private WorkoutSession? _currentSession;

    public WorkoutSession? CurrentSession
    {
        get
        {
            return _currentSession;
        }
    }

    public WorkoutSession StartWorkout(Routine routine)
    {
        WorkoutSession session = new WorkoutSession
        {
            Id = _sessions.Count + 1,
            RoutineId = routine.Id,
            RoutineName = routine.Name,
            StartTime = DateTime.Now
        };

        foreach (RoutineExercise routineExercise in routine.Exercises)
        {
            WorkoutExercise workoutExercise = new WorkoutExercise
            {
                ExerciseId = routineExercise.Exercise.Id,
                ExerciseName = routineExercise.Exercise.Name
            };

            session.Exercises.Add(workoutExercise);
        }

        _currentSession = session;

        return session;
    }

    public void AddSet(
        WorkoutExercise workoutExercise,
        int reps,
        double weight)
    {
        WorkoutSet set = new WorkoutSet
        {
            Id = workoutExercise.Sets.Count + 1,
            Reps = reps,
            Weight = weight,
            Completed = true
        };

        workoutExercise.Sets.Add(set);
    }

    public void CompleteExercise(WorkoutExercise workoutExercise)
    {
        workoutExercise.Completed = true;
        workoutExercise.Skipped = false;
    }

    public void SkipExercise(WorkoutExercise workoutExercise)
    {
        workoutExercise.Completed = false;
        workoutExercise.Skipped = true;
    }

    public void FinishWorkout()
    {
        if (_currentSession == null)
            return;

        _currentSession.EndTime = DateTime.Now;

        _sessions.Add(_currentSession);

        _currentSession = null;
    }

    public List<WorkoutSession> GetCompletedWorkouts()
    {
        return _sessions;
    }
}