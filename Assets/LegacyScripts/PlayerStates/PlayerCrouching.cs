using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Legacy
{
    public class PlayerCrouching : PlayerGrounded
    {
        private float runSpeed;
        private float direction;

        public PlayerCrouching(Player player) : this(player, 0)
        { }

        public PlayerCrouching(Player player, float direction) : base(player)
        {
            runSpeed = 3f;
            this.direction = direction;
        }

        public override void HandleInput()
        {
            base.HandleInput();

            direction = Input.GetAxisRaw("Horizontal");

            if (Input.GetAxisRaw("Vertical") != -1)
            {
                if (player.Velocity.x == 0)
                {
                    player.State = new PlayerIdle(player);
                }
                else
                {
                    player.State = new PlayerRunning(player, Mathf.Sign(player.Velocity.x));
                }
            }

        }
        public override void Update(float dt)
        {
            player.SetHorizontalVelocity(runSpeed * direction);

            base.Update(dt);

        }

        protected override void Jump()
        {
            throw new NotImplementedException();
        }
    } 
}