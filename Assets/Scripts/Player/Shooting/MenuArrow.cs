using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuArrow : ArrowBaseClass{
    public override void Shoot(float speed, PlayerPawn owner)
    {
        Vector2 direction = transform.rotation * Vector2.right;
        rb.AddForce(direction * speed);
    }

    protected override void Tick()
    {
        //stop arrow at right location
    }

    protected override void OnCollision(Collision2D otherObject)
    {
        
    }

    protected override void StopArrow()
    {
        rb.velocity = Vector2.zero;
    }

    protected override void PickupArrow(PlayerPawn player)
    {
        
    }
}
