using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Legacy
{
    public class PlayerHighImpact : PlayerState
    {
        private float time;
        private static float stunTime = 0.5f;

        public PlayerHighImpact(Player player) : base(player)
        {
            Debug.Log(base.player.Velocity);
            base.player.SetHorizontalVelocity(0);
            base.player.SetVerticalVelocity(0);
        }

        public static bool IsHighImpact(Vector2 velocity)
        {
            return velocity.x >= 35f || velocity.y >= 35f || velocity.x <= -35f || velocity.y <= -35f;
        }

        public override void HandleInput()
        {
            //base.HandleInput();
        }

        public override void Update(float dt)
        {
            time += dt;

            base.Update(dt);

            if (time >= stunTime)
            {
                if (collisionTracker.Down)
                    player.State = new PlayerIdle(player);
                else
                    player.State = new PlayerFalling(player);


            }

        }
    } 
}