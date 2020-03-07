using Godot;
using System;

public class Overlay : Node2D
{
    private AnimationPlayer AnimationPlayer => GetNode<AnimationPlayer>(nameof(AnimationPlayer));
    
    public override void _Ready()
    {
        AddToGroup(nameof(Game));
    }
    
    public void Goal(Goal goal)
    {
        if(Name == goal.Name)
            AnimationPlayer.Play(nameof(Goal).ToLower());
    }
}
