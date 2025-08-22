using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class Spike : Enemy
{
    public Spike(Entity hero, bool isGiant) : base(hero, isGiant) { }

    public override void LoadContent(ContentManager content)
    {
        string giant = IsGiant ? "Double" : "Default";

        Animations[CStates.Idle] = new List<Texture2D>
        {
            content.Load<Texture2D>($"kenney_new-platformer-pack-1.0/Sprites/Enemies/{giant}/slime_spike_rest")
        };

        Animations[CStates.Attack] = new List<Texture2D>
        {
            content.Load<Texture2D>($"kenney_new-platformer-pack-1.0/Sprites/Enemies/{giant}/slime_spike_walk_a"),
            content.Load<Texture2D>($"kenney_new-platformer-pack-1.0/Sprites/Enemies/{giant}/slime_spike_walk_b")
        };
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (HeroRef.Position.X > Position.X)
            IsFlipped = true;
        else
            IsFlipped = false;
    }
}
