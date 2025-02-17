using Raylib_CsLo;
using System.Collections.Generic;
using System.Numerics;
using System.Text.Json.Serialization;
using System.Text.Json;

public class LevelData
{
    public List<PlatformData> Platforms { get; set; } = new List<PlatformData>();
    public List<CollectibleData> Collectibles { get; set; } = new List<CollectibleData>();
    public List<EnemyData> Enemies { get; set; } = new List<EnemyData>();
}

public class PlatformData
{
    public Vector2 Position { get; set; } 
    public Vector2 Size { get; set; } 
    public Color Color { get; set; }
}

public class CollectibleData
{
    public Vector2 Position { get; set; } 
    public Vector2 Size { get; set; } 
    public Color Color { get; set; } 
}

public class EnemyData
{
    public Vector2 Position { get; set; } 
    public Vector2 Size { get; set; } 
    public Color Color { get; set; } 
    public float Speed { get; set; }
    public float MoveRange { get; set; }
}

public class Vector2Converter : JsonConverter<Vector2>
{
    public override Vector2 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            var root = doc.RootElement;
            float x = root.GetProperty("x").GetSingle();
            float y = root.GetProperty("y").GetSingle();
            return new Vector2(x, y);
        }
    }

    public override void Write(Utf8JsonWriter writer, Vector2 value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("x", value.X);
        writer.WriteNumber("y", value.Y);
        writer.WriteEndObject();
    }
}


