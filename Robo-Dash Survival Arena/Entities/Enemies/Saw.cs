using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class Saw : Enemy
{
    public Saw(Entity hero, bool isGiant) : base(hero, isGiant) { }

    public override void LoadContent(ContentManager content)
    {
        string giant = IsGiant ? "Double" : "Default";

        Animations[CStates.Idle] = new List<Texture2D>
        {
            content.Load<Texture2D>($"kenney_new-platformer-pack-1.0/Sprites/Enemies/{giant}/saw_rest")
        };

        Animations[CStates.Attack] = new List<Texture2D>
        {
            content.Load<Texture2D>($"kenney_new-platformer-pack-1.0/Sprites/Enemies/{giant}/saw_a"),
            content.Load<Texture2D>($"kenney_new-platformer-pack-1.0/Sprites/Enemies/{giant}/saw_b")
        };
    }

    public override Rectangle GetBoundingBox()
    {
        return new Rectangle((int)Position.X, (int)Position.Y + 20, Width, Height - 20);
    }
}
