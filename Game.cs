using Godot;
using System;
using boing;

public class Game : Node2D
{
    private Bat Bat => GetNode<Bat>(nameof(Bat));
    private Bat Bat2 => GetNode<Bat>(nameof(Bat2));
    private Ball Ball => GetNode<Ball>(nameof(Ball));
    public float Height => GetViewport().Size.y;
    public float Width => GetViewport().Size.x;
    
    public override void _Ready()
    {
        AddToGroup(nameof(Game));
        Bat.Controller = new AiController(this, Ball);
        Bat2.Controller = new AiController(this, Ball);
    }
}
