using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private PlayerState state;
    
    private Vector2 velocity; // In (unity)units per second

    void Awake () {
        Velocity = new Vector2();
        State = new PlayerIdle(this);

        DebugSettings();
	}
    
    void Update () {
        state.HandleInput();
        state.Update(Time.deltaTime);
    }

    public PlayerState State
    {
        set {
            state = value;
        }
    }
    
    public Vector2 Velocity
    {
        set
        {
            velocity = value;
        }
        get
        {
            return velocity;
        }
    }
    
    
    /* Convenience velocity methods */ 

    public void SetVerticalVelocity(float distance)
    {
        velocity.y = distance;
    }

    public void AddVerticalVelocity(float distance)
    {
        velocity.y += distance;
    }

    public void SetHorizontalVelocity(float distance)
    {
        velocity.x = distance;
    }
    
    public void AddHorizontalVelocity(float distance)
    {
        velocity.x += distance;
    }

    /* End region */


    /* Debug */
    private void DebugSettings()
    {
        Physics2D.alwaysShowColliders = true;
    }

    /* End region*/ 


}
