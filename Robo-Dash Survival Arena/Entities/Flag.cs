using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class Flag : Entity
{
    public override void LoadContent(ContentManager content)
    {
        Animations[CStates.Idle] = new List<Texture2D>
        {
            content.Load<Texture2D>($"kenney_new-platformer-pack-1.0/Sprites/Tiles/Default/flag_blue_a"),
            content.Load<Texture2D>($"kenney_new-platformer-pack-1.0/Sprites/Tiles/Default/flag_blue_b"),
        };
    }
}