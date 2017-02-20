using UnityEngine;
using System.Collections;

public class BetterBow : PickupParentClass {
    
    public float factorToIncreaseBowStrength = 2f;

    public override void UsePickup(Player player)
    {
        player.GetComponent<PlayerShooting>().arrowSpeed *= factorToIncreaseBowStrength;
    }
}
