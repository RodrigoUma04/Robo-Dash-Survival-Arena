using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public abstract class MenuScreen : IGameState
{
    private Button _button;
    public abstract string Text { get; set; }
    public abstract string Title { get; set; }
    public abstract Color TitleColor { get; set; }
    private Texture2D _backgroundTile;
    private ButtonFactory _buttonFactory;
    private MouseInputHandler _mouseInputHandler;
    private SpriteFont _font;
    protected GameStateManager _gameStateManager;

    public MenuScreen(MouseInputHandler mouseInputHandler, GameStateManager gameStateManager)
    {
        this._mouseInputHandler = mouseInputHandler;
        this._gameStateManager = gameStateManager;
    }

    public abstract void OnClick();

    public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
    {
        _backgroundTile = content.Load<Texture2D>("kenney_new-platformer-pack-1.0/Sprites/Backgrounds/Double/background_color_trees");
        _buttonFactory = new ButtonFactory(content, _mouseInputHandler);
        _font = content.Load<SpriteFont>("UI/Font");

        _button = _buttonFactory.CreateButton(Text, new Vector2(416, 250));

        _button.OnClick += () =>
        {
            OnClick();
        };
    }

    public void Update(GameTime gameTime)
    {
        _button.Update();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();

        for (int x = 0; x < 1024; x += _backgroundTile.Width)
        {
            spriteBatch.Draw(_backgroundTile, new Vector2(x, 0), Color.White);
        }

        float scale = 1.5f;
        Vector2 titleSize = _font.MeasureString(Title) * scale;
        Vector2 titlePos = new Vector2(
            (1024 - titleSize.X) / 2,
            150 - titleSize.Y / 2
        );
        spriteBatch.DrawString(_font, Title, titlePos, TitleColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);


        _button.Draw(spriteBatch);

        spriteBatch.End();
    }
}