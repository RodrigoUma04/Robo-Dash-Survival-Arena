using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class MouseInputHandler : IInputHandler
{
    private MouseState _prevState;

    public bool IsPressed(Rectangle bounds)
    {
        var state = Mouse.GetState();
        bool inside = bounds.Contains(state.Position);
        bool pressed = inside && state.LeftButton == ButtonState.Pressed;
        _prevState = state;
        return pressed;
    }

    public bool IsReleased(Rectangle bounds)
    {
        var state = Mouse.GetState();
        bool inside = bounds.Contains(state.Position);
        bool released = inside && _prevState.LeftButton == ButtonState.Pressed && state.LeftButton == ButtonState.Released;
        _prevState = state;
        return released; 
    }
}