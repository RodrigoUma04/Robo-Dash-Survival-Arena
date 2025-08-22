using Microsoft.Xna.Framework;

public class GameOverScreen : MenuScreen
{
    private Game _game;
    public GameOverScreen(Game game, MouseInputHandler mouseInputHandler, GameStateManager gameStateManager) : base(mouseInputHandler, gameStateManager)
    {
        _game = game;
    }

    public override string Text { get; set; } = "Exit";
    public override string Title { get; set; } = "Game Over!";
    public override Color TitleColor { get; set; } = Color.Red;

    public override void OnClick()
    {
        _game.Exit();
    }
}