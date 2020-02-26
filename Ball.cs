using Godot;
using System;

public class Ball : KinematicBody2D
{
    private const int MaxSpeed = 50;
    private int _speed = 10;
    private Vector2 _motion = Vector2.Zero;

    public override void _Ready()
    {
        var direction = new Random().Next() <= 0.5 ? Vector2.Left : Vector2.Right;
        _motion = direction;
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        Move();
    }

    private void Move()
    {
        var collisionInfo = MoveAndCollide(_motion * _speed);
        if (collisionInfo == default) return;
        if(_speed < MaxSpeed) _speed += 5;
        _motion = _motion.Bounce(collisionInfo.Normal);
    }
}
