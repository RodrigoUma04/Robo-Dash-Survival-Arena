using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class MouseInputHandler : IInputHandler
{
    private MouseState _prevState;
    private MouseState _currentState;

    public void Update(GameTime gameTime)
    {
        _prevState = _currentState;
        _currentState = Mouse.GetState();
    }

    public bool IsPressed(Rectangle bounds)
    {
        bool inside = bounds.Contains(_currentState.Position);
        return inside && _currentState.LeftButton == ButtonState.Pressed;
    }

    public bool IsReleased(Rectangle bounds)
    {
        bool inside = bounds.Contains(_currentState.Position);
        return inside &&
               _prevState.LeftButton == ButtonState.Pressed &&
               _currentState.LeftButton == ButtonState.Released;
    }
}