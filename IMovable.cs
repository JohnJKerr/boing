using Godot;

namespace boing
{
	public interface IMovable
	{
		Vector2 Position { get; }
		void Move(Direction direction);
	}
}