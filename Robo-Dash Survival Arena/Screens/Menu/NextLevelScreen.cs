using Microsoft.Xna.Framework;

public class NextLevelScreen : MenuScreen
{
    public NextLevelScreen(MouseInputHandler mouseInputHandler, GameStateManager gameStateManager) : base(mouseInputHandler, gameStateManager)
    {}

    public override string Text { get; set; } = "Continue";
    public override string Title { get; set; } = "Next Level";
    public override Color TitleColor { get; set; } = Color.Black;

    public override void OnClick()
    {
        _gameStateManager.NextLevel();
    }
}