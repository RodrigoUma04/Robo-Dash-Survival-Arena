using Microsoft.Xna.Framework;

public class RetryScreen : MenuScreen
{
    public RetryScreen(MouseInputHandler mouseInputHandler, GameStateManager gameStateManager) : base(mouseInputHandler, gameStateManager)
    {}

    public override string Text { get; set; } = "Retry";
    public override string Title { get; set; } = "You fell into a weird toxic liquid!";
    public override Color TitleColor { get; set; } = Color.Black;

    public override void OnClick()
    {
        _gameStateManager.RestartLevel();
    }
}