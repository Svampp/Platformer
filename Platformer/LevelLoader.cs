using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Numerics;
using Raylib_CsLo;
using System.Collections.Generic;

/// <summary>
/// Provides functionality to load and create game levels, platforms, collectibles, and enemies from JSON data.
/// </summary>
public static class LevelLoader
{
    /// <summary>
    /// Loads level data from a JSON file located at the specified file path.
    /// </summary>
    /// <param name="filePath">The path to the JSON file containing the level data.</param>
    /// <returns>A <see cref="LevelData"/> object representing the deserialized level data.</returns>
    /// <exception cref="FileNotFoundException">Thrown when the specified file path does not exist.</exception>
    /// <exception cref="JsonException">Thrown when there is an error during JSON deserialization.</exception>
    /// <exception cref="Exception">Thrown when an unexpected error occurs during the loading process.</exception>
    public static LevelData LoadLevel(string filePath)
    {
        try
        {
            string json = File.ReadAllText(filePath);
            Console.WriteLine("Loaded JSON: " + json);

            var options = new JsonSerializerOptions
            {
                Converters = { new Vector2Converter(), new ColorConverter() }
            };

            LevelData levelData = JsonSerializer.Deserialize<LevelData>(json, options);

            if (levelData == null)
            {
                throw new Exception("Failed to deserialize JSON into LevelData.");
            }

            return levelData;
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found: " + filePath);
            throw;
        }
        catch (JsonException ex)
        {
            Console.WriteLine("JSON parsing error: " + ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error: " + ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Creates a list of <see cref="GameObject"/> instances representing platforms based on the provided platform data.
    /// </summary>
    /// <param name="platformData">A list of <see cref="PlatformData"/> objects containing the platform definitions.</param>
    /// <returns>A list of <see cref="GameObject"/> instances representing the platforms.</returns>
    public static List<GameObject> CreatePlatforms(List<PlatformData> platformData)
    {
        var platforms = new List<GameObject>();
        foreach (var data in platformData)
        {
            Vector2 position = data.Position;
            Vector2 size = data.Size;
            Color color = data.Color;

            platforms.Add(new GameObject(position, size, color));
            Console.WriteLine($"Platform created at {position}");
        }
        return platforms;
    }

    /// <summary>
    /// Creates a list of <see cref="GameObject"/> instances representing collectibles based on the provided collectible data.
    /// </summary>
    /// <param name="collectibleData">A list of <see cref="CollectibleData"/> objects containing the collectible definitions.</param>
    /// <returns>A list of <see cref="GameObject"/> instances representing the collectibles.</returns>
    public static List<GameObject> CreateCollectibles(List<CollectibleData> collectibleData)
    {
        var collectibles = new List<GameObject>();
        foreach (var data in collectibleData)
        {
            Vector2 position = data.Position;
            Vector2 size = data.Size;
            Color color = data.Color;

            collectibles.Add(new GameObject(position, size, color));
            Console.WriteLine($"Collectible created at {position}");
        }
        return collectibles;
    }

    /// <summary>
    /// Creates a list of <see cref="Enemy"/> instances based on the provided enemy data.
    /// </summary>
    /// <param name="enemyData">A list of <see cref="EnemyData"/> objects containing the enemy definitions.</param>
    /// <returns>A list of <see cref="Enemy"/> instances representing the enemies.</returns>
    public static List<Enemy> CreateEnemies(List<EnemyData> enemyData)
    {
        var enemies = new List<Enemy>();
        foreach (var data in enemyData)
        {
            Vector2 position = data.Position;
            Vector2 size = data.Size;
            Color color = data.Color;
            float speed = data.Speed;
            float moveRange = data.MoveRange;

            enemies.Add(new Enemy(position, size, color, speed, moveRange));
            Console.WriteLine($"Enemy created at {position} with speed {speed} and move range {moveRange}");
        }
        return enemies;
    }
}