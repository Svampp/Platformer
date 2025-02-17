using Raylib_CsLo;
using System.Collections.Generic;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

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
    [JsonConverter(typeof(ColorConverter))]
    public Color Color { get; set; }
}

public class CollectibleData
{
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    [JsonConverter(typeof(ColorConverter))]
    public Color Color { get; set; }
}

public class EnemyData
{
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    [JsonConverter(typeof(ColorConverter))]
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
            float x = root.GetProperty("X").GetSingle();
            float y = root.GetProperty("Y").GetSingle();
            return new Vector2(x, y);
        }
    }

    public override void Write(Utf8JsonWriter writer, Vector2 value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("X", value.X);
        writer.WriteNumber("Y", value.Y);
        writer.WriteEndObject();
    }
}

public class ColorConverter : JsonConverter<Color>
{
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