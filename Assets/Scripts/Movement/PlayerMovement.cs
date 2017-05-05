using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour { 
    private PlayerAnimation animationComponent;

    public float movementSpeed = 5f;

    void Awake()
    {
        animationComponent = GetComponent<PlayerAnimation>();
    }

    public void Move(float horizontalAxis, float verticalAxis, bool isShooting)
    {
        if (!isShooting)
        {
            Vector2 toMove = new Vector2(horizontalAxis, verticalAxis);
            toMove.Normalize(); 
            GetComponent<Rigidbody2D>().velocity = toMove * movementSpeed * Time.deltaTime;

            animationComponent.MovementAnimation(verticalAxis, horizontalAxis);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
