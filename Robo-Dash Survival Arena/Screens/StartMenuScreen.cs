using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class StartMenuScreen : IGameState
{
    Button startButton;
    Button optionsButton;
    private Texture2D backgroundTile;
    private ButtonFactory buttonFactory;

    public void LoadContent(ContentManager content)
    {
        backgroundTile = content.Load<Texture2D>("Backgrounds/background_color_trees");
        buttonFactory = new ButtonFactory(content, new MouseInputHandler());

        startButton = buttonFactory.CreateButton("Start", new Vector2(100, 100));
        optionsButton = buttonFactory.CreateButton("Options", new Vector2(100, 200));

        // TODO add state handlings
        startButton.OnClick += () => { };
        optionsButton.OnClick += () => { };
    }

    public void Update(GameTime gameTime)
    {
        startButton.Update();
        optionsButton.Update();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();

        for (int x = 0; x < 800; x += backgroundTile.Width)
        {
            for (int y = 0; y < 480; y += backgroundTile.Height)
            {
                spriteBatch.Draw(backgroundTile, new Vector2(x, y), Color.White);
            }
        }

        startButton.Draw(spriteBatch);
        optionsButton.Draw(spriteBatch);

        spriteBatch.End();
    }
}