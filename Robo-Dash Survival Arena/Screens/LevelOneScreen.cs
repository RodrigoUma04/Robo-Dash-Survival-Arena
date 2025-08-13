using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

public class LevelOneScreen : IGameState
{
    private TiledMap _map;
    private TiledMapRenderer _mapRenderer;

    public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
    {
        _map = content.Load<TiledMap>("Tiled/Maps/level_1");
        _mapRenderer = new TiledMapRenderer(graphicsDevice, _map);
    }

    public void Update(GameTime gameTime)
    {
        _mapRenderer.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        _mapRenderer.Draw();
        spriteBatch.End();
    }
}