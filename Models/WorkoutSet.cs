namespace GymApp.Models;

public class WorkoutSet
{
    public int Id { get; set; }

    public int Reps { get; set; }

    public double Weight { get; set; }

    public bool Completed { get; set; }
}