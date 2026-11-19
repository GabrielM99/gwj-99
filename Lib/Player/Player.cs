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

    [Export]
    public AnimatedSprite2D sprite;

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
            sprite.FlipH = direction.X < 0;

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
