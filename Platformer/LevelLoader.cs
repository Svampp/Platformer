using System;
using System.IO;
using System.Text.Json;
using System.Numerics;
using Raylib_CsLo;
using System.Collections.Generic;

public static class LevelLoader
{
    public static LevelData LoadLevel(string filePath)
    {
        try
        {
            string json = File.ReadAllText(filePath);
            Console.WriteLine("Loaded JSON: " + json);
            LevelData levelData = JsonSerializer.Deserialize<LevelData>(json);

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

            enemies.Add(new Enemy(position, size, color, speed, moveRange)); // Создаем Enemy
            Console.WriteLine($"Enemy created at {position} with speed {speed} and move range {moveRange}");
        }
        return enemies;
    }
}