using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using System.Collections.Generic;

public abstract class Entity : IEntity
{
    public Vector2 Position { get; set; }
    public CStates CurrentState { get; set; } = CStates.Idle;
    public Dictionary<CStates, List<Texture2D>> Animations { get; set; } = new();

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
    }
}
