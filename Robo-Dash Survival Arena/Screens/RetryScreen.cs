using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class RetryScreen : IGameState
{
    private Button _retryButton;
    private Texture2D _backgroundTile;
    private ButtonFactory _buttonFactory;
    private MouseInputHandler _mouseInputHandler;
    private SpriteFont _font;
    private GameStateManager _gameStateManager;

    public RetryScreen(MouseInputHandler mouseInputHandler, GameStateManager gameStateManager)
    {
        this._mouseInputHandler = mouseInputHandler;
        this._gameStateManager = gameStateManager;
    }

    public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
    {
        _backgroundTile = content.Load<Texture2D>("kenney_new-platformer-pack-1.0/Sprites/Backgrounds/Double/background_color_trees");
        _buttonFactory = new ButtonFactory(content, _mouseInputHandler);
        _font = content.Load<SpriteFont>("UI/Font");

        PlayerData playerData = PlayerData.getInstance();

        _retryButton = _buttonFactory.CreateButton("Try again", new Vector2(416, 250));

        _retryButton.OnClick += () =>
        {
            _gameStateManager.RestartLevel();
        };
    }

    public void Update(GameTime gameTime)
    {
        _retryButton.Update();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();

        for (int x = 0; x < 1024; x += _backgroundTile.Width)
        {
            spriteBatch.Draw(_backgroundTile, new Vector2(x, 0), Color.White);
        }

        string title = "You fell into a weird toxic liquid!";
        float scale = 1.5f;
        Vector2 titleSize = _font.MeasureString(title) * scale;
        Vector2 titlePos = new Vector2(
            (1024 - titleSize.X) / 2,
            150 - titleSize.Y / 2
        );
        spriteBatch.DrawString(_font, title, titlePos, Color.Black, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

        _retryButton.Draw(spriteBatch);

        spriteBatch.End();
    }
}