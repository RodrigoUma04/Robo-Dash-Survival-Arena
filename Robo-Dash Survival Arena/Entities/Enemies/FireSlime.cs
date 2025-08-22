using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class FireSlime : Enemy
{
    private float _speed = 50f;
    private float _patrolDistance = 300f;
    private Vector2 _startPosition;
    private int _direction = 1;
    public FireSlime(Entity hero, bool isGiant) : base(hero, isGiant) { }

    public override void LoadContent(ContentManager content)
    {
        string giant = IsGiant ? "Double" : "Default";

        DetectionRange = 500f;

        Animations[CStates.Idle] = new List<Texture2D>
        {
            content.Load<Texture2D>($"kenney_new-platformer-pack-1.0/Sprites/Enemies/{giant}/slime_fire_rest")
        };

        Animations[CStates.Attack] = new List<Texture2D>
        {
            content.Load<Texture2D>($"kenney_new-platformer-pack-1.0/Sprites/Enemies/{giant}/slime_fire_walk_a"),
            content.Load<Texture2D>($"kenney_new-platformer-pack-1.0/Sprites/Enemies/{giant}/slime_fire_walk_b")
        };

        _startPosition = Position;
    }

    public override void Update(GameTime gameTime)
    {
        if (IsAwake)
        {
            Position += new Vector2(_direction * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);

            if (Position.X > _startPosition.X + _patrolDistance)
                _direction = -1;
            else if (Position.X < _startPosition.X - _patrolDistance)
                _direction = 1;

            IsFlipped = _direction == 1;
        }

        base.Update(gameTime);
    }
}
