using UnityEngine;
using System.Collections.Generic;

public class PickupLightEmUp : PickupParentClass {

    public float lightUpLength = 2.0f;
    public GameObject lightPlayersUp;

    public override void UsePickup(PlayerPawn player)
    {
        foreach (GameObject otherPlayer in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (otherPlayer.GetComponent<PlayerPawn>() != player)
            {
                GameObject light = Instantiate(lightPlayersUp, transform.position + new Vector3(0, 0, -1), transform.rotation);
                light.GetComponent<LightPlayersUp>().playerToStalk = otherPlayer;
                light.GetComponent<LightPlayersUp>().lifeSpan = lightUpLength;
            }
        }
    }
}
