using Godot;
using System;

/// <summary>
/// NPC se aplica para robos e inimigos
/// </summary>
public partial class NPC : WorldEntity
{
    [Export]
    public float MaxSpeed = 200f;

    [Export]
    public float Acceleration = 400f;

    [Export]
    public float Deceleration = 600f;

    [Export]
    public Vector2 Direction { get; set; } = Vector2.Zero;

    /// <summary>
    /// Deve ser definido por instancia
    /// </summary>
    [Export]
    public NPCType Type { get; set; }

    private Vector2 _currentVelocity = Vector2.Zero;

    [Export]
    public Health Health { get; set; }

    public override void _Ready()
    {
        Health?.Morte += Morreu;
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 targetVelocity = Direction.Normalized() * MaxSpeed;

        float rate = Direction.IsZeroApprox() ? Deceleration : Acceleration;

        _currentVelocity = _currentVelocity.MoveToward(targetVelocity, rate * (float)delta);

        GlobalPosition += _currentVelocity * (float)delta;
    }

    private void Morreu()
    {
        QueueFree();
    }
}
