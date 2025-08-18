using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using System.Collections.Generic;

public class Hero : Entity
{
    public override string SpawnType => "hero";
    public float Speed { get; set; } = 200f;

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
    }

    public void Move(Vector2 direction, float deltaTime)
    {
        Position += direction * Speed * deltaTime;
        ChangeState(direction != Vector2.Zero ? CStates.Walk : CStates.Idle);
    }

    public void Jump()
    {
        // TODO: physics-based jump
    }

    public void Duck()
    {
        // TODO: physics-based jump
    }
}
