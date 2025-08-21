using Microsoft.Xna.Framework;

public class GameOverScreen : MenuScreen
{
    public GameOverScreen(MouseInputHandler mouseInputHandler, GameStateManager gameStateManager) : base(mouseInputHandler, gameStateManager)
    {}

    public override string Text { get; set; } = "Exit";
    public override string Title { get; set; } = "Game Over!";
    public override Color TitleColor { get; set; } = Color.Red;

    public override void OnClick()
    {
        //TODO close game
    }
}