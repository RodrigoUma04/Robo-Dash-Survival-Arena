using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class ButtonFactory
{
    private Texture2D _normalTexture;
    private Texture2D _pressedTexture;
    private SpriteFont _font;
    private MouseInputHandler _inputHandler;

    public ButtonFactory(ContentManager content, MouseInputHandler inputHandler)
    {
        _normalTexture = content.Load<Texture2D>("UI/Buttons/button_rectangle_depth_flat");
        _pressedTexture = content.Load<Texture2D>("UI/Buttons/button_rectangle_flat");
        _font = content.Load<SpriteFont>("UI/Font");
        this._inputHandler = inputHandler;
    }

    public Button CreateButton(string text, Vector2 position)
    {
        return new Button(_normalTexture, _pressedTexture, _font, text, position, _inputHandler);
    }
}
