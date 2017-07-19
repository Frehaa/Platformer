using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected Player player;
    protected CollisionTracker collisionTracker;
    
    public PlayerState(Player player)
    {
        this.player = player;
        collisionTracker = new CollisionTracker(this.player.GetComponent<Collider2D>());

        
    }
    
    public abstract void HandleInput();
    
    public virtual void Update(float dt)
    {
        Vector2 travelDistance = player.Velocity * dt;

        // Check for collisions
        collisionTracker.CalculateCollisions(travelDistance);

        // Handle collisions
        if (collisionTracker)
            HandleCollision(ref travelDistance);

        player.transform.Translate(travelDistance);


    }

    protected virtual void HandleCollision(ref Vector2 nextPosition)
    {
        if (collisionTracker.Down)
            nextPosition.y = -collisionTracker.Down.Distance;

        else if (collisionTracker.Up)
            nextPosition.y = collisionTracker.Down.Distance;


        if (collisionTracker.Right)
            nextPosition.x = collisionTracker.Down.Distance;

        else if (collisionTracker.Left)
            nextPosition.x = -collisionTracker.Down.Distance;


    }



}