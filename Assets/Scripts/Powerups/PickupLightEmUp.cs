using UnityEngine;
using System.Collections.Generic;

public class PickupLightEmUp : PickupParentClass {

    public float lightUpLength = 2.0f;
    public GameObject lightPlayersUp;

    public override void UsePickup(PlayerPawn player)
    {
        foreach (Player otherPlayer in GM.GameInstance.Players)
        {
            if (otherPlayer.Pawn != player && otherPlayer.Pawn.IsPlayerAlive)
            {
                GameObject lightGameObject = Instantiate(lightPlayersUp, transform.position + new Vector3(0, 0, -1), transform.rotation);
                LightPlayersUp light = lightGameObject.GetComponent<LightPlayersUp>();
                light.playerToStalk = otherPlayer.Pawn.gameObject;
                light.lifeSpan = lightUpLength;
            }
        }
    }
}
