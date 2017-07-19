using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAirborne : PlayerState
{
    private float speed;
    private float direction;
    
    public PlayerAirborne(Player player, float direction = 0f) : base(player)
    {
        speed = 8f;
        this.direction = direction;
    }

    public override void HandleInput()
    {
        direction = Input.GetAxisRaw("Horizontal");
    }
    public override void Update(float dt)
    {
        if (Mathf.Abs(player.Velocity.x) <= speed + 3f)
        {
            if (direction != 0)
            {
                player.SetHorizontalVelocity(speed * direction);
            }
            else
            {
                player.SetHorizontalVelocity(0);
            }
        }

        base.Update(dt);


    }

    protected override void HandleCollision(ref Vector2 nextStep)
    {

        base.HandleCollision(ref nextStep);

        if (collisionTracker && PlayerHighImpact.IsHighImpact(player.Velocity))
        {
            player.State = new PlayerHighImpact(player);
        }

        if (collisionTracker.Right)
        {
            if (direction == 1)
                player.State = new PlayerWallgrab(player, direction);
        }

        if (collisionTracker.Left)
        {
            if (direction == -1)
                player.State = new PlayerWallgrab(player, direction);
        }

        if (collisionTracker.Up)
        {


        }
    }
}