using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Legacy { 
    public class PlayerRunning : PlayerGrounded
{
    private float runSpeed;
    private float direction;

    public PlayerRunning(Player player, float direction) : base(player)
    {
        runSpeed = 8f;
        this.direction = direction;
    }

    public override void HandleInput()
    {
        direction = Input.GetAxisRaw("Horizontal");

        base.HandleInput();


        if (direction == 0)
        {
            player.State = new PlayerIdle(player);
            player.SetHorizontalVelocity(0);
        }
                
        if(Input.GetAxisRaw("Vertical") == -1)
        {
            player.AddHorizontalVelocity(60f * direction);
            player.State = new PlayerSliding(player, direction);
        }


        
    }
    public override void Update(float dt)
    {
        player.SetHorizontalVelocity(runSpeed * direction);
        
        base.Update(dt);
        
        //float distance = Mathf.Abs(_player.Velocity.x);
        //float rad = Mathf.Deg2Rad * _collisionTracker.HorAngle;

        //_player.Velocity = new Vector2
        //{
        //    x = distance * Mathf.Cos(rad) * direction,
        //    y = distance * Mathf.Sin(rad)
        //};
    }

    protected override void Jump()
    {
        player.State = new PlayerJumping(player, direction);
    }

    protected override void HandleCollision(ref Vector2 nextPosition)
    {
        Collision cInfo;
        if (direction == 1)
        {
            cInfo = collisionTracker.Right;
        }
        else
        {
            cInfo = collisionTracker.Left;
        }

        if (cInfo && cInfo.Angle <= MaxRunSlopeAngle())
        {
            float distance = Mathf.Abs(nextPosition.x);
            float rad = Mathf.Deg2Rad * cInfo.Angle;

            //distance -= cInfo.Distance/*;*/

            nextPosition.x = distance * Mathf.Cos(rad) * direction;
            nextPosition.y = distance * Mathf.Sin(rad);

            //nextPosition.x += distance * direction;
            
        }


        //if (cInfo)
        //{
        //    nextPosition.x = cInfo.Distance * direction;
        //}
        

     

       else if (collisionTracker.Down)
            nextPosition.y = -collisionTracker.Down.Distance;

        else if (collisionTracker.Up)
            nextPosition.y = collisionTracker.Down.Distance;

        

    }

    private float MaxRunSlopeAngle()
    {
        return 65f;
    }
}
}