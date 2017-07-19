using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Collision
{
    private float distance;
    private float angle;
    private bool hasCollided;

    public float Distance
    {
        get { return distance; }
    }

    public bool HasCollided
    {
        get { return hasCollided; }
    }

    public float Angle
    {
        get { return angle; }
    }

    public void Reset()
    {
        distance = 0f;
        angle = 0f;
        hasCollided = false;
    }

    public void SetCollision(RaycastHit2D hit)
    {
        hasCollided = true;
        distance = hit.distance;        

        angle = (hit.transform.eulerAngles.z % 180);

        if (angle > 90f)
            angle = 180f - Angle;

    }

    public static implicit operator bool(Collision c)
    {
        return c.hasCollided;
    }

}

public class CollisionTracker
{
    private Collision down;
    private Collision up;
    private Collision left;
    private Collision right;
    
    private Collider2D collider;

    private float skinWidth;
    private int horizontalRayCount;
    private int verticalRayCount;
    private float horizontalRaySpacing;
    private float verticalRaySpacing;
    private LayerMask layerMask;

    public CollisionTracker(Collider2D collider)
    {
        this.collider = collider;
        skinWidth = 0.01f;
        horizontalRayCount = 5;
        verticalRayCount = 5;
        layerMask = LayerMask.GetMask("World");
        CalculateRaySpacing();

        left = new Collision();
        right = new Collision();
        up = new Collision();
        down = new Collision();
    }

    public static implicit operator bool(CollisionTracker c)
    {
        return c.Down || c.Up || c.Left || c.Right;
    }

    public Collision Down
    {
        get { return down; }
    }

    public Collision Up
    {
        get { return up; }
    }

    public Collision Left
    {
        get { return left; }
    }

    public Collision Right
    {
        get { return right; }
    }

    public void Reset()
    {
        up.Reset();
        down.Reset();
        left.Reset();
        Left.Reset();
    }

    public void CalculateCollisions(Vector2 travelDistance)
    {
        Reset();

        /* Vertical collision detection */

        // Even if there is no vertical velocity we still want to check if we stand on anything
        if (travelDistance.y == 0)
        {
            RaycastHit2D hit = VerticalRays(-0.1f);
            if (hit && hit.distance == 0)
                down.SetCollision(hit);
        }
        // Else check for normal vertical collisions
        else
        {
            RaycastHit2D hit = VerticalRays(travelDistance.y);

            if (hit)
            {
                if (travelDistance.y > 0)
                    up.SetCollision(hit);

                else
                    down.SetCollision(hit);

            }
        }

        /* End region */


        /* Horizontal collision detection */

        // No collision detection if we have no horizontal velocity
        if (travelDistance.x != 0)
        {
            RaycastHit2D hit = HorizontalRays(travelDistance.x);

            if (hit)
            {
                if (travelDistance.x > 0)
                    right.SetCollision(hit);

                else
                    left.SetCollision(hit);
            }
        }

        /* End region */

    }

    /* Debug / learning / testing method */
    public void TestBoxCast()
    {
        Vector2 playerCenter = collider.bounds.center;
        // TODO: rework 
        Vector2 size = new Vector2(1f, 0.9f);
        float distance = 1f;


        RaycastHit2D hit = Physics2D.BoxCast(playerCenter, size, 0f, Vector2.down, distance, layerMask);

        if (hit)
        {
            Debug.Log(hit + " Distance: " + hit.distance + " Target: " + hit.transform.name);

            GameObject gameobject = GameObject.FindGameObjectWithTag("Test");
            if (gameobject != null)
            {
                BoxCollider2D groundCollider = gameobject.GetComponent<BoxCollider2D>();

                if (groundCollider != null)
                {
                    bool overlap = groundCollider.OverlapPoint(hit.point);
                    Debug.Log("Overlap: " + overlap);
                }
            }

        }

    }

    // Returns the hit for the shortest distance to the target
    private RaycastHit2D HorizontalRays(float distance)
    {
        float direction = Mathf.Sign(distance);
        float rayLength = Mathf.Abs(distance);

        Bounds bounds = collider.bounds;
        //bounds.Expand(-2 * skinWidth);

        Vector2 origin = new Vector2
        {
            x = (direction == -1? bounds.min.x : bounds.max.x),
            y = bounds.min.y
        };
        Color[] colors = { Color.red, Color.blue, Color.blue, Color.blue, Color.blue };

        RaycastHit2D hit = new RaycastHit2D();
        for (int i = 0; i < horizontalRayCount; ++i)
        {
            Vector2 rayOrigin = origin;
            rayOrigin -= Vector2.down * (horizontalRaySpacing * i);

            Debug.DrawRay(rayOrigin, Vector2.right * direction * rayLength, colors[i]);
            RaycastHit2D raycast = Physics2D.Raycast(rayOrigin, Vector2.right * direction, rayLength, layerMask);

            if (i == 0 && raycast)
            {

            }

            if (raycast)
            {
                // Preventing detection of farther away collisions
                rayLength = raycast.distance;
                hit = raycast;
                //hit.distance -= skinWidth;
            }

        }

        return hit;

    }
    
    private RaycastHit2D VerticalRays(float distance)
    {
        float direction = Mathf.Sign(distance);
        float rayLength = Mathf.Abs(distance);

        Bounds bounds = collider.bounds;
        //bounds.Expand(-2 * skinWidth);

        Vector2 origin = new Vector2
        {
            x = bounds.min.x,
            y = (direction == -1 ? bounds.min.y : bounds.max.y)
        };
        
        RaycastHit2D hit = new RaycastHit2D();

        Color[] colors = { Color.red, Color.blue, Color.blue, Color.blue, Color.blue };

        for (int i = 0; i < verticalRayCount; ++i)
        {
            Vector2 rayOrigin = origin;
            rayOrigin += Vector2.right * (verticalRaySpacing * i);

            Debug.DrawRay(rayOrigin, Vector2.up * direction * rayLength, colors[i]);
            RaycastHit2D raycast = Physics2D.Raycast(rayOrigin, Vector2.up * direction, rayLength, layerMask);

            if (raycast)
            {
                // Preventing detection of farther away collisions
                rayLength = raycast.distance;
                hit = raycast;
                //hit.distance -= skinWidth;
            }

        }

        return hit;
    }
    
    private void CalculateRaySpacing()
    {        
        Vector2 min = collider.bounds.min;
        Vector2 max = collider.bounds.max;
        
        float playerWidth = (max.x - min.x - 2 * skinWidth);
        float playerHeight = (max.y - min.y - 2 * skinWidth);

        horizontalRaySpacing = playerHeight / (horizontalRayCount - 1);
        verticalRaySpacing = playerWidth / (verticalRayCount - 1);
    }

    public void Resize()
    {
        CalculateRaySpacing();
    }


}