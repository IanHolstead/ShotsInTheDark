using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

    private Animator animator;

    
    void Awake () {
        animator = GetComponent<Animator>();
    }

    public void MovementAnimation(float verticalAxis, float horizontalAxis)
    {
        animator.speed = GameFunctions.MapRangeClamped(new Vector2(verticalAxis, horizontalAxis).magnitude, 0, 1, .5f, 1);
        
        if (Mathf.Abs(verticalAxis) > Mathf.Abs(horizontalAxis))
        {
            if (verticalAxis > 0)
            {
                UpdateMovementAnimation(2, false);
            }
            else if (verticalAxis < 0)
            {
                UpdateMovementAnimation(0, false);
            }
        }
        else
        {
            if (horizontalAxis > 0)
            {
                UpdateMovementAnimation(1, false);
            }
            else if (horizontalAxis < 0)
            {
                UpdateMovementAnimation(3, false);
            }
            else
            {
                animator.SetBool("idle", true);
                animator.speed = 1;
            }
        }
    }

    private void UpdateMovementAnimation(int direction, bool idle)
    {
        animator.SetInteger("direction", direction);
        animator.SetBool("idle", idle);
    }

    public void AimAnimation(float aimHorizontal, float aimVertical, Quaternion arrowAngle)
    {
        if (Mathf.Abs(aimVertical) > Mathf.Abs(aimHorizontal))
        {
            if (aimVertical > 0)
            {
                UpdateAimAnimation(2);
            }
            else if (aimVertical < 0)
            {
                UpdateAimAnimation(0);
            }
        }
        else
        {
            if (aimHorizontal > 0)
            {
                UpdateAimAnimation(1);
            }
            else if (aimHorizontal < 0)
            {
                UpdateAimAnimation(3);
            }
        }
    }

    private void UpdateAimAnimation(int direction)
    {
        animator.SetInteger("shootingDirection", direction);
    }

    public void ShootAnimation(bool isShooting)
    {
        animator.SetBool("shooting", isShooting);
    }

}
