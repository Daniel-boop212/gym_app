using GymApp.Models;

namespace GymApp.ViewModels;

public sealed class WorkoutSessionSummary
{
    public string RoutineName { get; }
    public string DateText { get; }
    public TimeSpan Duration { get; }
    public string DurationText => FormatDuration(Duration);
    public int CompletedExerciseCount { get; }
    public int CompletedSetCount { get; }
    public string DetailsText => $"{CompletedExerciseCount} ejercicios · {CompletedSetCount} series";

    public WorkoutSessionSummary(WorkoutSession session)
    {
        RoutineName = session.RoutineName;
        DateText = (session.EndTime ?? session.StartTime).ToString("dd/MM/yyyy");

        DateTime endTime = session.EndTime ?? session.StartTime;
        Duration = endTime > session.StartTime
            ? endTime - session.StartTime
            : TimeSpan.Zero;

        CompletedExerciseCount = session.Exercises.Count(exercise =>
            exercise.Completed && !exercise.Skipped);

        CompletedSetCount = session.Exercises
            .SelectMany(exercise => exercise.Sets)
            .Count(set => set.Completed);
    }

    public static string FormatDuration(TimeSpan duration)
    {
        int totalMinutes = Math.Max(0, (int)Math.Round(duration.TotalMinutes));
        int hours = totalMinutes / 60;
        int minutes = totalMinutes % 60;

        if (hours == 0)
        {
            return $"{minutes} min";
        }

        return minutes == 0
            ? $"{hours} h"
            : $"{hours} h {minutes} min";
    }
}
