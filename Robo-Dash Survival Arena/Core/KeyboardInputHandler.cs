using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class KeyboardInputHandler : IInputHandler
{
    private KeyboardState _currentState;
    private KeyboardState _previousState;

    private Hero _hero;

    public KeyboardInputHandler(Hero hero)
    {
        _hero = hero;
    }

    public void Update(GameTime gameTime)
    {
        _previousState = _currentState;
        _currentState = Keyboard.GetState();

        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Vector2 moveDir = Vector2.Zero;

        if (_currentState.IsKeyDown(Keys.A)) moveDir.X -= 1;
        if (_currentState.IsKeyDown(Keys.D)) moveDir.X += 1;

        _hero.Move(moveDir);

        if (IsKeyPressed(Keys.W))
            _hero.Jump();
    }

    private bool IsKeyPressed(Keys key)
    {
        return _previousState.IsKeyUp(key) && _currentState.IsKeyDown(key);
    }


}