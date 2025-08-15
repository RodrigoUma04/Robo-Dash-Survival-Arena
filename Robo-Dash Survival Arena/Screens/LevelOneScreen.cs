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
    private OrthographicCamera _camera;

    public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
    {
        _map = content.Load<TiledMap>("Tiled/Maps/level_1");
        _mapRenderer = new TiledMapRenderer(graphicsDevice, _map);

        _camera = new OrthographicCamera(graphicsDevice);

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

        _camera.LookAt(_hero.Position);
    }

    public void Update(GameTime gameTime)
    {
        _mapRenderer.Update(gameTime);
        _hero.Update(gameTime);
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        // FIXME move this to camera class
        var heroPos = _hero.Position;

        float camX = MathHelper.Clamp(heroPos.X, 512, _map.WidthInPixels - 512);
        float camY = MathHelper.Clamp(heroPos.Y, 256, _map.HeightInPixels - 256);

        _camera.LookAt(new Vector2(camX, camY));
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