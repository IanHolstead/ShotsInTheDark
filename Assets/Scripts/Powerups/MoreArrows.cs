using UnityEngine;
using System.Collections;

public class MoreArrows : PickupParentClass {
    
    public int arrowsToAdd = 2;

    public override void UsePickup( Player player)
    {
        player.GetComponent<PlayerShooting>().AddArrows(arrowsToAdd);
    }
}
