using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class Button
{
    private Texture2D _normalTexture;
    private Texture2D _pressedTexture;
    private SpriteFont _font;
    private string _text;
    private MouseInputHandler _inputHandler;
    private bool _isPressed;
    private bool _wasPressedLastUpdate = false;

    public Vector2 Position { get; private set; }
    public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, _normalTexture.Width, _normalTexture.Height);

    public event System.Action OnClick;

    public Button(Texture2D normalTexture, Texture2D pressedTexture, SpriteFont font, string text, Vector2 position, MouseInputHandler inputHandler)
    {
        this._normalTexture = normalTexture;
        this._pressedTexture = pressedTexture;
        this._font = font;
        this._text = text;
        this.Position = position;
        this._inputHandler = inputHandler;
    }

    public void Update()
    {
        bool isCurrentlyPressed = _inputHandler.IsPressed(BoundingBox);
        if (isCurrentlyPressed && !_wasPressedLastUpdate)
        {
            SoundManager.getInstance().Play("click_a");
            _isPressed = true;
        }
        
        if (!_inputHandler.IsPressed(BoundingBox) && _wasPressedLastUpdate && _isPressed)
        {
            SoundManager.getInstance().Play("click_b");
            OnClick?.Invoke();
            _isPressed = false;
        }

        _wasPressedLastUpdate = isCurrentlyPressed;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var texture = _isPressed ? _pressedTexture : _normalTexture;
        spriteBatch.Draw(texture, Position, Color.White);

        Vector2 textSize = _font.MeasureString(_text);
        Vector2 textPosition = Position + new Vector2(
            (texture.Width - textSize.X) / 2,
            (texture.Height - textSize.Y) / 2);

        spriteBatch.DrawString(_font, _text, textPosition, Color.Black);
    }
}