using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

public class LevelOneScreen : IGameState
{
    private TiledMap _map;
    private TiledMapRenderer _mapRenderer;
    private Entity _hero;
    private List<Entity> _entities = new List<Entity>();
    private Camera _camera;
    private KeyboardInputHandler _keyboardInputHandler;

    public LevelOneScreen()
    {
        _hero = new Hero();
        _entities.Add(_hero);
        
        _keyboardInputHandler = new KeyboardInputHandler((Hero)_hero);
    }

    public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
    {
        _map = content.Load<TiledMap>("Tiled/Maps/level_1");
        _mapRenderer = new TiledMapRenderer(graphicsDevice, _map);

        foreach (var entity in _entities)
        {
            entity.Spawn(_map, content);
        }

        _camera = new Camera(graphicsDevice, _hero.Position);
    }

    public void Update(GameTime gameTime)
    {
        _mapRenderer.Update(gameTime);

        foreach (var entity in _entities)
        {
            entity.Update(gameTime);
        }

        _keyboardInputHandler.Update(gameTime);
        _camera.Update(_hero.Position, _map);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var viewMatrix = _camera.GetViewMatrix();

        _mapRenderer.Draw(viewMatrix);

        spriteBatch.Begin(transformMatrix: viewMatrix);

        foreach (var entity in _entities)
        {
            entity.Draw(spriteBatch);
        }

        spriteBatch.End();
    }
}