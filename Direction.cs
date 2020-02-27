namespace boing
{
	public class Direction
	{
		private Direction(string name)
		{
			Name = name;
		}

		private string Name { get; }
		
		public static Direction Up = new Direction(nameof(Up));
		public static Direction Down = new Direction(nameof(Down));

		public override bool Equals(object obj)
		{
			if (!(obj is Direction direction)) return false;
			return Equals(direction);
		}

		private bool Equals(Direction other)
		{
			return Name == other.Name;
		}

		public override int GetHashCode()
		{
			return (Name != null ? Name.GetHashCode() : 0);
		}
	}
}