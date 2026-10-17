using System.Text.Json;
using GymApp.Models;

namespace GymApp.Services;

public class ExerciseService
{
    public async Task<List<Exercise>> GetExercises()
    {
        using Stream stream = await FileSystem.OpenAppPackageFileAsync("Data/exercises.json");

        List<Exercise>? exercises = await JsonSerializer.DeserializeAsync<List<Exercise>>(
            stream,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        return exercises ?? new List<Exercise>();
    }
}