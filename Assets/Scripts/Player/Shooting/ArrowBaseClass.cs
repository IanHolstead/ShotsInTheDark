using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ArrowBaseClass : MonoBehaviour {
    protected Rigidbody2D rb;

    public float lifetime = 0.8f;
    public int damage = 1;

    public float fadeTime = .15f;

    protected bool arrowStoped = false;
    protected float age = 0;
    bool insideObject = false;
    protected PlayerPawn owner;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Collider2D>().enabled = false;
    }

	void Update () {
        age += Time.deltaTime;
        Tick();
	}

    protected virtual void Tick()
    {
        if (age > lifetime && owner && !insideObject)
        {
            StopArrow();
        }
    }

    public virtual void Shoot(float speed, PlayerPawn owner) {
        this.owner = owner;
        Physics2D.IgnoreCollision(owner.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        GetComponent<Collider2D>().enabled = true;
        Vector2 direction = transform.rotation * Vector2.right;
        rb.AddForce(direction * speed);
    }

    protected virtual void StopArrow()
    {
        rb.velocity = Vector2.zero;
        if (owner)
        {
            Physics2D.IgnoreCollision(owner.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
        }
        owner = null;
        arrowStoped = true;
    }

    protected virtual void PickupArrow(PlayerPawn player)
    {
        player.GetComponent<PlayerShooting>().AddArrows(1);

        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponentInChildren<LightScript>().FadeAway(fadeTime);
        Destroy(gameObject, fadeTime);
    }
    
    protected virtual void OnCollisionEnter2D(Collision2D otherObject)
    {
        PlayerPawn player = otherObject.gameObject.GetComponent<PlayerPawn>();
        
        if (!arrowStoped)
        {
            if (player)
            {
                bool isPlayerDead = player.OnShot(damage);
                Logger.Log(player.ToString() + isPlayerDead, this, LogLevel.Verbose);
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
    
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ArrowPassThough")
        {
            insideObject = true;
        }
    }
    
    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "ArrowPassThough")
        {
            //TODO: this is very naive. If two are overlapping this could stop an arrow inside it
            insideObject = false;
        }
    }
}
