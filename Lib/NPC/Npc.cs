using Godot;
using System;

/// <summary>
/// NPC se aplica para robos e inimigos
/// </summary>
public partial class Npc : Node2D
{
    [Export]
    public float MaxSpeed = 200f;

    [Export]
    public float Aceleracao = 400f;

	[Export]
    public float Desaceleracao = 600f;

    [Export]
	public Vector2 TargetPosition { get; set; } = Vector2.Zero;

    private Vector2 _direction = Vector2.Zero;
    private float _currentSpeed = 0f;

    [Export]
    private Vida _vida;

    public override void _Ready()
    {
        if (_vida != null){
            _vida.Morte += Morreu;
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
	{
		float dt = (float)delta;

        Vector2 Target = TargetPosition - GlobalPosition;
        float distancia = Target.Length();

        if (distancia < 1f){
            GlobalPosition = TargetPosition;
            _currentSpeed = 0f;
            return;
        }

		//calcula a velocidade maxima que ele pode continuar andando ate chegar no destino
        float velocidadeParando = Mathf.Sqrt(2f * Desaceleracao * distancia);
        float velocidade = Mathf.Min(MaxSpeed, velocidadeParando);

		//indica se esta parando ou nao
        float rate = velocidade < _currentSpeed ? Desaceleracao : Aceleracao;
        _currentSpeed = Mathf.MoveToward(_currentSpeed, velocidade, rate * dt);

		// pra nao andar alem da distancia que falta
        float step = Mathf.Min(_currentSpeed * dt, distancia);
        
		GlobalPosition += Target.Normalized() * step;
    }

    private void Morreu(){
        QueueFree();
    }
}
