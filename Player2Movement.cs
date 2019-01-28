using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : MonoBehaviour
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private Animator animator;

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontalp2");           //These are set up in Unity to WASD
        float moveVertical = Input.GetAxis("Verticalp2");

        Vector2 currentVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;

        //Movement along X-axis
        float newVelocityX = 0f;
        if (moveHorizontal < 0 && currentVelocity.x <= 0)
        {
            animator.SetInteger("DirectionX", -1);              //Tells the animator to use the walk left animation
            newVelocityX = -speed;                              //Speed in the direction of left

        }
        else if (moveHorizontal > 0 && currentVelocity.x >= 0)
        {
            animator.SetInteger("DirectionX", 1);               //Tells the animator to use the walk right animation
            newVelocityX = speed;                               //Speed in the direction of right
        }
        else
        {
            animator.SetInteger("DirectionX", 0);               //If no movement, animation stops/idle animation
        }


        //Movement along the Y-axis
        float newVelocityY = 0f;
        if (moveVertical < 0 && currentVelocity.y <= 0)
        {
            animator.SetInteger("DirectionY", -1);              //Tells the animator to use the walk down animation
            newVelocityY = -speed;                              //Speed in the direction of down
        }
        else if (moveVertical > 0 && currentVelocity.y >= 0)
        {
            newVelocityY = speed;                               //Speed in the direction of up
            animator.SetInteger("DirectionY", 1);               //Tells the animator to use the walk up animation
        }
        else
        {
            animator.SetInteger("DirectionY", 0);               //If no movement, animation stops/idle animation
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(newVelocityX, newVelocityY);
    }
}