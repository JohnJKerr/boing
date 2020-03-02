using System;
using Godot;

namespace boing
{
	public class AiController : IController
	{
		private readonly Game _game;
		private readonly Ball _ball;

		public AiController(Game game, Ball ball)
		{
			_game = game;
			_ball = ball;
		}

		public void Control(IMovable movable)
		{
			var distance = Math.Abs(_ball.Position.x - movable.Position.x);
			var target1 = _game.Height / 2;
			var target2 = _ball.Position.y;
			var weight1 = Math.Min(1, distance / (_game.Width / 2));
			var weight2 = 1 - weight1;
			var target = Math.Round((weight1 * target1) + (weight2 * target2));
			if (movable.Position.y > target) movable.Move(Direction.Up);
			if (movable.Position.y < target) movable.Move(Direction.Down);
		}
	}
}