using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

public class Button
{
    private Texture2D normalTexture;
    private Texture2D pressedTexture;
    private SpriteFont font;
    private string text;

    public Vector2 Position { get; private set; }
    public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, normalTexture.Width, normalTexture.Height);

    public event System.Action OnClick;

    private MouseState previousMouseState;
    private bool isPressed;

    public Button(Texture2D normalTexture, Texture2D pressedTexture,  SpriteFont font, string text, Vector2 position)
    {
        this.normalTexture = normalTexture;
        this.pressedTexture = pressedTexture;
        this.font = font;
        this.text = text;
        this.Position = position;
    }

    public void Update()
    {
        MouseState mouseState = Mouse.GetState();
        var mouseRect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

        bool wasPressed = isPressed;
        isPressed = false;

        if (mouseRect.Intersects(BoundingBox))
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                isPressed = true;
            }
            else if (mouseState.LeftButton == ButtonState.Released && wasPressed)
            {
                OnClick?.Invoke();
            }
        }

        previousMouseState = mouseState;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var texture = isPressed ? pressedTexture : normalTexture;
        spriteBatch.Draw(texture, Position, Color.White);

        Vector2 textSize = font.MeasureString(text);
        Vector2 textPosition = Position + new Vector2(
            (texture.Width - textSize.X) / 2,
            (texture.Height - textSize.Y) / 2);

        spriteBatch.DrawString(font, text, textPosition, Color.Black);
    }
}