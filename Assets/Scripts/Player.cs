using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Player : MonoBehaviour {
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private MoveSet moveSet;
    [SerializeField]
    private Vector2 gravity;
    [SerializeField]
    private float jumpVelocity = 10;
    [SerializeField]
    private float jumpDeceleration;
    [SerializeField]
    private float maxHoveringTime;
    [SerializeField]
    private float wallDeceleration;

    [SerializeField]
    private int horizontalRays;
    [SerializeField]
    private int verticalRays;
    [SerializeField]
    private float waitTime;


    private float horizontalRaySpacing;
    private float verticalRaySpacing;

    private double jumpTime = 0;

    private Vector3 velocity = Vector3.zero;
    private new BoxCollider2D collider;
    
    private int mask;
    private bool isHovering;
    private float hoveringTime;
    private float skinWidth = 0.02f;
    private Rigidbody2D body;
    private PlayerState state;
    public StateDisplay stateDisplay;

    void Start () {
        collider = GetComponent<BoxCollider2D>();
        mask = LayerMask.GetMask("World");
        body = GetComponent<Rigidbody2D>();

        CalculateRaySpacing();
        Physics2D.gravity = Vector2.zero;
        state = new PlayerState();
        stateDisplay.State = state;
	}

    private void CalculateRaySpacing()
    {
        Bounds bounds = GetBounds();
        
        float width = bounds.size.x;
        float height = bounds.size.y;

        horizontalRaySpacing = height / (horizontalRays - 1);
        verticalRaySpacing = width / (verticalRays - 1);
    }

    private void FixedUpdate()
    {
        
    }

    private void Update ()
    {
        //MovementIteration1();
        //MovementIteration2();
        //MovementIteration3();
        MovementIteration4();
    }

    private void MovementIteration4()
    {
        Vector2 newVelocity = body.velocity;
        
        state.IsGrounded = false;
        state.IsWallHugging = false;

        GroundCheck();
        WallCheck();
        
        if (state.IsGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            state.IsGrounded = false;
            state.IsJumping = true;
            newVelocity.y = jumpVelocity;
        }

        float directionX = Input.GetAxis("Horizontal");

        if (state.IsWallHugging && state.WallDirection == Direction.Left && directionX < 0)
        {

        }
        else if (state.IsWallHugging && state.WallDirection == Direction.Right && directionX > 0)
        {

        }
        else
        {
            newVelocity.x = speed * directionX;
        }

        if (state.IsGrounded == false && state.IsWallHugging == false)
        {            
            newVelocity += gravity * Time.deltaTime;
        }


        body.velocity = newVelocity;

    }

    private void WallCheck()
    {
        Bounds bounds = GetBounds();
        for (int i = 0; i < horizontalRays; i++)
        {
            Vector2 origin = bounds.min;
            origin.y += i * horizontalRaySpacing;
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.left, 0.1f + skinWidth, mask);

            if (hit && hit.distance < 0.02 + skinWidth)
            {
                state.IsWallHugging = true;
                state.WallDirection = Direction.Left;
                break;
            }
        }


        for (int i = 0; i < horizontalRays; i++)
        {
            Vector2 origin = new Vector2(bounds.max.x, bounds.min.y);
            origin.y += i * horizontalRaySpacing;
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right, 0.1f + skinWidth, mask);

            if (hit && hit.distance < 0.02 + skinWidth)
            {
                state.IsWallHugging = true;
                state.WallDirection = Direction.Right;
                break;
            }
        }
    }

    private void GroundCheck()
    {
        Bounds bounds = GetBounds();
        for (int i = 0; i < verticalRays; i++)
        {
            Vector2 origin = bounds.min;
            origin.x += i * verticalRaySpacing;
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 0.1f, mask);

            if (hit && hit.distance < 0.02 + skinWidth)
            {
                //Debug.Log("Hit: " + hit.distance);
                state.IsGrounded = true;
                state.IsJumping = false;
                break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.transform.name);        
    }

    //private void MovementIteration3()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        Debug.Log("Jump Start");
    //        jumpTime = 0;
    //        isJumping = true;
    //    }

    //    if (isJumping)
    //    {
    //        Debug.Log(jumpTime);
    //    }

    //    if (jumpTime >= 1.0)
    //    {
    //        isJumping = false;
    //    }
        
    //}

    //private void MovementIteration2()
    //{
    //    Bounds bounds = GetBounds();
    //    Vector2 moveVector;
    //    Vector2 origin;
    //    RaycastHit2D hit;

    //    HandleJump();
    //    HandleRun();

    //    moveVector = velocity * Time.deltaTime;

    //    // Limit vertically
    //    if (moveVector.y != 0)
    //    {
    //        isGrounded = false;
    //        int directionY = Math.Sign(moveVector.y);

    //        Debug.Log(directionY);

    //        if (IsUp(directionY))
    //        {
    //            origin = new Vector2(bounds.min.x, bounds.max.y);
    //        }
    //        else
    //        {
    //            origin = new Vector2(bounds.min.x, bounds.min.y);
    //        }

    //        for (int i = 0; i < verticalRays; ++i)
    //        {
    //            Vector2 rayOrigin = origin;
    //            rayOrigin.x += verticalRaySpacing * i;

    //            hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, directionY * moveVector.y + skinWidth, mask);
    //            Debug.DrawRay(rayOrigin, Vector2.up * directionY * (moveVector.y + skinWidth));

    //            if (hit)
    //            {
    //                float distance = hit.distance - skinWidth;
    //                moveVector.y = directionY * distance;
    //                velocity.y = 0;
    //                if (directionY == -1)
    //                    isGrounded = true;
    //            }
    //        }
    //    }

    //    // Limit horizontally
    //    if (moveVector.x != 0)
    //    {
    //        int directionX = Math.Sign(moveVector.x);

    //        if (IsRight(directionX))
    //        {
    //            origin = new Vector2(bounds.max.x, bounds.min.y);
    //        }
    //        else
    //        {
    //            origin = new Vector2(bounds.min.x, bounds.min.y);
    //        }
    //        for (int i = 0; i < horizontalRays; i++)
    //        {
    //            Vector2 rayOrigin = origin;
    //            rayOrigin.y += horizontalRaySpacing * i;

    //            hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, directionX * moveVector.x + skinWidth, mask);
    //            Debug.DrawRay(rayOrigin, Vector2.right * ((directionX * moveVector.x) + skinWidth));

    //            if (hit)
    //            {
    //                float distance = hit.distance - skinWidth;
    //                moveVector.x = directionX * distance;
    //                wallHugging = true;
    //            }
    //        }

    //    }

    //    transform.Translate(moveVector);
    //}

    private Bounds GetBounds()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(-2 * skinWidth);
        return bounds;
    }

    private void HandleRun()
    {
        int horizontal = Math.Sign(Input.GetAxisRaw("Horizontal"));
        velocity.x = speed * horizontal;
    }
    
    //private void HandleJump()
    //{
    //    if (isJumping)
    //    {
    //        jumpTime += Time.deltaTime;
    //    }
    //    if (isHovering)
    //        hoveringTime += Time.deltaTime;

    //    if (isGrounded && Input.GetKeyDown(KeyCode.Space))
    //    {
    //        isGrounded = false;
    //        isJumping = true;
    //        jumpTime = 0;
    //        hoveringTime = 0;
    //        SetVelocityY(jumpVelocity);
    //        Debug.Log("Jump start");
    //    }
        
    //    if (isJumping && (Input.GetKeyUp(KeyCode.Space) || jumpTime >= maxJumpTime || velocity.y < 0))
    //    {
    //        isJumping = false;
    //        isHovering = true;
    //        Debug.Log("Jump end");
    //        Debug.Log(transform.position);
    //    }
    //    if (isHovering && hoveringTime >= maxHoveringTime)
    //    {
    //        isHovering = false;
    //    }

    //    if (isJumping)
    //        DecelerateJump();
    //    else if (isHovering)
    //        SetVelocityY(0);
    //    else if (wallHugging)
    //        WallGlideDown();
    //    else
    //        AddGravity();
    //}

    private void WallGlideDown()
    {
        velocity.y -= wallDeceleration * Time.deltaTime;
    }

    //private void MovementIteration1()
    //{
    //    Bounds bounds = collider.bounds;
    //    Vector2 moveVector;
    //    Vector2 origin;
    //    RaycastHit2D hit;

    //    // Set Velocity according to 
    //    AddGravity();

    //    bool jump = Input.GetKeyDown(KeyCode.Space);

    //    if (jump && isGrounded)
    //    {
    //        SetVelocityY(jumpVelocity);
    //        isGrounded = false;
    //    }
    //    else if (jump && wallHugging)
    //    {
    //        SetVelocityY(jumpVelocity);
    //        SetVelocityX(3f);
    //        isGrounded = false;
    //    }

    //    int directionX = Math.Sign(Input.GetAxisRaw("Horizontal"));
    //    SetVelocityX(directionX * speed);

    //    wallHugging = false;

    //    // Limit movement 

    //    moveVector = velocity * Time.deltaTime;

    //    // Limit vertically 
    //    int directionY = Math.Sign(moveVector.y);

    //    if (IsUp(directionY))
    //    {
    //        origin = new Vector2(bounds.center.x, bounds.max.y);
    //    }
    //    else
    //    {
    //        origin = new Vector2(bounds.center.x, bounds.min.y);
    //    }


    //    hit = Physics2D.Raycast(origin, Vector2.up, moveVector.y, mask);
    //    Debug.DrawRay(origin, Vector2.up * moveVector.y);

    //    if (hit)
    //    {
    //        moveVector.y = directionY * hit.distance;
    //        velocity.y = 0;
    //        if (directionY == -1)
    //            isGrounded = true;
    //    }

    //    // Limit horizontally

    //    if (directionX != 0)
    //    {
    //        if (IsRight(directionX))
    //        {
    //            origin = new Vector2(bounds.max.x, bounds.center.y);
    //        }
    //        else
    //        {
    //            origin = new Vector2(bounds.min.x, bounds.center.y);
    //        }


    //        hit = Physics2D.Raycast(origin, Vector2.right * directionX, moveVector.x, mask);
    //        Debug.DrawRay(origin, Vector2.right * directionX * moveVector.x);

    //        if (hit)
    //        {
    //            transform.Translate(Vector2.right * directionX * hit.distance);
    //            wallHugging = true;
    //            moveVector.x = 0;
    //        }
    //    }


    //    // Move Character
    //    transform.Translate(moveVector);
    //}

    private void SetVelocityX(float velocityX)
    {
        velocity.x = velocityX;
    }

    private void SetVelocityY(float velocityY)
    {
        velocity.y = velocityY;
    }

    private void AddGravity()
    {
        //velocity += gravity * Time.deltaTime;
    }

    private void DecelerateJump()
    {
        velocity.y -= jumpDeceleration * Time.deltaTime;
    }

    private bool IsUp(int sign)
    {
        if (sign == 1)
            return true;
        else if (sign == -1)
            return false;
        else
            throw new ArgumentException();
    }

    private bool IsRight(int sign)
    {
        if (sign == 1)
            return true;
        else if (sign == -1)
            return false;
        else
            throw new ArgumentException();
    }

    public class PlayerState
    {
        public bool IsGrounded { get; set; }
        public bool IsWallHugging { get; set; }
        public bool IsJumping { get; set; }
        public Direction WallDirection { get; set; }
    }

    public enum Direction
    {
        Left, Right, None
    }
}
