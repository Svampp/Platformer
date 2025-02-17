using Raylib_CsLo;
using System.Numerics;

/// <summary>
/// The main program class for the 2D Platformer game.
/// </summary>
class Program
{
    /// <summary>
    /// Enum representing the different screens in the game.
    /// </summary>
    enum Screen { Menu, Playing, GameOver }

    /// <summary>
    /// The main entry point for the game.
    /// </summary>
    static void Main()
    {
        const int screenWidth = 1920;
        const int screenHeight = 1080;
        Raylib.InitWindow(screenWidth, screenHeight, "2D Platformer");
        Raylib.SetTargetFPS(60);

        Screen currentScreen = Screen.Menu;
        GameState gameState = GameState.Playing;

        // Load level data
        LevelData levelData = LevelLoader.LoadLevel("level1.json");
        List<GameObject> platforms = LevelLoader.CreatePlatforms(levelData.Platforms);
        List<GameObject> collectibles = LevelLoader.CreateCollectibles(levelData.Collectibles);
        List<Enemy> enemies = LevelLoader.CreateEnemies(levelData.Enemies);

        Player player = new Player(new Vector2(100, 350));
        int collectedCount = 0;
        int totalCollectibles = collectibles.Count;

        Camera2D camera = new Camera2D();
        camera.zoom = 1.0f;

        float levelWidth = 1920;
        float levelHeight = 1080;


        while (!Raylib.WindowShouldClose())
        {
            // Update camera position to follow the player
            camera.target = player.Position + new Vector2(player.Size.X / 2, player.Size.Y / 2);
            camera.offset = new Vector2(screenWidth / 2, screenHeight / 2);

            // Clamp camera movement within level bounds
            if (levelWidth >= screenWidth)
                camera.target.X = Math.Clamp(camera.target.X, screenWidth / 2, levelWidth - screenWidth / 2);
            else
                camera.target.X = levelWidth / 2;

            if (levelHeight >= screenHeight)
                camera.target.Y = Math.Clamp(camera.target.Y, screenHeight / 2, levelHeight - screenHeight / 2);
            else
                camera.target.Y = levelHeight / 2;

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.WHITE);

            if (currentScreen == Screen.Menu)
            {
                // Draw main menu
                Raylib.DrawText("2D Platformer", screenWidth / 2 - 150, screenHeight / 4, 50, Raylib.BLACK);
                Rectangle startButton = new Rectangle(screenWidth / 2 - 100, screenHeight / 2, 200, 50);
                Rectangle exitButton = new Rectangle(screenWidth / 2 - 100, screenHeight / 2 + 70, 200, 50);
                Raylib.DrawRectangleRec(startButton, Raylib.LIGHTGRAY);
                Raylib.DrawRectangleRec(exitButton, Raylib.LIGHTGRAY);
                Raylib.DrawText("Start", (int)startButton.x + 50, (int)startButton.y + 10, 30, Raylib.BLACK);
                Raylib.DrawText("Exit", (int)exitButton.x + 50, (int)exitButton.y + 10, 30, Raylib.BLACK);

                // Handle menu button clicks
                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT) &&
                    Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), startButton))
                    currentScreen = Screen.Playing;

                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT) &&
                    Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), exitButton))
                    break;
            }
            else if (currentScreen == Screen.Playing)
            {
                // Update game state
                gameState = player.Update(platforms, collectibles, enemies, ref collectedCount, totalCollectibles);
                foreach (var enemy in enemies) enemy.Update();

                Raylib.BeginMode2D(camera);
                foreach (var platform in platforms) platform.Draw();
                foreach (var item in collectibles) item.Draw();
                foreach (var enemy in enemies) enemy.Draw();
                player.Draw();
                Raylib.EndMode2D();

                // Display collected items
                Raylib.DrawText($"Collected: {collectedCount} / {totalCollectibles}", 20, 20, 30, Raylib.BLACK);

                if (gameState != GameState.Playing)
                    currentScreen = Screen.GameOver;
            }
            else if (currentScreen == Screen.GameOver)
            {
                // Display game over or victory message
                string message = gameState == GameState.Won ? "YOU WON!" : "GAME OVER";
                Raylib.DrawText(message, screenWidth / 2 - 100, screenHeight / 3, 50, Raylib.RED);
                Rectangle restartButton = new Rectangle(screenWidth / 2 - 100, screenHeight / 2, 200, 50);
                Raylib.DrawRectangleRec(restartButton, Raylib.LIGHTGRAY);
                Raylib.DrawText("Main Menu", (int)restartButton.x + 20, (int)restartButton.y + 10, 30, Raylib.BLACK);

                // Handle restart button click
                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT) &&
                    Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), restartButton))
                {
                    currentScreen = Screen.Menu;
                    player = new Player(new Vector2(100, 350));
                    collectedCount = 0;

                    // Logic for loading next level (currently commented out)
                    // currentLevel++;
                    // LoadNextLevel(currentLevel);
                }
            }
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}