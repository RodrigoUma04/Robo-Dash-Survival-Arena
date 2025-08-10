using Microsoft.Xna.Framework;

public interface IInputHandler
{
    bool IsPressed(Rectangle bounds);
    bool IsReleased(Rectangle bounds);
}