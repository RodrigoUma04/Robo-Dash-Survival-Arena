using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public abstract class Entity : IEntity
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity = Vector2.Zero;
    public bool IsGrounded { get; set; } = true;

    public CStates CurrentState { get; private set; } = CStates.Idle;
    public Dictionary<CStates, List<Texture2D>> Animations { get; protected set; } = new();
    public bool IsFlipped { get; protected set; } = false;
    public int Width { get; protected set; } = 64;
    public int Height { get; protected set; } = 64;
    public float Scale { get; protected set; } = 1f;
    
    public bool IsDestroyed { get; protected set; }

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
            var texture = Animations[CurrentState][_currentFrame];
            var effects = IsFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            spriteBatch.Draw(texture, Position, null, Color.White, 0f, Vector2.Zero, Scale, effects, 0f);
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

    public virtual Rectangle GetBoundingBox()
    {
        return new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
    }
}
