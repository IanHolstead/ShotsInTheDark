using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Arrow : MonoBehaviour {
    private Rigidbody2D rb;

    public float lifetime = 0.8f;
    public int damage = 1;

    public float fadeTime = .15f;

    private bool arrowStoped = false;
    private float age = 0;
    private Player owner;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Collider2D>().enabled = false;
    }

	void Update () {
        age += Time.deltaTime;
        if (age > lifetime && owner)
        {
            StopArrow();
        }
	}

    public void Shoot(float speed, Player owner) {
        this.owner = owner;
        Physics2D.IgnoreCollision(owner.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        GetComponent<Collider2D>().enabled = true;
        Vector2 direction = transform.rotation * Vector2.right;
        rb.AddForce(direction * speed);
    }

    void StopArrow()
    {
        rb.velocity = Vector2.zero;
        if (owner)
        {
            Physics2D.IgnoreCollision(owner.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
        }
        owner = null;
        arrowStoped = true;
    }

    void PickupArrow(Player player)
    {
        player.GetComponent<PlayerShooting>().AddArrows(1);

        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponentInChildren<LightScript>().FadeAway(fadeTime);
        Destroy(gameObject, fadeTime);
    }

    void OnCollisionEnter2D(Collision2D otherObject)
    {
        Player player = otherObject.gameObject.GetComponent<Player>();
        //TODO: Remove me
        if (player == owner)
        {
            Logger.Log("owner: " + owner, this, LogLevel.Log);
            return;
        }
        
        if (!arrowStoped)
        {
            if (player)
            {
                bool isPlayerDead = player.OnShot(damage);
                Debug.Log(player.ToString() + isPlayerDead);
                if (!isPlayerDead)
                {
                    PickupArrow(player);
                }
            }
            StopArrow();
            rb.Sleep();
        }
        else
        {
            if (player)
            {
                PickupArrow(player);
            }
        }
    }
}
