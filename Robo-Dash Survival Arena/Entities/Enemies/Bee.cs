using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class Bee : Enemy
{
    private float _speed = 125f;

    public Bee(Entity hero, bool isGiant) : base(hero, isGiant) { }

    public override void LoadContent(ContentManager content)
    {
        string giant = IsGiant ? " Double" : "Default";
        ChangeState(CStates.Attack);
        DetectionRange = 600f;

        Animations[CStates.Attack] = new List<Texture2D>
        {
            content.Load<Texture2D>($"kenney_new-platformer-pack-1.0/Sprites/Enemies/{giant}/bee_a"),
            content.Load<Texture2D>($"kenney_new-platformer-pack-1.0/Sprites/Enemies/{giant}/bee_b"),
        };
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (IsAwake)
        {
            Vector2 direction = HeroRef.Position - Position;

            if (direction != Vector2.Zero)
                direction.Normalize();

            Velocity = direction * _speed;

            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            IsFlipped = Velocity.X > 0;
        }
    }
}