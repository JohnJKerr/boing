using Godot;
using System;

public class Ball : KinematicBody2D
{
    private const int MaxSpeed = 50;
    private const int StartSpeed = 10;
    private int _speed = StartSpeed;
    private Vector2 _motion = Vector2.Zero;
    private Vector2 _startPosition;

    public override void _Ready()
    {
        _startPosition = Position;
        AddToGroup(nameof(Game));
        Start();
    }

    private void Start()
    {
        var direction = new Random().Next() <= 0.5 ? Vector2.Left : Vector2.Right;
        _motion = direction;
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        Move();
    }

    public void Goal(Goal goal)
    {
        _speed = StartSpeed;
        Position = _startPosition;
        Start();
    }

    private void Move()
    {
        var collisionInfo = MoveAndCollide(_motion * _speed);
        if (collisionInfo == default) return;
        if(_speed < MaxSpeed) _speed += 5;
        var collisionMotion = collisionInfo.Normal;
        if (collisionInfo.Collider is Bat bat)
        {
            var difference = Position.y - bat.Position.y;
            var deflection = difference / bat.Height;
            collisionMotion = new Vector2(collisionMotion.x, deflection);
            bat.Hit();
        }

        collisionMotion.y = Math.Min(Math.Max(collisionMotion.y, -1), 1);
        _motion = _motion.Bounce(collisionMotion).Normalized();
    }
}
