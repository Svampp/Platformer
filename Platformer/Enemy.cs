using Raylib_CsLo;
using System.Numerics;

/// <summary>
/// Represents an enemy in the game.
/// </summary>
public class Enemy : GameObject
{
    public float speed = 3f;
    private Vector2 startPos;
    public float moveRange = 100f;
    private bool movingRight = true;

    /// <summary>
    /// Initializes a new instance of the <see cref="Enemy"/> class.
    /// </summary>
    /// <param name="position">The initial position of the enemy.</param>
    public Enemy(Vector2 position, Vector2 size, Color color, float speed, float moveRange)
        : base(position, size, color)
    {
        startPos = position;
    }

    /// <summary>
    /// Updates the enemy's position and movement logic.
    /// </summary>
    public void Update()
    {
        if (movingRight)
            Position.X += speed;
        else
            Position.X -= speed;

        if (Position.X > startPos.X + moveRange) movingRight = false;
        if (Position.X < startPos.X - moveRange) movingRight = true;
    }
}