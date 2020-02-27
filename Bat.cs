using Godot;
using System;
using boing;

public class Bat : KinematicBody2D, IMovable, IControllable
{
	private const int _speed = 200;
	private Vector2 _motion = Vector2.Zero;
	[Export] private int _playerNumber = 0;

	private Sprite Sprite => GetNode<Sprite>(nameof(Sprite));

	public override void _Ready()
	{
		SetTexture();
		Controller = new PlayerController(_playerNumber);
	}

	public override void _Process(float delta)
	{
		base._Process(delta);
		Controller.Control(this);
	}

	private void SetTexture(int number = 0)
	{
		var texture = ResourceLoader.Load($"res://gfx/bat{_playerNumber}/bat{_playerNumber}{number}.png") as Texture;
		Sprite.Texture = texture;
	}

	public IController Controller { get; private set; }
	public void Move(Direction direction)
	{
		_motion = Vector2.Zero;
		if (direction.Equals(Direction.Up)) _motion.y -= _speed;
		if (direction.Equals(Direction.Down)) _motion.y += _speed;
		MoveAndSlide(_motion);
	}
}