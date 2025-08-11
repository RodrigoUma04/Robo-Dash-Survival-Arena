using Microsoft.Xna.Framework;

public interface IInputHandler
{
    void Update();
    bool IsPressed(Rectangle bounds);
    bool IsReleased(Rectangle bounds);
}