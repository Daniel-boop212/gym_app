using System.Collections.ObjectModel;

namespace GymApp.Models;

public class WorkoutSession
{
    public int Id { get; set; }

    public int RoutineId { get; set; }

    public string RoutineName { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public ObservableCollection<WorkoutExercise> Exercises { get; set; }

    public WorkoutSession()
    {
        RoutineName = "";
        Exercises = new ObservableCollection<WorkoutExercise>();
    }
}