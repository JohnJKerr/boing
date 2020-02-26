using Godot;
using System;

public class Bat : KinematicBody2D
{
	private const int Speed = 200;
	[Export] private int _playerNumber = 0;
	private Vector2 _motion = Vector2.Zero;

	private Sprite Sprite => GetNode<Sprite>(nameof(Sprite));

	public override void _Ready()
	{
		SetTexture();
	}

	public override void _Process(float delta)
	{
		base._Process(delta);
		Move();
	}

	private void Move()
	{
		_motion.y = 0;
		if (Input.IsActionPressed("up")) _motion.y -= Speed;
		if (Input.IsActionPressed("down")) _motion.y += Speed;
		MoveAndSlide(_motion);
	}

	private void SetTexture(int number = 0)
	{
		var texture = ResourceLoader.Load($"res://gfx/bat{_playerNumber}/bat{_playerNumber}{number}.png") as Texture;
		Sprite.Texture = texture;
	}
}