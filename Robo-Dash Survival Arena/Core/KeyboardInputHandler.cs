using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class KeyboardInputHandler : IInputHandler
{
    private KeyboardState _currentState;
    private KeyboardState _previousState;

    private Hero _hero;
    private GameStateManager _gameStateManager;
    private Camera _camera;

    public KeyboardInputHandler(Hero hero, GameStateManager gameStateManager, Camera camera)
    {
        _hero = hero;
        _gameStateManager = gameStateManager;
        _camera = camera;
    }

    public void Update(GameTime gameTime)
    {
        _previousState = _currentState;
        _currentState = Keyboard.GetState();

        Vector2 moveDir = Vector2.Zero;

        if (_currentState.IsKeyDown(Keys.A)) moveDir.X -= 1;
        if (_currentState.IsKeyDown(Keys.D)) moveDir.X += 1;

        _hero.Move(moveDir);

        if (IsKeyPressed(Keys.W))
            _hero.Jump();

        if (_gameStateManager.CurrentLevel == 3 && _hero.CanShake && IsKeyPressed(Keys.E))
        {
            _camera.Shake(10f, 2f);
            _hero.HasShaken = true;
        }
    }

    private bool IsKeyPressed(Keys key)
    {
        return _previousState.IsKeyUp(key) && _currentState.IsKeyDown(key);
    }


}