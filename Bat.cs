using Godot;
using System;
using boing;

public class Bat : KinematicBody2D, IMovable, IControllable
{
	private const int _speed = 200;
	private Vector2 _motion = Vector2.Zero;
	private Vector2 _startingPosition;
	private Sprite Sprite => GetNode<Sprite>(nameof(Sprite));
	private AnimationPlayer AnimationPlayer => GetNode<AnimationPlayer>(nameof(AnimationPlayer));
	
	[Export] public int PlayerNumber { get; private set; } = 0;
	[Export] public AvailableGoal GoalDefended { get; private set; }

	public float Height => Sprite.Texture.GetSize().y;

	public override void _Ready()
	{
		AddToGroup(nameof(Game));
		SetTexture();
		Controller = new PlayerController(this);
		_startingPosition = Position;
	}

	public override void _Process(float delta)
	{
		base._Process(delta);
		Controller.Control(this);
	}

	private void SetTexture(int number = 0)
	{
		var texture = ResourceLoader.Load($"res://gfx/bat{PlayerNumber}/bat{PlayerNumber}{number}.png") as Texture;
		Sprite.Texture = texture;
	}

	public IController Controller { get; set; }
	public void Move(Direction direction)
	{
		_motion = Vector2.Zero;
		if (direction.Equals(Direction.Up)) _motion.y -= _speed;
		if (direction.Equals(Direction.Down)) _motion.y += _speed;
		MoveAndSlide(_motion);
	}

	public void Hit()
	{
		Position = new Vector2(_startingPosition.x, Position.y);
		AnimationPlayer.Play(nameof(Hit).ToLower());
	}
	
	public void Goal(Goal goal)
	{
		if(GoalDefended.ToString() == goal.Name)
			Conceded();
	}

	public void Conceded()
	{
		AnimationPlayer.Play(nameof(Conceded).ToLower());
	}

	public enum AvailableGoal
	{
		Left,
		Right
	}
}