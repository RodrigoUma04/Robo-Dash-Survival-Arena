using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class NextLevelScreen : IGameState
{
    private Button _nextButton;
    private Texture2D _backgroundTile;
    private ButtonFactory _buttonFactory;
    private MouseInputHandler _mouseInputHandler;
    private SpriteFont _font;
    private GameStateManager _gameStateManager;

    public NextLevelScreen(MouseInputHandler mouseInputHandler, GameStateManager gameStateManager)
    {
        this._mouseInputHandler = mouseInputHandler;
        this._gameStateManager = gameStateManager;
    }

    public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
    {
        _backgroundTile = content.Load<Texture2D>("kenney_new-platformer-pack-1.0/Sprites/Backgrounds/Double/background_color_trees");
        _buttonFactory = new ButtonFactory(content, _mouseInputHandler);
        _font = content.Load<SpriteFont>("UI/Font");

        _nextButton = _buttonFactory.CreateButton("Next", new Vector2(416, 250));

        _nextButton.OnClick += () =>
        {
            _gameStateManager.NextLevel();
        };
    }

    public void Update(GameTime gameTime)
    {
        _nextButton.Update();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();

        for (int x = 0; x < 1024; x += _backgroundTile.Width)
        {
            spriteBatch.Draw(_backgroundTile, new Vector2(x, 0), Color.White);
        }

        string title = "Next Level";
        float scale = 1.5f;
        Vector2 titleSize = _font.MeasureString(title) * scale;
        Vector2 titlePos = new Vector2(
            (1024 - titleSize.X) / 2,
            150 - titleSize.Y / 2
        );
        spriteBatch.DrawString(_font, title, titlePos, Color.Black, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);


        _nextButton.Draw(spriteBatch);

        spriteBatch.End();
    }
}