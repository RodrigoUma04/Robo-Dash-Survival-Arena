using Microsoft.Xna.Framework;
public class StartMenuScreen : MenuScreen
{
    public StartMenuScreen(MouseInputHandler mouseInputHandler, GameStateManager gameStateManager) : base(mouseInputHandler, gameStateManager)
    {}

    public override string Text { get; set; } = "Start";
    public override string Title { get; set; } = "Alien Survival World";
    public override Color TitleColor { get; set; } = Color.Black;

    public override void OnClick()
    {
        _gameStateManager.StartGame();
    }
}