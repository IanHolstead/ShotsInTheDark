using UnityEngine;
using System.Collections;

public abstract class PickupParentClass : MonoBehaviour {
    public float fadeTime = .25f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            UsePickup(player);

            Destroy(gameObject, fadeTime);
            GetComponentInChildren<LightScript>().FadeAway(fadeTime);
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public abstract void UsePickup(Player parent);

    //TODO: rewrite this
    void OnDestroy()
    {
        PowerupManager.pickupPickedup();

    }
}
