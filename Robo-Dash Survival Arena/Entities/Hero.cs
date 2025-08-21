using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class Hero : Entity
{
    private static Hero _uniqueInstance;
    public float Speed { get; set; } = 200f;
    public float JumpForce { get; set; } = 600f;
    private SoundEffect _jumpEffect;

    private Hero(){}

    public static Hero getInstance()
    {
        if (_uniqueInstance == null)
        {
            _uniqueInstance = new Hero();
        }
        return _uniqueInstance;
    }

    public override void LoadContent(ContentManager content)
    {
        Animations[CStates.Idle] = new List<Texture2D>
        {
            content.Load<Texture2D>("kenney_new-platformer-pack-1.0/Sprites/Characters/Default/character_purple_idle")
        };

        Animations[CStates.Walk] = new List<Texture2D>
        {
            content.Load<Texture2D>("kenney_new-platformer-pack-1.0/Sprites/Characters/Default/character_purple_walk_a"),
            content.Load<Texture2D>("kenney_new-platformer-pack-1.0/Sprites/Characters/Default/character_purple_walk_b")
        };

        Animations[CStates.Jump] = new List<Texture2D>
        {
            content.Load<Texture2D>("kenney_new-platformer-pack-1.0/Sprites/Characters/Default/character_purple_jump")
        };

        _jumpEffect = content.Load<SoundEffect>("kenney_new-platformer-pack-1.0/Sounds/sfx_jump-high");
    }

    public void Move(Vector2 direction)
    {
        Velocity = new Vector2(direction.X * Speed, Velocity.Y);

        if (direction.X > 0)
            IsFlipped = false;
        else if (direction.X < 0)
            IsFlipped = true;

        if (direction.X != 0 && IsGrounded)
            ChangeState(CStates.Walk);
        else if (direction.X == 0 && IsGrounded)
            ChangeState(CStates.Idle);
    }

    public void Jump()
    {
        if (IsGrounded)
        {
            _jumpEffect.Play();
            Velocity = new Vector2(Velocity.X, -JumpForce);
            IsGrounded = false;
            ChangeState(CStates.Jump);
        }
    }

    public override Rectangle GetBoundingBox()
    {
        return new Rectangle((int)Position.X + Width / 2, (int)Position.Y + 25, Width, 100);
    }
}
