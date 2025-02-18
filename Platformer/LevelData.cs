using Raylib_CsLo;
using System.Collections.Generic;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the data structure for a game level, including platforms, collectibles, and enemies.
/// </summary>
public class LevelData
{
    /// <summary>
    /// Gets or sets the list of platforms in the level.
    /// </summary>
    public List<PlatformData> Platforms { get; set; } = new List<PlatformData>();

    /// <summary>
    /// Gets or sets the list of collectibles in the level.
    /// </summary>
    public List<CollectibleData> Collectibles { get; set; } = new List<CollectibleData>();

    /// <summary>
    /// Gets or sets the list of enemies in the level.
    /// </summary>
    public List<EnemyData> Enemies { get; set; } = new List<EnemyData>();
}

/// <summary>
/// Represents the data structure for a platform in the game.
/// </summary>
public class PlatformData
{
    /// <summary>
    /// Gets or sets the position of the platform in 2D space.
    /// </summary>
    public Vector2 Position { get; set; }

    /// <summary>
    /// Gets or sets the size of the platform in 2D space.
    /// </summary>
    public Vector2 Size { get; set; }

    /// <summary>
    /// Gets or sets the color of the platform.
    /// </summary>
    [JsonConverter(typeof(ColorConverter))]
    public Color Color { get; set; }
}

/// <summary>
/// Represents the data structure for a collectible in the game.
/// </summary>
public class CollectibleData
{
    /// <summary>
    /// Gets or sets the position of the collectible in 2D space.
    /// </summary>
    public Vector2 Position { get; set; }

    /// <summary>
    /// Gets or sets the size of the collectible in 2D space.
    /// </summary>
    public Vector2 Size { get; set; }

    /// <summary>
    /// Gets or sets the color of the collectible.
    /// </summary>
    [JsonConverter(typeof(ColorConverter))]
    public Color Color { get; set; }
}

/// <summary>
/// Represents the data structure for an enemy in the game.
/// </summary>
public class EnemyData
{
    /// <summary>
    /// Gets or sets the position of the enemy in 2D space.
    /// </summary>
    public Vector2 Position { get; set; }

    /// <summary>
    /// Gets or sets the size of the enemy in 2D space.
    /// </summary>
    public Vector2 Size { get; set; }

    /// <summary>
    /// Gets or sets the color of the enemy.
    /// </summary>
    [JsonConverter(typeof(ColorConverter))]
    public Color Color { get; set; }

    /// <summary>
    /// Gets or sets the movement speed of the enemy.
    /// </summary>
    public float Speed { get; set; }

    /// <summary>
    /// Gets or sets the range within which the enemy can move.
    /// </summary>
    public float MoveRange { get; set; }
}

/// <summary>
/// Provides custom JSON serialization and deserialization for <see cref="Vector2"/> objects.
/// </summary>
public class Vector2Converter : JsonConverter<Vector2>
{
    /// <summary>
    /// Reads a JSON object and converts it into a <see cref="Vector2"/>.
    /// </summary>
    /// <param name="reader">The JSON reader.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">The JSON serializer options.</param>
    /// <returns>A <see cref="Vector2"/> object.</returns>
    public override Vector2 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            var root = doc.RootElement;
            float x = root.GetProperty("X").GetSingle();
            float y = root.GetProperty("Y").GetSingle();
            return new Vector2(x, y);
        }
    }

    /// <summary>
    /// Writes a <see cref="Vector2"/> object into its JSON representation.
    /// </summary>
    /// <param name="writer">The JSON writer.</param>
    /// <param name="value">The <see cref="Vector2"/> value to write.</param>
    /// <param name="options">The JSON serializer options.</param>
    public override void Write(Utf8JsonWriter writer, Vector2 value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("X", value.X);
        writer.WriteNumber("Y", value.Y);
        writer.WriteEndObject();
    }
}

/// <summary>
/// Provides custom JSON serialization and deserialization for <see cref="Color"/> objects.
/// </summary>
public class ColorConverter : JsonConverter<Color>
{
    /// <summary>
    /// Reads a JSON object and converts it into a <see cref="Color"/>.
    /// </summary>
    /// <param name="reader">The JSON reader.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">The JSON serializer options.</param>
    /// <returns>A <see cref="Color"/> object.</returns>
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            var root = doc.RootElement;
            byte r = root.GetProperty("R").GetByte();
            byte g = root.GetProperty("G").GetByte();
            byte b = root.GetProperty("B").GetByte();
            byte a = root.GetProperty("A").GetByte();
            return new Color(r, g, b, a);
        }
    }

    /// <summary>
    /// Writes a <see cref="Color"/> object into its JSON representation.
    /// </summary>
    /// <param name="writer">The JSON writer.</param>
    /// <param name="value">The <see cref="Color"/> value to write.</param>
    /// <param name="options">The JSON serializer options.</param>
    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("R", value.r);
        writer.WriteNumber("G", value.g);
        writer.WriteNumber("B", value.b);
        writer.WriteNumber("A", value.a);
        writer.WriteEndObject();
    }
}