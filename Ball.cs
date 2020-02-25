using Godot;
using System;

public class Ball : KinematicBody2D
{
    private Vector2 _motion = Vector2.Zero;

    public override void _Ready()
    {
        var direction = new Random().Next() <= 0.5 ? Vector2.Left : Vector2.Right;
        _motion = direction;
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        var collisionInfo = MoveAndCollide(_motion);
        if (collisionInfo != default)
            _motion = _motion.Bounce(collisionInfo.Normal);
    }
}
