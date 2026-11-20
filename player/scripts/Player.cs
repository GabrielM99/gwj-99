using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public float Speed = 200.0f;
	[Export]
	public float LadderSpeed = 100.0f;
	[Export]
	public float JumpVelocity = -200.0f;
	[Export]
    public bool CanUseLadder = false;

    private bool OnLadder = false;

    [Export]
    public AnimatedSprite2D sprite;

    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor() && !OnLadder)
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("pular") && IsOnFloor())
		{
            velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("andar_esquerda", "andar_direita", "subir", "descer");
		if (direction.X != 0)
		{
			velocity.X = direction.X * Speed;
			sprite.FlipH = direction.X < 0;
            sprite.Play("walk");
        }
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            sprite.Play("idle");
        }

		// Escada
		OnLadder = CanUseLadder && (OnLadder || direction.Y != 0);
		if (!CanUseLadder) OnLadder = false;

		if (OnLadder)
		{
			velocity.Y = direction.Y * LadderSpeed; // 0 quando não aperta nada, então fica parado
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
