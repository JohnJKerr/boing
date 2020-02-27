using Godot;

namespace boing
{
	public interface IControllable
	{
		IController Controller { get; }
	}
}