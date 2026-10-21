using System.Collections.ObjectModel;

namespace GymApp.Models;

public class WorkoutExercise
{
    public int Id { get; set; }

    public int ExerciseId { get; set; }

    public string ExerciseName { get; set; }

    public bool Completed { get; set; }

    public bool Skipped { get; set; }

    public ObservableCollection<WorkoutSet> Sets { get; set; }

    public WorkoutExercise()
    {
        ExerciseName = "";
        Completed = false;
        Skipped = false;
        Sets = new ObservableCollection<WorkoutSet>();
    }
}