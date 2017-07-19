using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private MoveSet moveSet;

    private Rigidbody2D body;
    private float direction = 0f;

    private bool isGrounded = false;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
	}

    private void FixedUpdate()
    {
        SetVelocityX(direction * speed);
    }

    // Update is called once per frame
    void Update () {
        direction = Input.GetAxisRaw("Horizontal");

        if (body.velocity.y == 0)
            isGrounded = true;

        bool jump = Input.GetAxisRaw("Jump") == 1;

        if (jump && isGrounded)
        {
            SetVelocityY(5f);
        }



    }

    private void SetVelocityX(float velocityX)
    {
        Vector3 newVelocity = body.velocity;
        newVelocity.x = velocityX;
        body.velocity = newVelocity;
    }

    private void SetVelocityY(float velocityY)
    {
        Vector3 newVelocity = body.velocity;
        newVelocity.y = velocityY;
        body.velocity = newVelocity;
    }
}
