using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

    private Animator animator;

    
    void Start () {
        animator = GetComponent<Animator>();
    }

    public void MovementAnimation(float verticalAxis, float horizontalAxis)
    {
        //TODO: make this just one function call and set variables
        if (Mathf.Abs(verticalAxis) > Mathf.Abs(horizontalAxis))
        {
            if (verticalAxis > 0)
            {
                animator.SetInteger("direction", 2);
                animator.SetBool("idle", false);
            }
            else if (verticalAxis < 0)
            {
                animator.SetInteger("direction", 0);
                animator.SetBool("idle", false);
            }
        }
        else
        {
            if (horizontalAxis > 0)
            {
                animator.SetInteger("direction", 1);
                animator.SetBool("idle", false);
            }
            else if (horizontalAxis < 0)
            {
                animator.SetInteger("direction", 3);
                animator.SetBool("idle", false);
            }
            else
            {
                animator.SetBool("idle", true);
            }
        }
    }

    public void AimAnimation(float aimHorizontal, float aimVertical, Quaternion arrowAngle)
    {
        if (Mathf.Abs(aimVertical) > Mathf.Abs(aimHorizontal))
        {
            if (aimVertical > 0)
            {
                animator.SetInteger("shootingDirection", 2);
            }
            else if (aimVertical < 0)
            {
                animator.SetInteger("shootingDirection", 0);
            }
        }
        else
        {
            if (aimHorizontal > 0)
            {
                animator.SetInteger("shootingDirection", 1);
            }
            else if (aimHorizontal < 0)
            {
                animator.SetInteger("shootingDirection", 3);
            }
        }
    }

    public void ShootAnimation(bool isShooting)
    {
        animator.SetBool("shooting", isShooting);
    }

}
