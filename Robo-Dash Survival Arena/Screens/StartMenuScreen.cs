using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class StartMenuScreen : IGameState
{
    private Button _startButton;
    private Button _optionsButton;
    private Texture2D _backgroundTile;
    private ButtonFactory _buttonFactory;
    private MouseInputHandler _mouseInputHandler;
    private SpriteFont _font;

    public StartMenuScreen(MouseInputHandler mouseInputHandler)
    {
        this._mouseInputHandler = mouseInputHandler;
    }

    public void LoadContent(ContentManager content)
    {
        _backgroundTile = content.Load<Texture2D>("Backgrounds/background_color_trees");
        _buttonFactory = new ButtonFactory(content, _mouseInputHandler);

        _font = content.Load<SpriteFont>("UI/Font");

        _startButton = _buttonFactory.CreateButton("Start", new Vector2(416, 250));
        _optionsButton = _buttonFactory.CreateButton("Options", new Vector2(416, 350));

        // TODO add state handlings
        _startButton.OnClick += () => { };
        _optionsButton.OnClick += () => { };
    }

    public void Update(GameTime gameTime)
    {
        _startButton.Update();
        _optionsButton.Update();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();

        for (int x = 0; x < 1024; x += _backgroundTile.Width)
        {
            spriteBatch.Draw(_backgroundTile, new Vector2(x, 0), Color.White);
        }

        string title = "Robo-Dash Survival Arena";
        float scale = 1.5f;
        Vector2 titleSize = _font.MeasureString(title) * scale;
        Vector2 titlePos = new Vector2(
            (1024 - titleSize.X) / 2,
            150 - titleSize.Y / 2
        );
        spriteBatch.DrawString(_font, title, titlePos, Color.Black, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);


        _startButton.Draw(spriteBatch);
        _optionsButton.Draw(spriteBatch);

        spriteBatch.End();
    }
}