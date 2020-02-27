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
		
		public void UpdateMotion(IMovable movable)
		{
			bool IsActionPressed(string name) => Input.IsActionPressed(name + _playerNumber);
			var motion = Vector2.Zero;
			if (IsActionPressed("up")) motion.y -= movable.Speed;
			if (IsActionPressed("down")) motion.y += movable.Speed;
			movable.Motion = motion;
		}
	}
}