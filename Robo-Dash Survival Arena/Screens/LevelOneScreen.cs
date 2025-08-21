using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;

public class LevelOneScreen : LevelScreen
{
    public LevelOneScreen(GameStateManager gameStateManager) : base(gameStateManager) { }

    public override string Song { get; set; } = "level1";

    public override void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
    {
        _map = content.Load<TiledMap>("Tiled/Maps/level_1");
        base.LoadContent(content, graphicsDevice);
    }
}