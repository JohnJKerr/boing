using Godot;
using System;
using System.Linq;
using System.Reflection;

public class Ball : KinematicBody2D
{
    private const int MaxSpeed = 20;
    private const int StartSpeed = 5;
    private int _speed = StartSpeed;
    private Vector2 _motion = Vector2.Zero;
    private Vector2 _startPosition;
    private readonly Random _random;
    private Speed CurrentSpeed => Speed.GetByValue(_speed);

    public Ball()
    {
        _random = new Random();
    }

    public override void _Ready()
    {
        _startPosition = Position;
        AddToGroup(nameof(Game));
        Start();
    }

    private void Start()
    {
        var direction = _random.Next(2) <= 0.5 ? Vector2.Left : Vector2.Right;
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
        Collide(collisionInfo);
    }

    private void Collide(KinematicCollision2D collision)
    {
        GetFaster();
        Bounce(collision);
    }

    private void GetFaster()
    {
        if(_speed < MaxSpeed) _speed += 1;
    }

    private void Bounce(KinematicCollision2D collision)
    {
        var collisionMotion = collision.Normal;
        if (collision.Collider is Bat bat)
        {
            var difference = Position.y - bat.Position.y;
            var deflection = difference / bat.Height;
            collisionMotion = new Vector2(collisionMotion.x, deflection);
            bat.Hit();
        }

        collisionMotion.y = Math.Min(Math.Max(collisionMotion.y, -1), 1);
        _motion = _motion.Bounce(collisionMotion).Normalized();
        PlayBounceSound();
    }

    private void PlayBounceSound()
    {
        const string sound = "Hit";
        var defaultPlayer = GetNode<AudioStreamPlayer2D>($"SFX/{sound}");
        defaultPlayer.Play();
        var additionalPlayer = GetNode<AudioStreamPlayer2D>($"SFX/Hit{CurrentSpeed.Name}");
        additionalPlayer.Play();
    }

    private class Speed
    {
        private readonly int _speed;

        private Speed()
        {
        }
        
        private Speed(int speed, string name)
        {
            _speed = speed;
            Name = name;
        }

        public string Name { get; }
        
        private static Speed Slow = new Speed(StartSpeed, nameof(Slow));
        private static Speed Medium = new Speed(MaxSpeed / 2, nameof(Medium));
        private static Speed Fast = new Speed(MaxSpeed - StartSpeed, nameof(Fast));
        private static Speed VeryFast = new Speed(MaxSpeed, nameof(VeryFast));

        internal static Speed GetByValue(int speed)
        {
            var type = typeof(Speed);
            var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly);
            var instance = new Speed();
            var values = fields
                .Select(f => f.GetValue(instance) as Speed)
                .OrderByDescending(s => s._speed);
            return values.First(v => v._speed <= speed);
        }
    }
}
