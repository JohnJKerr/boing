using Godot;
using System;

public class Goal : Node2D
{
    public override void _Ready()
    {
        
    }

    public void OnGoalScored(Node node)
    {
        if (node is Ball ball)
            GetTree().CallGroup(nameof(Game), nameof(Goal), this);
    }
}
