namespace GymApp.Models;

public class Exercise
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string MuscleGroup { get; set; } = string.Empty;
    public List<string> SecondaryMuscles { get; set; } = new();
    public string Equipment { get; set; } = string.Empty;
    public string Difficulty { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Instructions { get; set; } = new();
    public List<string> Tips { get; set; } = new();
}
