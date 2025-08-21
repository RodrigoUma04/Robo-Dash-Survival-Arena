using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class GameStateManager : IObserver
{
    private Dictionary<string, IGameState> _gameStates = new();
    private IGameState _activeGameState;
    private IGameState _previousGameState;
    private int _currentLevel = 1;

    private ContentManager _content;
    private GraphicsDevice _graphicsDevice;

    public GameStateManager(ContentManager content, GraphicsDevice graphicsDevice)
    {
        _content = content;
        _graphicsDevice = graphicsDevice;
    }

    public void AddGameState(string key, IGameState gameState)
    {
        _gameStates[key] = gameState;
    }

    public void SetActiveGameState(string key)
    {
        if (_gameStates.TryGetValue(key, out var gameState))
        {
            _previousGameState = _activeGameState;
            _activeGameState = gameState;
            _activeGameState.LoadContent(_content, _graphicsDevice);
        }
    }

    public void StartGame()
    {
        SetActiveGameState($"Level{_currentLevel}Screen");
    }

    public void RestartLevel()
    {
        if (_previousGameState is LevelScreen levelScreen)
        {
            System.Console.WriteLine("Clearing entities");
            levelScreen.Entities.Clear();
        }
        StartGame();
    }

    public void NextLevel()
    {
        _currentLevel++;
        StartGame();
    }

    public void EndGame()
    {
        if (_previousGameState is LevelScreen levelScreen)
        {
            System.Console.WriteLine("Clearing entities");
            levelScreen.Entities.Clear();
        }

        SetActiveGameState("GameOver");
    }

    public void Update(GameTime gameTime)
    {
        _activeGameState?.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _activeGameState?.Draw(spriteBatch);
    }

    public void Update(string eventType, int value)
    {
        if (eventType == "Lives" && value <= 0)
        {
            EndGame();
        }
    }
}