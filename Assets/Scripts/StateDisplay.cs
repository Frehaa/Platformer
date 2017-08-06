using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateDisplay : MonoBehaviour {

    public Player.PlayerState State { get; set; }

    private Text left;
    private Text right;
    private Text grounded;
    private Text jumping;

    // Use this for initialization
    void Start () {        
        Debug.Log("Children: " + transform.childCount);
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Text text = child.GetComponent<Text>();

            switch (child.tag)
            {
                case "StateDisplayerLeft":
                    left = text;
                    break;
                case "StateDisplayerRight":
                    right = text;
                    break;
                case "StateDisplayerJumping":
                    jumping = text;
                    break;
                case "StateDisplayerGrounded":
                    grounded = text;
                    break;
                default:
                    break;
            }
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        if (State.IsWallHugging && State.WallDirection == Player.Direction.Left)
            left.color = Color.red;
        else
            left.color = Color.white;

        if (State.IsWallHugging && State.WallDirection == Player.Direction.Right)
            right.color = Color.red;
        else
            right.color = Color.white;

        if (State.IsGrounded)
            grounded.color = Color.red;
        else
            grounded.color = Color.white;

        if (State.IsJumping)
            jumping.color = Color.red;
        else
            jumping.color = Color.white;
    }
}
