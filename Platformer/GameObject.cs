using Raylib_CsLo;
using System.Numerics;

/// <summary>
/// Represents a game object with a position, size, and color.
/// </summary>
public class GameObject
{
    /// <summary>
    /// The position of the game object.
    /// </summary>
    public Vector2 Position;

    /// <summary>
    /// The size of the game object.
    /// </summary>
    public Vector2 Size;

    /// <summary>
    /// The color of the game object.
    /// </summary>
    public Color Color;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameObject"/> class.
    /// </summary>
    /// <param name="position">The position of the game object.</param>
    /// <param name="size">The size of the game object.</param>
    /// <param name="color">The color of the game object.</param>
    public GameObject(Vector2 position, Vector2 size, Color color)
    {
        Position = position;
        Size = size;
        Color = color;
    }

    /// <summary>
    /// Draws the game object on the screen.
    /// </summary>
    public void Draw()
    {
        Raylib.DrawRectangleV(Position, Size, Color);
    }
}