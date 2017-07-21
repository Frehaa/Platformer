using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Legacy
{
    public class PlayerIdle : PlayerGrounded
    {

        public PlayerIdle(Player player) : base(player)
        { }

        public override void HandleInput()
        {
            base.HandleInput();

            float horizontal = Input.GetAxisRaw("Horizontal");

            if (horizontal != 0)
                player.State = new PlayerRunning(player, horizontal);

            else if (Input.GetAxisRaw("Vertical") == -1)
                player.State = new PlayerCrouching(player);

        }
        public override void Update(float dt)
        {
            base.Update(dt);
        }

        protected override void Jump()
        {
            player.State = new PlayerJumping(player);
        }
    } 
}