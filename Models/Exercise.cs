namespace GymApp.Models;

public class Exercise
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string MuscleGroup { get; set; }
    public List<string> SecondaryMuscles { get; set; }
    public string Equipment { get; set; }
    public string Difficulty { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public List<string> Instructions { get; set; }
    public List<string> Tips { get; set; }
}