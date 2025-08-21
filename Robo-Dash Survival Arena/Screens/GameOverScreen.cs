using Microsoft.Xna.Framework;

public class GameOverScreen : MenuScreen
{
    public GameOverScreen(MouseInputHandler mouseInputHandler, GameStateManager gameStateManager) : base(mouseInputHandler, gameStateManager)
    {}

    public override string Text { get; set; } = "Game Over!";
    public override string Title { get; set; } = "Exit";
    public override Color TitleColor { get; set; } = Color.Red;

    public override void OnClick()
    {
        //TODO close game
    }
}