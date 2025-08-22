using Microsoft.Xna.Framework;

public class GameWonScreen : MenuScreen
{
    private Game _game;
    public GameWonScreen(Game game, MouseInputHandler mouseInputHandler, GameStateManager gameStateManager) : base(mouseInputHandler, gameStateManager)
    {
        _game = game;
    }

    public override string Text { get; set; } = "Exit";
    public override string Title { get; set; } = "Congratulations, you survived!";
    public override Color TitleColor { get; set; } = Color.Green;

    public override void OnClick()
    {
        _game.Exit();
    }
}