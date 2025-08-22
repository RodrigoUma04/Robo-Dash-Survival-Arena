using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Robo_Dash_Survival_Arena
{
    public class Game1 : Game
    {
        #region Screens config
        public static int ScreenWidth = 1024;
        public static int ScreenHeight = 512;
        private GameStateManager _gameStateManager;
        #endregion

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private MouseInputHandler _mouseInputHandler;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.ApplyChanges();

            _mouseInputHandler = new MouseInputHandler();
            _gameStateManager = new GameStateManager(Content, GraphicsDevice);
            PlayerData.getInstance().RegisterObserver(HUD.getInstance());
            PlayerData.getInstance().RegisterObserver(_gameStateManager);
            BossData.getInstance().RegisterObserver(HUD.getInstance());
            BossData.getInstance().RegisterObserver(_gameStateManager);

            CollisionHandler collisionHandler = new CollisionHandler(_gameStateManager);


            _gameStateManager.AddGameState("StartMenu", new StartMenuScreen(_mouseInputHandler, _gameStateManager));
            _gameStateManager.AddGameState("Level1Screen", new LevelOneScreen(_gameStateManager, collisionHandler));
            _gameStateManager.AddGameState("Level2Screen", new LevelTwoScreen(_gameStateManager, collisionHandler));
            _gameStateManager.AddGameState("Level3Screen", new LevelThreeScreen(_gameStateManager, collisionHandler));
            _gameStateManager.AddGameState("Retry", new  RetryScreen(_mouseInputHandler, _gameStateManager));
            _gameStateManager.AddGameState("GameOver", new  GameOverScreen(_mouseInputHandler, _gameStateManager));
            _gameStateManager.AddGameState("NextLevel", new  NextLevelScreen(_mouseInputHandler, _gameStateManager));

            _gameStateManager.SetActiveGameState("StartMenu");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SoundManager.getInstance().LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _mouseInputHandler.Update(gameTime);
            _gameStateManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Transparent);

            _gameStateManager.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
