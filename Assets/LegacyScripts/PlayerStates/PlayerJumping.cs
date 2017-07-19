using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumping : PlayerAirborne
{
    private float time;
    private float speed;
    private float jumpTime;

    public PlayerJumping(Player player, float direction = 0f) : base(player, direction)
    {
        time = 0f;
        speed = 6f;
        jumpTime = 1f;
    }

    public override void HandleInput()
    {
        bool jump = Input.GetAxisRaw("Jump") == 1;

        if (!jump)
            player.State = new PlayerFalling(player);

        base.HandleInput();

    }
    public override void Update(float dt)
    {
        time += dt;

        if (time > jumpTime)
            player.State = new PlayerFalling(player);
        else
            player.SetVerticalVelocity(speed);

        base.Update(dt);

        if (collisionTracker.Up)
            player.State = new PlayerFalling(player);

    }
}