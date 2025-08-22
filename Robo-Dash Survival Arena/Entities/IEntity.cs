using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public enum CStates
{
    Idle,
    Walk,
    Jump,
    Hit,
    Duck,
    Attack
}

public interface IEntity
{
    Vector2 Position { get; }
    bool IsGrounded { get; set; }
    CStates CurrentState { get; }
    Dictionary<CStates, List<Texture2D>> Animations { get; }
    bool IsFlipped { get; }
    int Width { get; }
    int Height { get; }
    float Scale { get;  }

    bool IsDestroyed { get; }

    void LoadContent(ContentManager content);
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
    void ChangeState(CStates newState);
    Rectangle GetBoundingBox();
}
