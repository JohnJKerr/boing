using Godot;

namespace boing
{
	public interface IMovable
	{
		int Speed { get; }
		Vector2 Position { get; }
		Vector2 Motion { get; set; }
	}
}