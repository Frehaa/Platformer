using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Legacy
{
    public abstract class PlayerGrounded : PlayerState
    {
        public PlayerGrounded(Player player) : base(player)
        { }

        public override void HandleInput()
        {
            if (Input.GetAxisRaw("Jump") == 1)
                Jump();
        }

        public override void Update(float dt)
        {
            base.Update(dt);

            if (collisionTracker.Down == false)
                player.State = new PlayerFalling(player);
        }

        protected abstract void Jump();
    } 
}