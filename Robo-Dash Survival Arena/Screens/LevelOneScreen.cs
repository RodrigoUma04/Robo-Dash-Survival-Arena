using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

public class LevelOneScreen : IGameState
{
    private TiledMap _map;
    private TiledMapRenderer _mapRenderer;
    private Entity _hero;
    private List<Entity> _entities = new List<Entity>();
    private Camera _camera;
    private KeyboardInputHandler _keyboardInputHandler;
    private CollisionHandler _collisionHandler;
    private HUD _hud;
    private GameStateManager _gameStateManager;

    public LevelOneScreen(GameStateManager gameStateManager)
    {

        _gameStateManager = gameStateManager;
        _collisionHandler = new CollisionHandler(_gameStateManager);

        PlayerData playerData = PlayerData.getInstance();
        _hud = HUD.getInstance();

        playerData.NotifyObservers("Coins", playerData.Coins);
        playerData.NotifyObservers("Lives", playerData.Lives);
    }

    public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
    {
        _map = content.Load<TiledMap>("Tiled/Maps/level_1");
        _mapRenderer = new TiledMapRenderer(graphicsDevice, _map);

        _collisionHandler.LoadFromMap(_map, content);
        _hud.LoadContent(content);

        var spawns = _map.GetLayer<TiledMapObjectLayer>("Spawns");
        foreach (var obj in spawns.Objects)
        {
            if (!obj.Properties.ContainsKey("type")) continue;

            string type = obj.Properties["type"].ToString().ToLower();
            var position = new Vector2(obj.Position.X, obj.Position.Y);

            var entity = EntityFactory.Create(type, _hero, position, content);
            if (entity != null)
            {
                _entities.Add(entity);
                if (entity is Hero hero)
                {
                    _hero = hero;
                    _keyboardInputHandler = new KeyboardInputHandler(hero);
                }
            }
        }

        _camera = new Camera(graphicsDevice, _hero.Position);
    }

    public void Update(GameTime gameTime)
    {
        _mapRenderer.Update(gameTime);

        _keyboardInputHandler.Update(gameTime);

        _collisionHandler.Resolve(_hero, (float)gameTime.ElapsedGameTime.TotalSeconds);

        foreach (var entity in _entities)
            entity.Update(gameTime);

        _entities.RemoveAll(e => e.IsDestroyed);

        _camera.Update(_hero.Position, _map);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var viewMatrix = _camera.GetViewMatrix();

        _mapRenderer.Draw(viewMatrix);

        spriteBatch.Begin(transformMatrix: viewMatrix);

        foreach (var entity in _entities)
        {
            entity.Draw(spriteBatch);
        }

        spriteBatch.End();

        spriteBatch.Begin();
        _hud.Draw(spriteBatch);
        spriteBatch.End();
    }
}