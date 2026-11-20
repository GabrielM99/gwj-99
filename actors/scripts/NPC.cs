using Godot;
using System;

/// <summary>
/// NPC se aplica para robos e inimigos
/// </summary>
public partial class NPC : Node2D
{
    [Export]
    public float MaxSpeed = 200f;

    [Export]
    public float Acceleration = 400f;

    [Export]
    public float Deceleration = 600f;

    [Export]
    public Vector2 TargetPosition { get; set; } = Vector2.Zero;

    /// <summary>
    /// Deve ser definido por instancia
    /// </summary>
    [Export]
    public NPCType Type { get; set; }

    private Vector2 _direction = Vector2.Zero;
    private float _currentSpeed = 0f;

    [Export]
    public Health Health { get; set; }

    public override void _Ready()
    {
        if (Health != null)
        {
            Health.Morte += Morreu;
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        float dt = (float)delta;

        Vector2 target = TargetPosition - GlobalPosition;
        float distancia = target.Length();

        if (distancia < 1f)
        {
            GlobalPosition = TargetPosition;
            _currentSpeed = 0f;
            return;
        }

        //calcula a velocidade maxima que ele pode continuar andando ate chegar no destino
        float velocidadeParando = Mathf.Sqrt(2f * Deceleration * distancia);
        float velocidade = Mathf.Min(MaxSpeed, velocidadeParando);

        //indica se esta parando ou nao
        float rate = velocidade < _currentSpeed ? Deceleration : Acceleration;
        _currentSpeed = Mathf.MoveToward(_currentSpeed, velocidade, rate * dt);

        // pra nao andar alem da distancia que falta
        float step = Mathf.Min(_currentSpeed * dt, distancia);

        GlobalPosition += target.Normalized() * step;
    }

    private void Morreu()
    {
        QueueFree();
    }
}
