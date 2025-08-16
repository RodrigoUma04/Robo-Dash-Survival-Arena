using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using System.Collections.Generic;

public class Hero : Entity
{
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

    public override void Update(GameTime gameTime)
    {
        //FIXME move this to a keyboard input handler
        var keyboard = Keyboard.GetState();
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        bool moving = false;

        var pos = Position;

        if (keyboard.IsKeyDown(Keys.W)) { pos.Y -= Speed * dt; moving = true; }
        if (keyboard.IsKeyDown(Keys.S)) { pos.Y += Speed * dt; moving = true; }
        if (keyboard.IsKeyDown(Keys.A)) { pos.X -= Speed * dt; moving = true; }
        if (keyboard.IsKeyDown(Keys.D)) { pos.X += Speed * dt; moving = true; }

        Position = pos;

        ChangeState(moving ? CStates.Walk : CStates.Idle);

        base.Update(gameTime);
    }

    public override void Spawn(TiledMap map, ContentManager content)
    {
        var characterLayer = map.GetLayer<TiledMapObjectLayer>("Spawns");
        if (characterLayer != null)
        {
            foreach (var obj in characterLayer.Objects)
            {
                if (obj.Properties.ContainsKey("type") && obj.Properties["type"].ToString() == "hero")
                {
                    this.Position = new Vector2(obj.Position.X, obj.Position.Y);
                    this.LoadContent(content);
                    break;
                }
            }
        }
    }
}
