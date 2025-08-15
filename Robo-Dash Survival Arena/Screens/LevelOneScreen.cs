using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

public class LevelOneScreen : IGameState
{
    private TiledMap _map;
    private TiledMapRenderer _mapRenderer;
    private Hero _hero;
    private Camera _camera;

    public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
    {
        _map = content.Load<TiledMap>("Tiled/Maps/level_1");
        _mapRenderer = new TiledMapRenderer(graphicsDevice, _map);

        var characterLayer = _map.GetLayer<TiledMapObjectLayer>("Spawns");
        if (characterLayer != null)
        {
            foreach (var obj in characterLayer.Objects)
            {
                if (obj.Properties.ContainsKey("type") && obj.Properties["type"].ToString() == "hero")
                {
                    _hero = new Hero(new Vector2(obj.Position.X, obj.Position.Y));
                    _hero.LoadContent(content);
                    break;
                }
            }
        }

        _camera = new Camera(graphicsDevice, _hero.Position);
    }

    public void Update(GameTime gameTime)
    {
        _mapRenderer.Update(gameTime);
        _hero.Update(gameTime);
        _camera.Update(_hero.Position, _map);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var viewMatrix = _camera.GetViewMatrix();

        _mapRenderer.Draw(viewMatrix);

        spriteBatch.Begin(transformMatrix: viewMatrix);
        _hero.Draw(spriteBatch);
        spriteBatch.End();
    }
}