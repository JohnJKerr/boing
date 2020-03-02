using Godot;

namespace boing
{
	public class PlayerController : IController
	{
		private readonly Bat _bat;
		
		public PlayerController(Bat bat)
		{
			_bat = bat;
		}
		
		public void Control(IMovable movable)
		{
			bool IsActionPressed(string name) => Input.IsActionPressed(name + _bat.PlayerNumber);
			if (IsActionPressed("up")) movable.Move(Direction.Up);
			if (IsActionPressed("down")) movable.Move(Direction.Down);
		}
	}
}