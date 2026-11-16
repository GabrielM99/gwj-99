using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 200.0f;
	public const float LadderSpeed = 100.0f;
	public const float JumpVelocity = -200.0f;

    public bool CanUseLadder = false;


    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
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
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			if (CanUseLadder){
            	velocity.Y = direction.Y * LadderSpeed;
			}
        }
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
