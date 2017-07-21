using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Legacy
{

    public class PlayerSliding : PlayerGrounded
{
    private float direction;
    private float minSlideSpeed;

    public PlayerSliding(Player player, float direction) : base(player)
    {
        this.direction = direction;
        minSlideSpeed = 4f;
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void Update(float dt)
    {
        if (CanSlide())
        {
            player.AddHorizontalVelocity(SlideSlow() * direction * dt);
        }
        else
        {
            player.SetHorizontalVelocity(0);
            player.State = new PlayerIdle(player);
        }

        base.Update(dt);
        
        if (collisionTracker.Right || collisionTracker.Left)
        {
            if (PlayerHighImpact.IsHighImpact(player.Velocity))
                player.State = new PlayerHighImpact(player);

            else
                player.State = new PlayerIdle(player);
        }
    }

    private bool CanSlide()
    {
        return Mathf.Abs(player.Velocity.x) > minSlideSpeed;
    }

    private float SlideSlow()
    {
        return -25f;
    }

    protected override void Jump()
    {
        throw new NotImplementedException();
    }
}
}
