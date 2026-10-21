using System.Collections.ObjectModel;

namespace GymApp.Models;

public class RoutineExercise
{
    public int Id { get; set; }

    public Exercise Exercise { get; set; }

    public int Sets { get; set; }

    public int Reps { get; set; }

    public double Weight { get; set; }

    public int RestSeconds { get; set; }

    public RoutineExercise()
    {
        Exercise = new Exercise();
        Sets = 3;
        Reps = 10;
        Weight = 0;
        RestSeconds = 60;
    }
}