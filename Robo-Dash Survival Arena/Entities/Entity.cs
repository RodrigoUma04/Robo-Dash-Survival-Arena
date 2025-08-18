using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using System.Collections.Generic;

public abstract class Entity : IEntity
{
    public Vector2 Position { get; protected set; }
    public CStates CurrentState { get; private set; } = CStates.Idle;
    public Dictionary<CStates, List<Texture2D>> Animations { get; protected set; } = new();
    public abstract string SpawnType { get; }

    protected int _currentFrame = 0;
    protected float _frameTime = 0.2f;
    protected float _frameTimer = 0f;

    public virtual void LoadContent(ContentManager content) { }

    public virtual void Update(GameTime gameTime)
    {
        if (!Animations.ContainsKey(CurrentState) || Animations[CurrentState].Count == 0)
            return;

        _frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_frameTimer >= _frameTime)
        {
            _frameTimer = 0f;
            _currentFrame = (_currentFrame + 1) % Animations[CurrentState].Count;
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        if (Animations.ContainsKey(CurrentState) && Animations[CurrentState].Count > 0)
        {
            spriteBatch.Draw(Animations[CurrentState][_currentFrame], Position, Color.White);
        }
    }

    public void ChangeState(CStates newState)
    {
        if (newState != CurrentState)
        {
            CurrentState = newState;
            _currentFrame = 0;
            _frameTimer = 0f;
        }
    }

    public virtual void Spawn(TiledMap map, ContentManager content)
    {
        var characterLayer = map.GetLayer<TiledMapObjectLayer>("Spawns");
        if (characterLayer != null)
        {
            foreach (var obj in characterLayer.Objects)
            {
                if (obj.Properties.ContainsKey("type") && obj.Properties["type"].ToString() == SpawnType)
                {
                    this.Position = new Vector2(obj.Position.X, obj.Position.Y);
                    this.LoadContent(content);
                    break;
                }
            }
        }
    }
}
