using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class GameStateManager
{
    private Dictionary<string, IGameState> _gameStates = new();
    private IGameState _activeGameState;

    public void AddGameState(string key, IGameState gameState)
    {
        _gameStates[key] = gameState;
    }

    public void setActiveGameState(string key, ContentManager content, GraphicsDevice graphicsDevice)
    {
        if (_gameStates.ContainsKey(key))
        {
            _activeGameState = _gameStates[key];
            _activeGameState.LoadContent(content, graphicsDevice);
        }
    }

    public void Update(GameTime gameTime)
    {
        _activeGameState?.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _activeGameState?.Draw(spriteBatch);
    }
}