using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallgrab : PlayerState
{
    private float horDirection;
    private bool drop;
    private float speed;

    public PlayerWallgrab(Player player, float direction) : base(player)
    {
        if (direction != -1 && direction != 1)
            throw new System.ArgumentException("Invalid Direction argument. Argument not -1 or 1");

        speed = -3f;
        horDirection = direction;

        base.player.SetHorizontalVelocity(0);
        base.player.SetVerticalVelocity(0);
    }

    public override void HandleInput()
    {   
        if (Input.GetAxisRaw("Horizontal") != horDirection)
        {
            player.State = new PlayerFalling(player);
        }
        drop = Input.GetAxisRaw("Vertical") == -1;
    }
    public override void Update(float dt)
    {
        if (drop)
            player.SetVerticalVelocity(speed);
        else
            player.SetVerticalVelocity(0);

        base.Update(dt);

        if (collisionTracker.Down)
            player.State = new PlayerIdle(player);
    }
}