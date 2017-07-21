using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Legacy
{
    public class PlayerFalling : PlayerAirborne
    {
        private float gravity;

        public PlayerFalling(Player player) : base(player)
        {
            gravity = -50f;
        }


        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void Update(float dt)
        {
            player.AddVerticalVelocity(gravity * dt);

            base.Update(dt);

            if (collisionTracker.Down == true)
            {
                if (PlayerHighImpact.IsHighImpact(player.Velocity))
                {
                    player.State = new PlayerHighImpact(player);
                }
                else
                {
                    if (player.Velocity.x == 0)
                        player.State = new PlayerIdle(player);

                    else
                        player.State = new PlayerRunning(player, Mathf.Sign(player.Velocity.x));

                    player.SetVerticalVelocity(0);

                }

            }

        }
    } 
}