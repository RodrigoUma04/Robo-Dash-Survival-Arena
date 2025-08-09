using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class StartMenuScreen : IScreen
{
    Button startButton;
    Button optionsButton;

    public void LoadContent(ContentManager content)
    {
        var normalTexture = content.Load<Texture2D>("UI/MenuButtons/button_rectangle_flat.png");
        var pressedTexture = content.Load<Texture2D>("UI/MenuButtons/button_rectangle_depth_flat.png");
        var font = content.Load<SpriteFont>(""); // TODO add font

        startButton = new Button(normalTexture, pressedTexture, font, "Start", new Vector2(100, 100));
        optionsButton = new Button(normalTexture, pressedTexture, font, "Options", new Vector2(100, 200));

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

        startButton.Draw(spriteBatch);
        optionsButton.Draw(spriteBatch);

        spriteBatch.End();
    }
}