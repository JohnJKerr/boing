using Godot;

namespace boing
{
	public class PlayerController : IController
	{
		private readonly int _playerNumber;
		
		public PlayerController(int playerNumber)
		{
			_playerNumber = playerNumber;
		}
		
		public void Control(IMovable movable)
		{
			bool IsActionPressed(string name) => Input.IsActionPressed(name + _playerNumber);
			if (IsActionPressed("up")) movable.Move(Direction.Up);
			if (IsActionPressed("down")) movable.Move(Direction.Down);
		}
	}
}