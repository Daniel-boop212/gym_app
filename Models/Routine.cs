using System.Collections.ObjectModel;

namespace GymApp.Models;

public class Routine
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ObservableCollection<RoutineExercise> Exercises { get; set; }

    public Routine()
    {
        Exercises = new ObservableCollection<RoutineExercise>();
    }
}
