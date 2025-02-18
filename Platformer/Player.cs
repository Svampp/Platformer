using System.Collections.Generic;
using System.Numerics;
using Raylib_CsLo;

/// <summary>
/// Represents the player character in the game.
/// </summary>
class Player : GameObject
{
    /// <summary>
    /// The velocity of the player.
    /// </summary>
    public Vector2 Velocity;

    /// <summary>
    /// The gravitational force applied to the player.
    /// </summary>
    private const float Gravity = 0.5f;

    /// <summary>
    /// The force applied when the player jumps.
    /// </summary>
    private const float JumpForce = -10f;

    /// <summary>
    /// The movement speed of the player.
    /// </summary>
    private const float MoveSpeed = 5f;

    /// <summary>
    /// Indicates whether the player is currently on the ground.
    /// </summary>
    public bool IsOnGround;

    /// <summary>
    /// Initializes a new instance of the <see cref="Player"/> class.
    /// </summary>
    /// <param name="position">The initial position of the player.</param>
    public Player(Vector2 position) : base(position, new Vector2(40, 40), Raylib.RED) { }

    /// <summary>
    /// Updates the player's state based on input, collisions, and interactions with objects in the game world.
    /// </summary>
    /// <param name="platforms">The list of platforms in the game.</param>
    /// <param name="collectibles">The list of collectible items in the game.</param>
    /// <param name="enemies">The list of enemies in the game.</param>
    /// <param name="collectedCount">The number of collectibles collected by the player (passed by reference).</param>
    /// <param name="totalCollectibles">The total number of collectibles in the level.</param>
    /// <param name="collisionMessage">An output message describing any collisions detected.</param>
    /// <returns>The current game state after updating (Playing, Won, or Lost).</returns>
    public GameState Update(List<GameObject> platforms, List<GameObject> collectibles, List<Enemy> enemies, ref int collectedCount, int totalCollectibles, out string collisionMessage)
    {
        collisionMessage = "";

        // Apply gravity
        Velocity.Y += Gravity;

        // Handle movement input
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) Velocity.X = -MoveSpeed;
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) Velocity.X = MoveSpeed;
        else Velocity.X = 0;

        // Handle jumping
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && IsOnGround)
        {
            Velocity.Y = JumpForce;
            IsOnGround = false;
        }

        // Update position
        Position += Velocity;

        // Check screen boundaries
        if (Position.X < 0)
        {
            Position.X = 0;
            collisionMessage = "You hit a wall!";
        }
        if (Position.X + Size.X > 1920)
        {
            Position.X = 1920 - Size.X;
            collisionMessage = "You hit a wall!";
        }
        if (Position.Y < 0)
        {
            Position.Y = 0;
            collisionMessage = "You hit a ceiling!";
        }
        if (Position.Y + Size.Y > 1080)
        {
            Position.Y = 1080 - Size.Y;
            collisionMessage = "You hit the ground!";
        }

        // Check collisions with platforms
        foreach (var platform in platforms)
        {
            if (CheckCollision(platform))
            {
                Position.Y = platform.Position.Y - Size.Y;
                IsOnGround = true;
                Velocity.Y = 0;
            }
        }

        // Check collisions with collectibles
        for (int i = collectibles.Count - 1; i >= 0; i--)
        {
            if (CheckCollision(collectibles[i]))
            {
                collectibles.RemoveAt(i);
                collectedCount++;
            }
        }

        // Check collisions with enemies
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (CheckCollision(enemies[i]))
            {
                if (Velocity.Y > 0)
                {
                    enemies.RemoveAt(i);
                    Velocity.Y = JumpForce / 2; // Bounce after defeating an enemy
                }
                else
                {
                    collisionMessage = "You were killed by an enemy!";
                    return GameState.Lost;
                }
            }
        }

        // Determine game state
        return collectedCount == totalCollectibles ? GameState.Won : GameState.Playing;
    }

    /// <summary>
    /// Checks for a collision between the player and another game object.
    /// </summary>
    /// <param name="obj">The game object to check for collision.</param>
    /// <returns>True if a collision occurs, otherwise false.</returns>
    private bool CheckCollision(GameObject obj)
    {
        return Position.X < obj.Position.X + obj.Size.X &&
               Position.X + Size.X > obj.Position.X &&
               Position.Y < obj.Position.Y + obj.Size.Y &&
               Position.Y + Size.Y > obj.Position.Y;
    }
}
