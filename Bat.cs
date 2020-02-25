using Godot;
using System;

public class Bat : KinematicBody2D
{
	[Export] private int _playerNumber = 0;

	private Sprite Sprite => GetNode<Sprite>(nameof(Sprite));

	public override void _Ready()
	{
		SetTexture();
	}

	private void SetTexture(int number = 0)
	{
		var texture = ResourceLoader.Load($"res://gfx/bat{_playerNumber}/bat{_playerNumber}{number}.png") as Texture;
		Sprite.Texture = texture;
	}
}