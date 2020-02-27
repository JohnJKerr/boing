using Godot;
using System;
using boing;

public class Bat : KinematicBody2D, IMovable, IControllable
{
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
		Move();
	}

	private void Move()
	{
		Controller.UpdateMotion(this);
		MoveAndSlide(Motion);
	}

	private void SetTexture(int number = 0)
	{
		var texture = ResourceLoader.Load($"res://gfx/bat{_playerNumber}/bat{_playerNumber}{number}.png") as Texture;
		Sprite.Texture = texture;
	}

	public int Speed { get; } = 200;
	public Vector2 Motion { get; set; } = Vector2.Zero;
	public IController Controller { get; private set; }
}