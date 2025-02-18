using Raylib_CsLo;
using System.Numerics;

/// <summary>
/// Represents an enemy in the game.
/// </summary>
public class Enemy : GameObject
{
    /// <summary>
    /// The movement speed of the enemy.
    /// </summary>
    public float speed = 3f;

    /// <summary>
    /// The starting position of the enemy.
    /// </summary>
    private Vector2 startPos;

    /// <summary>
    /// The range within which the enemy moves back and forth.
    /// </summary>
    public float moveRange = 100f;

    /// <summary>
    /// Determines whether the enemy is currently moving to the right.
    /// </summary>
    private bool movingRight = true;

    /// <summary>
    /// Initializes a new instance of the <see cref="Enemy"/> class.
    /// </summary>
    /// <param name="position">The initial position of the enemy.</param>
    /// <param name="size">The size of the enemy.</param>
    /// <param name="color">The color of the enemy.</param>
    /// <param name="speed">The speed of the enemy.</param>
    /// <param name="moveRange">The range of movement for the enemy.</param>
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
