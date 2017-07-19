using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script to give a "drag 'n throw" kind of behavior to an object with a rigidbody. */ 
public class VelocityByDrag : MonoBehaviour {

    private Player player;
    private Vector2 startPosition;
    
	void Awake () {
        player = GetComponent<Player>();
        startPosition = new Vector2();
	}
    
    void Update () {
        if(Input.GetMouseButtonDown(0))
            startPosition = Input.mousePosition;

        if(Input.GetMouseButtonUp(0))
        {
            Vector2 velocityChange = (Vector2) Input.mousePosition - startPosition;
            player.Velocity = velocityChange * 0.05f;
        }

	}
}
